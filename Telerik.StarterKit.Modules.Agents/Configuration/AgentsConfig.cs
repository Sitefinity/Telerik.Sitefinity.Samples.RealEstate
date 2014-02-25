using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using System.Collections.Specialized;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.StarterKit.Modules.Agents.Data.Implementation;
using Telerik.StarterKit.Modules.Agents.Web.UI;

namespace Telerik.StarterKit.Modules.Agents.Configuration
{
    public class AgentsConfig : ContentModuleConfigBase
    {

        protected override void InitializeDefaultProviders(ConfigElementDictionary<string, DataProviderSettings> providers)
        {
            providers.Add(new DataProviderSettings(providers)
            {
                Name = "OpenAccessDataProvider",
                Description = "A provider that stores agents' data in database using OpenAccess ORM.",
                ProviderType = typeof(OpenAccessProvider),
                Parameters = new NameValueCollection() { { "applicationName", "/Agents" } }
            });
        }

        protected override void InitializeDefaultViews(ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
        {
            contentViewControls.Add(AgentsDefinitions.DefineAgentsBackendContentView(contentViewControls));
            contentViewControls.Add(AgentsDefinitions.DefineAgentsFrontendContentView(contentViewControls));
        }
    }
}
