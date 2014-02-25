using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.StarterKit.Modules.Agents.Web.UI.Public.Designers
{
    public class AgentsDesigner : ContentViewDesignerBase
    {
        // Methods
        protected override void AddViews(Dictionary<string, ControlDesignerView> views)
        {
            ListSettingsDesignerView view2 = new ListSettingsDesignerView();
            view2.DesignedMasterViewType = typeof(MasterListView).FullName;
            SingleItemSettingsDesignerView view3 = new SingleItemSettingsDesignerView();
            view3.DesignedDetailViewType = typeof(DetailsView).FullName;
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
