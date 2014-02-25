using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers
{
    public class RealEstateDesigner : ContentViewDesignerBase
    {
        // Methods
        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {

            RealEstateResources resources = Res.Get<RealEstateResources>();
            RealEstateViewContentSelectorsDesignerView view = new RealEstateViewContentSelectorsDesignerView
            {
                ContentTitleText = resources.WhichItemsToDisplay,
                ChooseAllText = resources.AllPublishedItems,
                ChooseSingleText = resources.OneParticularItemOnly,
                ChooseSimpleFilterText = resources.SelectionOfItems,
                ChooseAdvancedFilterText = resources.AdvancedSelection,
                NoContentToSelectText = resources.NoItemsHaveBeenCreatedYet
            };
            view.ContentSelector.TitleText = resources.SelectItems;
            view.ContentSelector.ItemType = typeof(MasterListView).FullName;
            

            ListSettingsDesignerView view2 = new ListSettingsDesignerView
            {
                SortItemsText = resources.SortItems,
                DesignedMasterViewType = typeof(MasterListView).FullName
            };
            SingleItemSettingsDesignerView view3 = new SingleItemSettingsDesignerView
            {
                DesignedDetailViewType = typeof(DetailsView).FullName
            };
            views.Add(view.ViewName, view);
            views.Add(view2.ViewName, view2);
            views.Add(view3.ViewName, view3);

        }


        // Properties
        protected override string ScriptDescriptorTypeName
        {
            get
            {
                return typeof(ContentViewDesignerBase).FullName;
            }
        }

        protected override System.Type ResourcesAssemblyInfo
        {
            get
            {
                return Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
            }
        }

    }
}
