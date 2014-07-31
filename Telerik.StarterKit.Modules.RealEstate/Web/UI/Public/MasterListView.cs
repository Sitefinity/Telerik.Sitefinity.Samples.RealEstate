using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.StarterKit.Modules.RealEstate.Data;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public
{
    /// <summary>
    /// Represents master view that displays a collection content items as list.
    /// </summary>
    public class MasterListView : ViewBase
    {
        #region Properties
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
                var templateName = base.LayoutTemplateName;
                if (string.IsNullOrEmpty(templateName))
                    return layoutTemplateName;
                return templateName;
            }
        }

        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected RealEstateManager Manager
        {
            get
            {
                if (this.manager == null)
                    this.manager = RealEstateManager.GetManager(this.Host.ControlDefinition.ProviderName);

                return this.manager;
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

        protected PageNode CurrentPage
        {
            get
            {
                if (this.currentPage == null)
                {
                    SiteMapNode currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
                    if (currentNode != null)
                    {
                        Guid currentPageId = new Guid(currentNode.Key);
                        this.currentPage = this.PageManager.GetPageNode(currentPageId);
                    }
                }

                return this.currentPage;
            }
        }
        #endregion

        #region Control References

        /// <summary>
        /// Gets the repeater for Real Estate items list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView ItemsList_Flow
        {
            get
            {
                return this.Container.GetControl<RadListView>("ItemsList_Flow", true);
            }
        }

        /// <summary>
        /// Gets the repeater for Real Estate items list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView ItemsList_Thumb
        {
            get
            {
                return this.Container.GetControl<RadListView>("ItemsList_Thumb", true);
            }
        }

        /// <summary>
        /// Gets the literal control for Title.
        /// </summary>
        /// <value>The literal control.</value>
        protected internal virtual Literal Title
        {
            get
            {
                return this.Container.GetControl<Literal>("Title", true);
            }
        }

        protected internal virtual LinkButton lbnThumbnailView
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnThumbnailView", true);
            }
        }

        protected internal virtual LinkButton lbnListView
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnListView", true);
            }
        }

        protected internal virtual LinkButton lbnItemPerPage_Nine
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnItemPerPage_Nine", true);
            }
        }

        protected internal virtual LinkButton lbnItemPerPage_Eighteen
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnItemPerPage_Eighteen", true);
            }
        }

        protected internal virtual LinkButton lbnItemPerPage_Twentyseven
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnItemPerPage_Twentyseven", true);
            }
        }

        protected internal virtual LinkButton lbnSortBy_Price
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lbnSortBy_Price", true);
            }
        }

        //protected internal virtual LinkButton lbnSortBy_Location
        //{
        //    get
        //    {
        //        return this.Container.GetControl<LinkButton>("lbnSortBy_Location", true);
        //    }
        //}

        /// <summary>
        /// Gets the pager.
        /// </summary>
        /// <value>The pager.</value>
        protected internal virtual Pager Pager
        {
            get
            {
                return this.Container.GetControl<Pager>("pager", true);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                this.AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                base.RenderEndTag(writer);
            }
        }

        /// <summary>
        /// Gets or sets the layout template.
        /// </summary>
        public override ITemplate LayoutTemplate
        {
            get
            {
                if (this.layoutTemplate == null)
                {
                    var tempInfo = new TemplateInfo()
                    {
                        TemplatePath = this.LayoutTemplatePath,
                        TemplateName = this.LayoutTemplateName,
                        TemplateResourceInfo = typeof(MasterListView),
                        ControlType = this.GetType(),
                        Key = TemplateKey
                    };

                    this.layoutTemplate = ControlUtilities.GetTemplate(tempInfo);
                }
                return this.layoutTemplate;
            }
            set
            {
                this.layoutTemplate = value;
            }
        }


        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The controls container.</param>
        /// <param name="definition">The content view definition.</param>
        protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
        {
            if (this.CurrentPage != null)
            {
                this.Title.Text = this.CurrentPage.Title;
            }

            lbnListView.Click += new EventHandler(ViewTypeButtonsClicked);
            lbnThumbnailView.Click += new EventHandler(ViewTypeButtonsClicked);

            lbnItemPerPage_Nine.Click += new EventHandler(ItemPerPageButtonsClicked);
            lbnItemPerPage_Eighteen.Click += new EventHandler(ItemPerPageButtonsClicked);
            lbnItemPerPage_Twentyseven.Click += new EventHandler(ItemPerPageButtonsClicked);

            lbnSortBy_Price.Click += new EventHandler(SortByButtonsClicked);

            if (!Page.IsPostBack)
            {
                this.BindItems();
            }
        }

        private void BindItems()
        {
            isThumbView = this.IsThumbView();
            int itemsPerPage = ItemsPerPage();
            string sortBy = SortBy();
            string sortDir = SortDir();

            lbnItemPerPage_Nine.CssClass = string.Empty;
            lbnItemPerPage_Eighteen.CssClass = string.Empty;
            lbnItemPerPage_Twentyseven.CssClass = string.Empty;

            switch (itemsPerPage)
            {
                case 9:
                    lbnItemPerPage_Nine.CssClass = "current";
                    break;
                case 18:
                    lbnItemPerPage_Eighteen.CssClass = "current";
                    break;
                case 27:
                    lbnItemPerPage_Twentyseven.CssClass = "current";
                    break;
            }

            if (sortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
            {
                if (sortDir.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    lbnSortBy_Price.CssClass = "arrow-down current";
                }
                else
                {
                    lbnSortBy_Price.CssClass = "arrow-up current";
                }
            }

            var masterDefinition = this.MasterViewDefinition as IContentViewMasterDefinition;
            if (masterDefinition != null)
            {
                var query = this.Manager.GetItems();
                if (masterDefinition.AllowUrlQueries.HasValue && masterDefinition.AllowUrlQueries.Value)
                {
                    query = this.EvaluateUrl(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Taxonomy", "", typeof(RealEstateItem), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                int? totalCount = 0;
                int? itemsToSkip = 0;
                if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value)
                {
                    itemsToSkip = this.GetItemsToSkipCount(ItemsPerPage(), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                CultureInfo uiCulture = null;
                if (Config.Get<ResourcesConfig>().Multilingual)
                {
                    uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
                }

                string sortExpression = "Price ";

                if (sortBy.Equals("location", StringComparison.OrdinalIgnoreCase))
                {
                    sortExpression = "Locations ";
                }

                sortExpression += sortDir;

                var filterExpression = DefinitionsHelper.GetFilterExpression(masterDefinition);
                query = Telerik.Sitefinity.Data.DataProviderBase.SetExpressions(
                    query,
                    filterExpression,
                    SortBy() + " " + SortDir(),
                    uiCulture,
                    itemsToSkip,
                    itemsPerPage,
                    ref totalCount);

                this.IsEmptyView = (totalCount == 0);

                if (totalCount == 0)
                {
                    this.ItemsList_Flow.Visible = false;
                    this.ItemsList_Thumb.Visible = false;
                }
                else
                {
                    this.ConfigurePager(totalCount.Value, masterDefinition);
                    this.librariesManager.Provider.SuppressSecurityChecks = true;


                    if (this.IsThumbView())
                    {
                        this.ItemsList_Thumb.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(ItemsListControl_ItemDataBound);
                        this.ItemsList_Thumb.DataSource = query;
                        this.ItemsList_Thumb.Visible = true;
                        this.ItemsList_Flow.Visible = false;

                        lbnThumbnailView.CssClass = "thumbnails current";
                        lbnListView.CssClass = "list";
                    }
                    else
                    {
                        this.ItemsList_Flow.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(ItemsListControl_ItemDataBound);
                        this.ItemsList_Flow.DataSource = query;
                        this.ItemsList_Flow.Visible = true;
                        this.ItemsList_Thumb.Visible = false;

                        lbnListView.CssClass = "list current";
                        lbnThumbnailView.CssClass = "thumbnails";
                    }
                }
            }
        }

        private void ViewTypeButtonsClicked(object sender, EventArgs e)
        {
            LinkButton lbn = sender as LinkButton;
            if (lbn == null)
            {
                return;
            }

            if (lbn.CommandArgument.Equals("thumbs", StringComparison.OrdinalIgnoreCase))
            {
                this.Context.Response.Cookies.Add(
                    new HttpCookie("ThumbView")
                    {
                        Expires = DateTime.Now.AddMonths(6),
                        Value = "true"
                    });

                newViewTypeIsThumbView = true;

                this.BindItems();
            }
            else if (lbn.CommandArgument.Equals("flow", StringComparison.OrdinalIgnoreCase))
            {
                this.Context.Response.Cookies.Add(
                    new HttpCookie("ThumbView")
                    {
                        Expires = DateTime.Now.AddMonths(6),
                        Value = "false"
                    });

                newViewTypeIsThumbView = false;

                this.BindItems();
            }
        }

        private void ItemPerPageButtonsClicked(object sender, EventArgs e)
        {
            LinkButton lbn = sender as LinkButton;
            if (lbn == null)
            {
                return;
            }

            this.Context.Response.Cookies.Add(
                new HttpCookie("ItemsPerPage")
                {
                    Expires = DateTime.Now.AddMonths(6),
                    Value = lbn.CommandArgument
                });

            newItemsPerPage = Convert.ToInt32(lbn.CommandArgument);

            this.BindItems();
        }

        private void SortByButtonsClicked(object sender, EventArgs e)
        {
            LinkButton lbn = sender as LinkButton;
            if (lbn == null)
            {
                return;
            }

            string oldSortBy = SortBy();
            string oldSortDir = SortDir();

            this.Context.Response.Cookies.Add(
                new HttpCookie("SortBy")
                {
                    Expires = DateTime.Now.AddMonths(6),
                    Value = lbn.CommandArgument
                });

            string newSortDirValue = oldSortBy.Equals(lbn.CommandArgument) ? (oldSortDir.Equals("DESC") ? "ASC" : "DESC") : "ASC";

            this.Context.Response.Cookies.Add(
                new HttpCookie("SortDir")
                {
                    Expires = DateTime.Now.AddMonths(6),
                    Value = newSortDirValue
                });

            newSortBy = lbn.CommandArgument;
            newSortDir = newSortDirValue;

            this.BindItems();
        }

        private int ItemsPerPage()
        {
            if (newItemsPerPage.HasValue)
            {
                return newItemsPerPage.Value;
            }

            HttpCookie cookie = this.Context.Request.Cookies["ItemsPerPage"];

            int cookieValue;

            if (cookie != null && int.TryParse(cookie.Value, out cookieValue))
            {
                return cookieValue;
            }

            return 9;
        }

        private string SortBy()
        {
            if (!newSortBy.IsNullOrEmpty())
            {
                return newSortBy;
            }

            HttpCookie cookie = this.Context.Request.Cookies["SortBy"];

            if (cookie != null)
            {
                return cookie.Value;
            }

            return "Price";
        }

        private string SortDir()
        {
            if (!newSortDir.IsNullOrEmpty())
            {
                return newSortDir;
            }

            HttpCookie cookie = this.Context.Request.Cookies["SortDir"];

            if (cookie != null)
            {
                return cookie.Value;
            }

            return "ASC";
        }

        private bool IsThumbView()
        {
            if (newViewTypeIsThumbView.HasValue)
            {
                return newViewTypeIsThumbView.Value;
            }

            HttpCookie cookie = this.Context.Request.Cookies["ThumbView"];

            if (cookie != null && cookie.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private void ItemsListControl_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RealEstateItem item = ((RadListViewDataItem)e.Item).DataItem as RealEstateItem;
            if (item == null)
            {
                return;
            }

            System.Web.UI.WebControls.Image photo = (System.Web.UI.WebControls.Image)e.Item.FindControl("Photo");

            if (isThumbView)
            {
                photo.ImageUrl = item.GetPhotoUrl(SinglePhotoType.ThumbnailListPhoto);
            }
            else
            {
                photo.ImageUrl = item.GetPhotoUrl(SinglePhotoType.FlowListPhoto);
            }

            RadListView FeaturesList = (RadListView)e.Item.FindControl("FeaturesList");
            FeaturesList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(FeaturesList_ItemDataBound);
            FeaturesList.DataSource = item.GetTaxons<FlatTaxon>(TaxonType.Features);
            FeaturesList.DataBind();
        }

        private void FeaturesList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            FlatTaxon feature = (FlatTaxon)dataItem.DataItem;
            Literal ltrFeature = (Literal)e.Item.FindControl("ltrFeature");
            ltrFeature.Text = feature.Title;
        }

        /// <summary>
        /// Configures the pager.
        /// </summary>
        /// <param name="vrtualItemCount">The virtual item count.</param>
        /// <param name="masterDefinition">The master definition.</param>
        protected virtual void ConfigurePager(int virtualItemCount, IContentViewMasterDefinition masterDefinition)
        {
            if (masterDefinition.AllowPaging.HasValue &&
                masterDefinition.AllowPaging.Value &&
                ItemsPerPage() > 0)
            {
                this.Pager.VirtualItemCount = virtualItemCount;
                this.Pager.PageSize = ItemsPerPage();
                this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
            }
            else
            {
                this.Pager.Visible = false;
            }
        }
        #endregion

        #region Private fields

        private RealEstateManager manager;
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.MasterListView.ascx";
        private ITemplate layoutTemplate;
        private Telerik.Sitefinity.Modules.Libraries.LibrariesManager librariesManager = new Sitefinity.Modules.Libraries.LibrariesManager();
        private bool isThumbView;
        private bool? newViewTypeIsThumbView;
        private int? newItemsPerPage;
        private string newSortBy;
        private string newSortDir;
        private PageManager pageManager;
        private PageNode currentPage;
        #endregion
    }
}
