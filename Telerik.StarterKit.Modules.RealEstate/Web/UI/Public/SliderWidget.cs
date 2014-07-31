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

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public
{

    [ControlDesigner(typeof(SliderWidgetDesigner))]
    public class SliderWidget : SimpleView
    {

        #region Constants
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.SliderWidgetTemplate.ascx";
        #endregion

        #region Fields
        private string forRentPageUrl = string.Empty;
        private string forSalePageUrl = string.Empty; 
        #endregion

        #region Properties

        public int NumberOfItemsToShow
        {
            get;
            set;
        }

        public Guid ForRentPageId
        {
            get;
            set;
        }

        public Guid ForSalePageId
        {
            get;
            set;
        }

        public string ForRentPageTitle
        {
            get
            {
                if (this.ForRentPageId.Equals(Guid.Empty))
                {
                    return "Page not selected";
                }

                return App.Prepare().WorkWith().Page(this.ForRentPageId).Get().Title;
            }
        }

        public string ForSalePageTitle
        {
            get
            {
                if (this.ForSalePageId.Equals(Guid.Empty))
                {
                    return "Page not selected";
                }

                return App.Prepare().WorkWith().Page(this.ForSalePageId).Get().Title;
            }
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

        #endregion

        #region Control references

        protected virtual RadListView SlidesList
        {
            get
            {
                return this.Container.GetControl<RadListView>("SlidesList", true);
            }
        }

        protected virtual RadListView CarouselList
        {
            get
            {
                return this.Container.GetControl<RadListView>("CarouselList", true);
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
                this.SlidesList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(SlidesList_ItemDataBound);
                this.CarouselList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(CarouselList_ItemDataBound);
                List<RealEstateItem> dataSource = this.GetRealEstateItems();

                if (dataSource.Count > 0)
                {
                    this.SlidesList.DataSource = dataSource;
                    this.SlidesList.DataBind();

                    this.CarouselList.DataSource = dataSource;
                    this.CarouselList.DataBind();
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
            RealEstateManager mng = new RealEstateManager();
            List<RealEstateItem> items = mng.GetItems()
                .Where(item => item.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
                .OrderByDescending(item => item.DateCreated)
                .Take(this.NumberOfItemsToShow).ToList();

            return items;
        }

        private void SlidesList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            RealEstateItem item = (RealEstateItem)dataItem.DataItem;

            string sliderPhotoUrl = item.GetPhotoUrl(SinglePhotoType.SliderPhoto);
            string sliderThumbnailUrl = item.GetPhotoUrl(SinglePhotoType.SliderThumbnail);
            if (sliderPhotoUrl.IsNullOrEmpty() || sliderThumbnailUrl.IsNullOrEmpty())
            {
                e.Item.Visible = false;
                return;
            }

            bool isForSale = item.IsForSale();
            string detailsPageUrl = this.GetDetailsPageUrl(isForSale);

            HyperLink hlDetails = (HyperLink)e.Item.FindControl("hlDetails");
            hlDetails.NavigateUrl = detailsPageUrl + this.RealEstateManager.GetItemUrl(item);

            Image imgSlider = (Image)e.Item.FindControl("imgSlider");
            imgSlider.AlternateText = item.Title;
            imgSlider.ImageUrl = sliderPhotoUrl;

            Literal ltrAddress = (Literal)e.Item.FindControl("ltrAddress");
            ltrAddress.Text = item.Address;

            Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
            ltrPrice.Text = item.Price.ToString("n2");

            RadListView featuresList = (RadListView)e.Item.FindControl("FeaturesList");
            featuresList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(FeaturesList_ItemDataBound);
            featuresList.DataSource = item.GetTaxons<FlatTaxon>(TaxonType.Features);
            featuresList.DataBind();
        }

        private string GetDetailsPageUrl(bool isForSale)
        {
            if (isForSale)
            {
                if (forSalePageUrl.IsNullOrEmpty() && !this.ForSalePageId.Equals(Guid.Empty))
                {
                    PageNode pageNode = App.WorkWith().Page(this.ForSalePageId).Get();

                    if (pageNode != null)
                    {
                        forSalePageUrl = pageNode.GetFullUrl();
                    }
                }

                return forSalePageUrl;
            }
            else
            {
                if (forRentPageUrl.IsNullOrEmpty() && !this.ForRentPageId.Equals(Guid.Empty))
                {
                    PageNode pageNode = App.WorkWith().Page(this.ForRentPageId).Get();

                    if (pageNode != null)
                    {
                        forRentPageUrl = pageNode.GetFullUrl();
                    }
                }

                return forRentPageUrl;
            }
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

        private void CarouselList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            RealEstateItem item = (RealEstateItem)dataItem.DataItem;

            string sliderPhotoUrl = item.GetPhotoUrl(SinglePhotoType.SliderPhoto);
            string sliderThumbnailUrl = item.GetPhotoUrl(SinglePhotoType.SliderThumbnail);
            if (sliderPhotoUrl.IsNullOrEmpty() || sliderThumbnailUrl.IsNullOrEmpty())
            {
                e.Item.Visible = false;
                return;
            }

            bool isForSale = item.IsForSale();
            string detailsPageUrl = this.GetDetailsPageUrl(isForSale);

            HyperLink hlDetails = (HyperLink)e.Item.FindControl("hlDetails");
            hlDetails.NavigateUrl = detailsPageUrl + this.RealEstateManager.GetItemUrl(item);

            Image imgThumbnail = (Image)e.Item.FindControl("imgThumbnail");
            imgThumbnail.AlternateText = item.Title;
            imgThumbnail.ImageUrl = sliderThumbnailUrl;

            Literal ltrAddress = (Literal)e.Item.FindControl("ltrAddress");
            ltrAddress.Text = item.Address;

            Literal ltrPrice = (Literal)e.Item.FindControl("ltrPrice");
            ltrPrice.Text = item.Price.ToString("n2");
        }

        #endregion

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

        private RealEstateManager realEstateManager;
    }
}
