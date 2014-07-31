using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Samples.Common;
using TemplateImporter;

namespace StarterKitWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Telerik.Sitefinity.Abstractions.Bootstrapper.Initializing += new EventHandler<Telerik.Sitefinity.Data.ExecutingEventArgs>(Bootstrapper_Initializing);
        }

        protected void Bootstrapper_Initializing(object sender, Telerik.Sitefinity.Data.ExecutingEventArgs args)
        {
            if (args.CommandName == "RegisterRoutes")
            {
                var virtualPathConfig = Config.Get<VirtualPathSettingsConfig>();
                var jobsModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = "~/SFRealEstate/*",
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = "Telerik.StarterKit.Modules.RealEstate"
                };
                virtualPathConfig.VirtualPaths.Add(jobsModuleVirtualPathConfig);

                SampleUtilities.RegisterModule<TemplateImporterModule>("Template Importer", "This module imports templates from template builder.");
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}