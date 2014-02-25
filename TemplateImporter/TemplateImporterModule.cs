using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Web.UI;
using TemplateImporter.Configuration;
using TemplateImporter.Localization;
using System.Text;
using Telerik.Sitefinity.Abstractions.VirtualPath;


namespace TemplateImporter
{
    public class TemplateImporterModule : ModuleBase
    {
        public const string ModuleName = "Template Importer";

        public static readonly Guid TemplateImporterPageGroupID = new Guid("13FA3CB0-6F00-4DFB-A534-000000000001");
        public static readonly Guid TemplateImporterModuleLandingPage = new Guid("A52C36E1-3D29-4F39-BB8D-BB1F064E556A");
        public static string TemplateImporterVirtualPath = "~/SFTemplateImporter/";

        public override Type[] Managers
        {
            get
            {
                return null;
            }
        }

        public override Guid LandingPageId
        {
            get
            {
                return TemplateImporterModule.TemplateImporterModuleLandingPage;
            }
        }

        public Guid SubPageId
        {
            get
            {
                return TemplateImporterModule.TemplateImporterPageGroupID;
            }
        }

        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);
            Config.RegisterSection<TemplateImporterConfig>();
            Res.RegisterResource<TemplateImporterResources>();

            TypeResolutionService.RegisterAssembly(typeof(TemplateImporterModule).Assembly.GetName());
        }

        public override void Install(SiteInitializer initializer)
        {
            this.InstallCustomVirtualPaths(initializer);
            this.InstallPages(initializer);
        }

        private void InstallCustomVirtualPaths(SiteInitializer initialzer)
        {
            var virtualPathConfig = initialzer.Context.GetConfig<VirtualPathSettingsConfig>();
            ConfigManager.Executed += new EventHandler<Telerik.Sitefinity.Data.ExecutedEventArgs>(this.ConfigManager_Executed);
            var TemplateImporterModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
            {
                VirtualPath = TemplateImporterModule.TemplateImporterVirtualPath + "*",
                ResolverName = "EmbeddedResourceResolver",
                ResourceLocation = "TemplateImporter"
            };
            if (!virtualPathConfig.VirtualPaths.ContainsKey(TemplateImporterModule.TemplateImporterVirtualPath + "*"))
                virtualPathConfig.VirtualPaths.Add(TemplateImporterModuleVirtualPathConfig);
        }

        private void ConfigManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "SaveSection")
            {
                var section = args.CommandArguments as VirtualPathSettingsConfig;

                if (section != null)
                {

                    // Reset the Virtual path manager, whenever the section of the VirtualPathSettingsConfig is saved.
                    // This is needed so that the prefixes for templates in our module assembly are taken into account.
                    VirtualPathManager.Reset();

                }
            }
        }

        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        { }

        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<TemplateImporterConfig>();
        }

        protected void InstallPages(SiteInitializer initializer)
        {
            var pageManager = initializer.PageManager;
            var moduleNode = pageManager.GetPageNode(SiteInitializer.DesignNodeId);

            PageNode TemplateImporterNode = pageManager.GetPageNodes().Where(p => p.Id == TemplateImporterModule.TemplateImporterPageGroupID).SingleOrDefault();
            if (TemplateImporterNode == null)
            {
                TemplateImporterNode = initializer.CreatePageNode(TemplateImporterModule.TemplateImporterPageGroupID, moduleNode, NodeType.Group);

                TemplateImporterNode.Name = TemplateImporterModule.ModuleName;
                TemplateImporterNode.ShowInNavigation = true;
                TemplateImporterNode.Attributes["ModuleName"] = TemplateImporterModule.ModuleName;
                TemplateImporterNode.Title = TemplateImporterModule.ModuleName;
                TemplateImporterNode.UrlName = TemplateImporterModule.ModuleName;
                TemplateImporterNode.Description = "Module for importing Template packages";
            }

            var landingPage = pageManager.GetPageNodes().SingleOrDefault(p => p.Id == this.LandingPageId);

            if (landingPage == null)
            {
                var pageInfo = new PageDataElement()
                {
                    PageId = this.LandingPageId,
                    Name = "TemplateImporter",
                    MenuName = "TemplateImporter Module",
                    UrlName = "TemplateImporter",
                    Description = "TemplateImporterLandingPageDescription",
                    HtmlTitle = "Template Importer",
                    //ResourceClassId = typeof(TemplateImporterResources).Name,
                    IncludeScriptManager = true,
                    ShowInNavigation = false,
                    EnableViewState = false,
                    TemplateName = SiteInitializer.BackendTemplateName,
                };

                pageInfo.Parameters["ModuleName"] = TemplateImporterModule.ModuleName;
              
                TemplateImporterServerControl control = new TemplateImporterServerControl();
                              
                Control[] pageControls = new Control[] { 
                   control
                };
                
                initializer.CreatePageFromConfiguration(pageInfo, TemplateImporterNode, pageControls);
            }
        }
    }
}