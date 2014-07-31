using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.StarterKit.Modules.RealEstate.Data;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers;
using Telerik.Web.UI;
using Telerik.Sitefinity.Taxonomies;
using System.Web;
using Telerik.Sitefinity.Model;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Web;
using Telerik.OpenAccess;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public
{

    [ControlDesigner(typeof(SimilarPropertiesWidgetDesigner))]
    public class SimilarPropertiesWidget : SimpleView
    {

        #region Constants
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.SimilarPropertiesWidgetTemplate.ascx";
        private const string widgetNameRegularExpression = @"/!(?<urlPrefix>\w+)/.*";
        #endregion

        #region Properties

        public int NumberOfItemsToShow
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

        protected RealEstateManager RealEstateManager
        {
            get
            {
                if (this.realEstateManager == null)
                {
                    this.realEstateManager = RealEstateManager.GetManager();
                }
                return this.realEstateManager;
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

        protected RealEstateItem CurrentItem
        {
            get
            {
                if (this.currentItem == null)
                {
                    this.currentItem = this.ResolveDetailItemFromUrl();
                }

                return this.currentItem;
            }
        }

        #endregion

        #region Control references

        protected virtual RadListView ItemsList
        {
            get
            {
                return this.Container.GetControl<RadListView>("ItemsList", true);
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
            if (this.NumberOfItemsToShow > 0)
            {
                this.ItemsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(ItemsList_ItemDataBound);
                List<RealEstateItem> dataSource = this.GetRealEstateItems();

                if (dataSource.Count > 0)
                {
                    this.ItemsList.DataSource = dataSource;
                    this.ItemsList.DataBind();
                }
                else
                {
                    if (!this.IsBackend() && !this.IsDesignMode())
                    {
                        this.Visible = false;
                    }
                }
            }
            
        }

        private List<RealEstateItem> GetRealEstateItems()
        {
            if (this.CurrentItem == null)
            {
                return new List<RealEstateItem>(0);
            }

            List<RealEstateItem> dataSource = new List<RealEstateItem>(this.NumberOfItemsToShow);

            TrackedList<Guid> categoryIds = (TrackedList<Guid>)this.CurrentItem.GetValue("Category");
            foreach (Guid id in categoryIds)
            {
                var items = this.RealEstateManager.GetItems()
                                .Where(nI => nI.GetValue<TrackedList<Guid>>("Category").Contains(id));

                var filter = items
                .Distinct()
                .Where(i => i.Status == ContentLifecycleStatus.Live && i.Id != this.CurrentItem.Id);

                FlatTaxon itemType = this.CurrentItem.GetTaxon<FlatTaxon>(TaxonType.Types);
                if (itemType != null)
                {
                    filter = filter.Where(i => i.GetValue<TrackedList<Guid>>("Types").Contains(itemType.Id));
                }

                FlatTaxon location = this.CurrentItem.GetTaxon<FlatTaxon>(TaxonType.Locations);
                if (location != null)
                {
                    filter = filter.Where(i => i.GetValue<TrackedList<Guid>>("Locations").Contains(location.Id));
                }

                return filter.OrderByDescending(n => n.PublicationDate)
                .Take(this.NumberOfItemsToShow)
                .ToList();


            }

            return new List<RealEstateItem>();
        }

        private void ItemsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            RealEstateItem item = (RealEstateItem)dataItem.DataItem;

            string sliderThumbnailUrl = item.GetPhotoUrl(SinglePhotoType.SliderThumbnail);
            if (sliderThumbnailUrl.IsNullOrEmpty())
            {
                e.Item.Visible = false;
                return;
            }

            HyperLink hlPhoto = (HyperLink)e.Item.FindControl("hlPhoto");
            hlPhoto.NavigateUrl = this.CurrentPageUrl + this.RealEstateManager.GetItemUrl(item);

            HyperLink hlDetails = (HyperLink)e.Item.FindControl("hlDetails");
            hlDetails.NavigateUrl = hlPhoto.NavigateUrl;
            hlDetails.Text = item.Title;

            Image imgThumbnail = (Image)e.Item.FindControl("imgThumbnail");
            imgThumbnail.AlternateText = item.Title;
            imgThumbnail.ImageUrl = sliderThumbnailUrl;
        }

        #endregion

        private RealEstateItem ResolveDetailItemFromUrl()
        {
            RealEstateItem result = null;
            var itemUrl = this.GetUrlParameterString(true);
            if (itemUrl != null)
            {
                string redirectUrl;
                string urlKeyPrefix = String.Empty;
                var item = (IContent)this.RealEstateManager.GetItemFromUrl(typeof(RealEstateItem), itemUrl, true, out redirectUrl);
                if (item != null)
                {
                    if (!String.IsNullOrEmpty(this.GetUrlParameterString(false)))
                    {
                        var matches = Regex.Matches(this.GetUrlParameterString(false), widgetNameRegularExpression);
                        if ((matches.Count == 1 && matches[0].Groups["urlPrefix"].Value == urlKeyPrefix)
                            || (matches.Count == 0 && String.IsNullOrEmpty(urlKeyPrefix)))
                        {
                            result = (RealEstateItem)item;
                        }
                    }
                }
            }

            return result;
        }

        private RealEstateManager realEstateManager;
        private PageManager pageManager;
        private TaxonomyManager taxonomyManager;
        private PageNode currentPage;
        private string currentPageUrl;
        private RealEstateItem currentItem;
    }
}
