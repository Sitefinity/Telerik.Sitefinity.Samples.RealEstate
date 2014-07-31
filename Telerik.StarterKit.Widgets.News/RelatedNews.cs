using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI;
using System.Web.UI;
using Telerik.Sitefinity.Web.Utilities;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.News.Model;
using Telerik.Web.UI;
using System.Web;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Model;
using System.Text.RegularExpressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.StarterKit.Widgets.News
{
    [ControlDesigner(typeof(RelatedNewsDesigner))]
    public class RelatedNews : SimpleView, ICustomWidgetVisualization
    {
        private NewsManager newsManager;
        private PageManager pageManager;
        private TaxonomyManager taxonomyManager;
        private PageNode currentPage;
        private string currentPageUrl;
        private NewsItem currentNewsItem;

        #region Constants

        private const string layoutTemplateName = "Telerik.StarterKit.Widgets.News.Resources.Views.RelatedNewsTemplate.ascx";
        private const string widgetNameRegularExpression = @"/!(?<urlPrefix>\w+)/.*";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of news items to be shown.
        /// </summary>
        public int NumberOfNewsToShow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                return layoutTemplateName;
            }
        }

        protected NewsManager NewsManager
        {
            get
            {
                if (this.newsManager == null)
                {
                    this.newsManager = NewsManager.GetManager();
                }
                return this.newsManager;
            }
        }

        protected PageManager PageManager
        {
            get
            {
                if (this.pageManager == null)
                {
                    this.pageManager = PageManager.GetManager();
                }
                return this.pageManager;
            }
        }

        protected TaxonomyManager TaxonomyManager
        {
            get
            {
                if (this.taxonomyManager == null)
                {
                    this.taxonomyManager = TaxonomyManager.GetManager();
                }
                return this.taxonomyManager;
            }
        }

        protected PageNode CurrentPage
        {
            get
            {
                if (this.currentPage == null)
                {
                    SiteMapNode currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
                    Guid currentPageId = new Guid(currentNode.Key);
                    this.currentPage = this.PageManager.GetPageNode(currentPageId);
                }

                return this.currentPage;
            }
        }

        protected string CurrentPageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(this.currentPageUrl))
                {
                    this.currentPageUrl = this.CurrentPage.GetFullUrl();
                }
                return this.currentPageUrl;
            }
        }

        protected NewsItem CurrentNewsItem
        {
            get
            {
                if (this.currentNewsItem == null)
                {
                    this.currentNewsItem = this.ResolveDetailItemFromUrl();
                }

                return this.currentNewsItem;
            }
        }

        #endregion

        #region Control references

        /// <summary>
        /// Gets the list control.
        /// </summary>
        /// <value>The instance of the list control.</value>
        protected virtual RadListView NewsList
        {
            get
            {
                return this.Container.GetControl<RadListView>("NewsList", true);
            }
        }

        #endregion

        #region Overridden controls

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            if (this.NumberOfNewsToShow > 0)
            {
                this.NewsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(NewsList_ItemDataBound);
                List<NewsItem> dataSource = this.GetNewsListDataSource();

                if (dataSource.Count > 0)
                {
                    this.NewsList.DataSource = dataSource;
                    this.NewsList.DataBind();
                }
                else
                {
                    this.Visible = false;
                }
                //this.ContentHtml.Text = LinkParser.ResolveLinks(this.Html, DynamicLinksParser.GetContentUrl, null, false);
            }

            this.IsEmpty = this.NumberOfNewsToShow < 1;
        }

        private void NewsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            NewsItem newsItem = (NewsItem)dataItem.DataItem;
            HyperLink hlNews = (HyperLink)e.Item.FindControl("hlNews");
            hlNews.Text = newsItem.Title;
            hlNews.NavigateUrl = this.CurrentPageUrl + this.NewsManager.GetItemUrl(newsItem);
        }

        private List<NewsItem> GetNewsListDataSource()
        {
            if (this.CurrentNewsItem == null)
            {
                return new List<NewsItem>(0);
            }

            List<NewsItem> dataSource = new List<NewsItem>(this.NumberOfNewsToShow);
            List<NewsItem> allNewsWithSameCategories = new List<NewsItem>();

            TrackedList<Guid> categoryIds = (TrackedList<Guid>)this.CurrentNewsItem.GetValue("Category");
            foreach (Guid id in categoryIds)
            {
                var items = App.WorkWith()
                                .NewsItems()
                                .Where(nI => nI.GetValue<TrackedList<Guid>>("Category").Contains(id)).Get().ToList();

                allNewsWithSameCategories.AddRange(items);
            }

            return allNewsWithSameCategories
                .Distinct()
                .Where(n => n.Status == ContentLifecycleStatus.Live && n.Id != this.CurrentNewsItem.Id)
                .OrderByDescending(n => n.PublicationDate)
                .Take(this.NumberOfNewsToShow)
                .ToList();
        }

        private NewsItem ResolveDetailItemFromUrl()
        {
            NewsItem result = null;
            var itemUrl = this.GetUrlParameterString(true);
            if (itemUrl != null)
            {
                string redirectUrl;
                string urlKeyPrefix = String.Empty;
                var item = (IContent)this.NewsManager.GetItemFromUrl(typeof(NewsItem), itemUrl, true, out redirectUrl);
                if (item != null)
                {
                    if (!String.IsNullOrEmpty(this.GetUrlParameterString(false)))
                    {
                        var matches = Regex.Matches(this.GetUrlParameterString(false), widgetNameRegularExpression);
                        if ((matches.Count == 1 && matches[0].Groups["urlPrefix"].Value == urlKeyPrefix)
                            || (matches.Count == 0 && String.IsNullOrEmpty(urlKeyPrefix)))
                        {
                            result = (NewsItem)item;
                        }
                    }
                }
            }

            return result;
        }

        #endregion
        #region ICustomWidgetVisualization Members

        public bool IsEmpty
        {
            get;
            protected set;
        }

        public string EmptyLinkText
        {
            get
            {
                return "This widget will list related news items within the same category. <br/>" +
                        "Please click here to set how many news items you would like the widget to display.";
            }
        }

        #endregion
    }
}
