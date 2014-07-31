using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI;
using System.Web.UI;
using Telerik.Sitefinity.Web.Utilities;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Web.UI;
using System.Web;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Model;
using System.Text.RegularExpressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies.Model;
using System.Threading;

namespace Telerik.StarterKit.Widgets.Events
{
    [ControlDesigner(typeof(RelatedEventsDesigner))]
    public class RelatedEvents : SimpleView, ICustomWidgetVisualization
    {
        private EventsManager eventsManager;
        private PageManager pageManager;
        private TaxonomyManager taxonomyManager;
        private PageNode currentPage;
        private string currentPageUrl;
        private Event currentEventItem;

        #region Constants

        private const string layoutTemplateName = "Telerik.StarterKit.Widgets.Events.Resources.Views.RelatedEventsTemplate.ascx";
        private const string widgetNameRegularExpression = @"/!(?<urlPrefix>\w+)/.*";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of Event items to be shown.
        /// </summary>
        public int NumberOfEventsToShow
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

        protected EventsManager EventsManager
        {
            get
            {
                if (this.eventsManager == null)
                {
                    this.eventsManager = EventsManager.GetManager();
                }
                return this.eventsManager;
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
                    this.currentPageUrl = this.CurrentPage.GetFullUrl(Thread.CurrentThread.CurrentCulture, false);
                }
                return this.currentPageUrl;
            }
        }

        protected Event CurrentEventItem
        {
            get
            {
                if (this.currentEventItem == null)
                {
                    this.currentEventItem = this.ResolveDetailItemFromUrl();
                }

                return this.currentEventItem;
            }
        }


        #endregion

        #region Control references

        /// <summary>
        /// Gets the list control.
        /// </summary>
        /// <value>The instance of the list control.</value>
        protected virtual RadListView EventsList
        {
            get
            {
                return this.Container.GetControl<RadListView>("EventsList", true);
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
            if (this.NumberOfEventsToShow > 0)
            {
                this.EventsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(EventsList_ItemDataBound);
                List<Event> dataSource = this.GetEventsListDataSource();

                if (dataSource.Count > 0)
                {
                    this.EventsList.DataSource = dataSource;
                    this.EventsList.DataBind();
                }
                else
                {
                    this.Visible = false;
                }
                //this.ContentHtml.Text = LinkParser.ResolveLinks(this.Html, DynamicLinksParser.GetContentUrl, null, false);
            }

            this.IsEmpty = this.NumberOfEventsToShow < 1;
        }

        private void EventsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            Event eventItem = (Event)dataItem.DataItem;
            HyperLink hlEvents = (HyperLink)e.Item.FindControl("hlEvent");
            hlEvents.Text = eventItem.Title;
            hlEvents.NavigateUrl = this.CurrentPageUrl + this.EventsManager.GetItemUrl(eventItem);
        }

        private List<Event> GetEventsListDataSource()
        {
            if (this.CurrentEventItem == null)
            {
                return new List<Event>(0);
            }

            List<Event> dataSource = new List<Event>(this.NumberOfEventsToShow);
            List<Event> allEventsWithSameCategories = new List<Event>();

            TrackedList<Guid> categoryIds = (TrackedList<Guid>)this.CurrentEventItem.GetValue("Category");
            foreach (Guid id in categoryIds)
            {
                var items = App.WorkWith()
                                .Events()
                                .Where(eI => eI.GetValue<TrackedList<Guid>>("Category").Contains(id)).Get().ToList();
   
                allEventsWithSameCategories.AddRange(items);
            }

            return allEventsWithSameCategories
                .Distinct()
                .Where(e => e.Status == ContentLifecycleStatus.Live && e.Id != this.CurrentEventItem.Id)
                .OrderByDescending(e => e.PublicationDate)
                .Take(this.NumberOfEventsToShow)
                .ToList();
        }

        private Event ResolveDetailItemFromUrl()
        {
            Event result = null;
            var itemUrl = this.GetUrlParameterString(true);
            if (itemUrl != null)
            {
                string redirectUrl;
                string urlKeyPrefix = String.Empty;
                var item = (IContent)this.EventsManager.GetItemFromUrl(typeof(Event), itemUrl, true, out redirectUrl);
                if (item != null)
                {
                    if (!String.IsNullOrEmpty(this.GetUrlParameterString(false)))
                    {
                        var matches = Regex.Matches(this.GetUrlParameterString(false), widgetNameRegularExpression);
                        if ((matches.Count == 1 && matches[0].Groups["urlPrefix"].Value == urlKeyPrefix)
                            || (matches.Count == 0 && String.IsNullOrEmpty(urlKeyPrefix)))
                        {
                            result = (Event)item;
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
                return "This widget will list related event items within the same category. <br/>" +
                        "Please click here to set how many event items you would like the widget to display.";
            }
        }

        #endregion
    }
}
