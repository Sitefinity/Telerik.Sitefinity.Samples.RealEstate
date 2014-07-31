using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Web.UI;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity;
using Telerik.StarterKit.Modules.Agents.Data;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.StarterKit.Modules.RealEstate.Data;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public
{
    /// <summary>
    /// Represents a view that displays detailed information about a specified content item
    /// </summary>
    public class DetailsView : ViewBase
    {
        private const string AGENTS_PAGE = "~/Agents";

        #region Properties
        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected AgentsManager AgentsManager
        {
            get
            {
                if (this.agentsManager == null)
                    this.agentsManager = AgentsManager.GetManager();

                return this.agentsManager;
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
                var templateName = base.LayoutTemplateName;
                if (string.IsNullOrEmpty(templateName))
                    return layoutTemplateName;
                return templateName;
            }
        }

        /// <summary>
        /// Gets the repeater for news list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView DetailsViewControl
        {
            get
            {
                return this.Container.GetControl<RadListView>("DetailsView", true);
            }
        } 
        #endregion

        #region Overridden methods

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
                        TemplateResourceInfo = typeof(DetailsView),
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
            var detailDefinition = definition as IContentViewDetailDefinition;
            if (detailDefinition != null)
            {
                var realEstateView = (RealEstateView)this.Host;

                var item = realEstateView.DetailItem as RealEstateItem;
                if (item == null)
                {
                    if (this.IsDesignMode())
                    {
                        this.Controls.Clear();
                        this.Controls.Add(new LiteralControl("A Real Estate item was not selected or has been deleted. Please select another one."));
                    }
                    return;
                }
                else
                {
                    this.librariesManager.Provider.SuppressSecurityChecks = true;
                    this.DetailsViewControl.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(DetailsView_ItemDataBound);
                    this.DetailsViewControl.DataSource = new RealEstateItem[] { item };

                    string gMapsKey = WebConfigurationManager.AppSettings["GoogleMapsKey"];
                    this.Page.ClientScript.RegisterClientScriptInclude("gmaps", string.Format("http://maps.google.com/maps?file=api&v=2&key={0}", gMapsKey));
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the ItemDataBound event of the DetailsView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadListViewItemEventArgs"/> instance containing the event data.</param>
        private void DetailsView_ItemDataBound(object sender, RadListViewItemEventArgs e)
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

            #region Contact Our Agent
            RadTabStrip tabStrip = (RadTabStrip)e.Item.FindControl("tabStrip");
            RadMultiPage multiPage = (RadMultiPage)e.Item.FindControl("multiPage");
            RadTab tabContactOurAgent = tabStrip.Tabs[5];

            if (tabContactOurAgent != null && !item.AgentId.Equals(Guid.Empty))
            {
                AgentItem agent = this.AgentsManager.GetAgent(item.AgentId);
                if (agent != null)
                {
                    tabContactOurAgent.NavigateUrl = AGENTS_PAGE + this.AgentsManager.GetItemUrl(agent);
                }
                else
                {
                    tabContactOurAgent.Visible = false;
                    multiPage.PageViews[5].Visible = false;
                }
            }
            else
            {
                tabContactOurAgent.Visible = false;
                multiPage.PageViews[5].Visible = false;
            }
            #endregion

            RadListView rlvOverview = (RadListView)e.Item.FindControl("rlvOverview");
            List<Photo> overviewTabPhotos = item.GetPhotos(MultiplePhotoType.OverviewTabPhoto);
            rlvOverview.DataSource = overviewTabPhotos;
            rlvOverview.DataBind();
            tabStrip.Tabs[0].Visible = (overviewTabPhotos.Count > 0);
            multiPage.PageViews[0].Visible = tabStrip.Tabs[0].Visible;

            RadListView rlvPhotos = (RadListView)e.Item.FindControl("rlvPhotos");
            List<Photo> photosTabPhotos = item.GetPhotos(MultiplePhotoType.PhotosTabPhoto);
            rlvPhotos.DataSource = photosTabPhotos;
            rlvPhotos.DataBind();
            tabStrip.Tabs[1].Visible = (photosTabPhotos.Count > 0);
            multiPage.PageViews[1].Visible = tabStrip.Tabs[1].Visible;

            RadListView rlvPanaromicView = (RadListView)e.Item.FindControl("rlvPanaromicView");
            List<Photo> panaromicViewTabPhotos = item.GetPhotos(MultiplePhotoType.PanaromicViewTabPhoto);
            rlvPanaromicView.DataSource = panaromicViewTabPhotos;
            rlvPanaromicView.DataBind();
            tabStrip.Tabs[2].Visible = (panaromicViewTabPhotos.Count > 0);
            multiPage.PageViews[2].Visible = tabStrip.Tabs[2].Visible;

            RadListView rlvFloorPlan = (RadListView)e.Item.FindControl("rlvFloorPlan");
            List<Photo> floorPlanTabPhotos = item.GetPhotos(MultiplePhotoType.FloorPlanTabPhoto);
            rlvFloorPlan.DataSource = floorPlanTabPhotos;
            rlvFloorPlan.DataBind();
            tabStrip.Tabs[3].Visible = (floorPlanTabPhotos.Count > 0);
            multiPage.PageViews[3].Visible = tabStrip.Tabs[3].Visible;

            if (item.Latitude <= 0 || item.Longitude <= 0)
            {
                tabStrip.Tabs[4].Visible = false;
                multiPage.PageViews[4].Visible = false;
            }

            for (int i = 0; i < tabStrip.Tabs.Count; i++)
            {
                if (tabStrip.Tabs[i].Visible)
                {
                    tabStrip.SelectedIndex = i;
                    multiPage.SelectedIndex = i;

                    this.Page.ClientScript.RegisterClientScriptBlock(
                        this.GetType(), "initialTabStripId",
                        string.Format("var initialTabStripId = '{0}';", multiPage.PageViews[i].ClientID), true);

                    break;
                }
            }

            Panel pnlPrice = (Panel)e.Item.FindControl("pnlPrice");
            pnlPrice.Controls.Clear();
            pnlPrice.Controls.Add(new Literal() { Text = item.Price.ToString("n2") });

            if (!item.Description.Value.IsNullOrEmpty())
            {
                PlaceHolder phDescription = (PlaceHolder)e.Item.FindControl("phDescription");
                phDescription.Visible = true;
            }

            #region Location
            FlatTaxon locationTaxon = item.GetTaxon<FlatTaxon>(TaxonType.Locations);
            if (locationTaxon != null)
            {
                Literal ltrLocation = (Literal)e.Item.FindControl("ltrLocation");

                ltrLocation.Text = locationTaxon.Title;
            }
            #endregion

            #region Item Type
            FlatTaxon itemTypeTaxon = item.GetTaxon<FlatTaxon>(TaxonType.Types);
            if (itemTypeTaxon != null)
            {
                PlaceHolder phItemType = (PlaceHolder)e.Item.FindControl("phItemType");
                Literal ltrItemType = (Literal)e.Item.FindControl("ltrItemType");

                ltrItemType.Text = itemTypeTaxon.Title;
                phItemType.Visible = true;
            } 
            #endregion

            #region Housing
            if (!item.Housing.IsNullOrEmpty())
            {
                PlaceHolder phHousing = (PlaceHolder)e.Item.FindControl("phHousing");
                Literal ltrHousing = (Literal)e.Item.FindControl("ltrHousing");
                ltrHousing.Text = string.Format("{0} m<sup>2</sup>", item.Housing);
                phHousing.Visible = true;
            }
            #endregion

            #region Rooms
            if (!item.NumberOfRooms.IsNullOrEmpty())
            {
                PlaceHolder phRooms = (PlaceHolder)e.Item.FindControl("phRooms");
                Literal ltrRooms = (Literal)e.Item.FindControl("ltrRooms");
                ltrRooms.Text = item.NumberOfRooms;
                phRooms.Visible = true;
            }
            #endregion

            #region Floors
            if (!item.NumberOfFloors.IsNullOrEmpty())
            {
                PlaceHolder phFloors = (PlaceHolder)e.Item.FindControl("phFloors");
                Literal ltrFloors = (Literal)e.Item.FindControl("ltrFloors");
                ltrFloors.Text = item.NumberOfFloors;
                phFloors.Visible = true;
            }
            #endregion

            #region Built
            if (!item.YearBuilt.IsNullOrEmpty())
            {
                PlaceHolder phBuilt = (PlaceHolder)e.Item.FindControl("phBuilt");
                Literal ltrBuilt = (Literal)e.Item.FindControl("ltrBuilt");
                ltrBuilt.Text = item.YearBuilt;
                phBuilt.Visible = true;
            }
            #endregion

            #region Payment
            if (item.Payment > 0)
            {
                PlaceHolder phPayment = (PlaceHolder)e.Item.FindControl("phPayment");
                Literal ltrPayment = (Literal)e.Item.FindControl("ltrPayment");
                ltrPayment.Text = item.Payment.ToString("n2");
                phPayment.Visible = true;
            }
            #endregion

            #region MonthlyRate
            if (item.MonthlyRate > 0)
            {
                PlaceHolder phMonthlyRate = (PlaceHolder)e.Item.FindControl("phMonthlyRate");
                Literal ltrMonthlyRate = (Literal)e.Item.FindControl("ltrMonthlyRate");
                ltrMonthlyRate.Text = item.MonthlyRate.ToString("n2");
                phMonthlyRate.Visible = true;
            }
            #endregion

            #region Net
            if (item.Net > 0)
            {
                PlaceHolder phNet = (PlaceHolder)e.Item.FindControl("phNet");
                Literal ltrNet = (Literal)e.Item.FindControl("ltrNet");
                ltrNet.Text = item.Net.ToString("n2");
                phNet.Visible = true;
            }
            #endregion

            #region PriceSquareMeter
            if (item.PriceSquareMeter > 0)
            {
                PlaceHolder phPriceSquareMeter = (PlaceHolder)e.Item.FindControl("phPriceSquareMeter");
                Literal ltrPriceSquareMeter = (Literal)e.Item.FindControl("ltrPriceSquareMeter");
                ltrPriceSquareMeter.Text = item.PriceSquareMeter.ToString("n2");
                phPriceSquareMeter.Visible = true;
            }
            #endregion

            RadListView FeaturesList = (RadListView)e.Item.FindControl("FeaturesList");
            FeaturesList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(FeaturesList_ItemDataBound);
            FeaturesList.DataSource = item.GetTaxons<FlatTaxon>(TaxonType.Features);
            FeaturesList.DataBind();

            RadListView RoomsList = (RadListView)e.Item.FindControl("RoomsList");
            RoomsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(RoomsList_ItemDataBound);
            RoomsList.DataSource = item.GetTaxons<FlatTaxon>(TaxonType.Rooms);
            RoomsList.DataBind();

            #region Agent
            if (!item.AgentId.Equals(Guid.Empty))
            {
                AgentItem agent = this.AgentsManager.GetAgent(item.AgentId);

                if (agent != null)
                {
                    PlaceHolder phAgent = (PlaceHolder)e.Item.FindControl("phAgent");
                    phAgent.Visible = true;

                    Literal ltrAgentName = (Literal)e.Item.FindControl("ltrAgentName");
                    ltrAgentName.Text = agent.Title;


                    #region Address
                    if (!agent.Address.IsNullOrEmpty())
                    {
                        PlaceHolder phAgentAddress = (PlaceHolder)e.Item.FindControl("phAgentAddress");
                        Literal ltrAgentAddress = (Literal)e.Item.FindControl("ltrAgentAddress");
                        ltrAgentAddress.Text = agent.Address;
                        phAgentAddress.Visible = true;
                    }
                    #endregion

                    #region PhoneNumber
                    if (!agent.PhoneNumber.IsNullOrEmpty())
                    {
                        PlaceHolder phAgentPhoneNumber = (PlaceHolder)e.Item.FindControl("phAgentPhoneNumber");
                        Literal ltrAgentPhoneNumber = (Literal)e.Item.FindControl("ltrAgentPhoneNumber");
                        ltrAgentPhoneNumber.Text = agent.PhoneNumber;
                        phAgentPhoneNumber.Visible = true;
                    }
                    #endregion


                    #region Email
                    if (!agent.Email.IsNullOrEmpty())
                    {
                        PlaceHolder phAgentEmail = (PlaceHolder)e.Item.FindControl("phAgentEmail");
                        HyperLink hlAgentEmail = (HyperLink)e.Item.FindControl("hlAgentEmail");
                        hlAgentEmail.Text = agent.Email;
                        hlAgentEmail.NavigateUrl = tabContactOurAgent.NavigateUrl;
                        phAgentEmail.Visible = true;
                    }
                    #endregion
                }
            }
            #endregion
        }

        #endregion

        #region Helper methods

        private void InitCommentsView(ContentView view)
        {
            if (view != null)
            {
                view.ControlDefinition.ProviderName = this.Host.ControlDefinition.ProviderName;
                var detailItem = this.Host.DetailItem as Telerik.Sitefinity.GenericContent.Model.Content;
                if (detailItem != null)
                {
                    view.DetailItem = detailItem;
                }
            }
        }

        #endregion

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

        private void RoomsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            RadListViewDataItem dataItem = (RadListViewDataItem)e.Item;
            FlatTaxon room = (FlatTaxon)dataItem.DataItem;
            Literal ltrItem = (Literal)e.Item.FindControl("ltrItem");
            ltrItem.Text = room.Title;
        }

        #region Private Fields & Constants

        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.DetailsView.ascx";
        private ITemplate layoutTemplate;
        private Telerik.Sitefinity.Modules.Libraries.LibrariesManager librariesManager = new Sitefinity.Modules.Libraries.LibrariesManager();
        private AgentsManager agentsManager;
        #endregion

        private Control FindControlRecursive(string ID)
        {
            return this.FindControlRecursive(this, ID);
        }

        internal Control FindControlRecursive(Control searcher, string ID)
        {
            //First search the same naming container:
            if (searcher.NamingContainer != null)
            {
                Control searched = searcher.NamingContainer.FindControl(ID);
                if (searched != null)
                {
                    return searched;
                }
            }

            Control root;
            if (searcher.Page.Master != null)
            {
                root = searcher.Page.Master;
            }
            else
            {
                root = searcher.Page;
            }

            Control control = root.FindControl(ID);
            if (control != null)
            {
                return control;
            }

            control = searcher.Page.FindControl(ID);
            if (control != null)
            {
                return control;
            }

            if (searcher.UniqueID == ID || searcher.ClientID == ID)
            {
                return searcher;
            }

            return FindControlRecursive(ID, root);
        }

        private Control FindControlRecursive(string ID, Control root)
        {
            Control control = null;

            if (root is System.Web.UI.WebControls.DataBoundControl && !root.Visible)
                return control;

            foreach (Control ctrl in root.Controls)
            {
                if (ctrl is INamingContainer && ctrl.FindControl(ID) != null)
                {
                    control = ctrl.FindControl(ID);
                    break;
                }
                else if (ctrl.HasControls())
                {
                    control = FindControlRecursive(ID, ctrl);
                    if (control != null && (control.UniqueID == ID || control.ID == ID))
                    {
                        break;
                    }
                }
            }
            return control;
        }
    }
}
