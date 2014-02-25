using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using System.Collections.Specialized;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.StarterKit.Modules.RealEstate.Data.Implementation;
using Telerik.StarterKit.Modules.RealEstate.Web.UI;

namespace Telerik.StarterKit.Modules.RealEstate.Configuration
{
    public class RealEstateConfig : ContentModuleConfigBase
    {

        protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
        {
            providers.Add(new DataProviderSettings(providers)
            {
                Name = "OpenAccessDataProvider",
                Description = "A provider that stores Real Estate items' data in database using OpenAccess ORM.",
                ProviderType = typeof(OpenAccessProvider),
                Parameters = new NameValueCollection() { { "applicationName", "/RealEstate" } }
            });
        }

        protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
        {
            contentViewControls.Add(RealEstateDefinitions.DefineRealEstateBackendContentView(contentViewControls));
            contentViewControls.Add(RealEstateDefinitions.DefineRealEstateFrontendContentView(contentViewControls));
        }
    }
}
