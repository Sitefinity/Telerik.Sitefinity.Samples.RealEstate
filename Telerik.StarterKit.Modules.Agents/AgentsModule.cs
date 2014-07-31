using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.StarterKit.Modules.Agents.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Abstractions;
using Telerik.StarterKit.Modules.Agents.Web.UI.Public;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.StarterKit.Modules.Agents.Data;
using Telerik.StarterKit.Modules.Agents.Web.UI;
using Telerik.Sitefinity.Workflow.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.StarterKit.Modules.Agents.Web.Services;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.StarterKit.Modules.Agents
{
    /// <summary>
    /// Agents module - API entry point
    /// </summary>
    public class AgentsModule : ContentModuleBase
    {
        #region Properties

        /// <summary>
        /// Gets the CLR types of all data managers provided by this module.
        /// </summary>
        /// <value>An array of <see cref="Type"/> objects.</value>
        public override Type[] Managers
        {
            get { return managerTypes; }
        }

        /// <summary>
        /// Gets the identity of the home (landing) page for the Agents module.
        /// </summary>
        /// <value>The landing page id.</value>
        public override Guid LandingPageId
        {
            get
            {
                return AgentsModule.HomePageId;
            }
        }

        #endregion

        #region Module Initialization

        /// <summary>
        /// Initializes the service with specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            Config.RegisterSection<AgentsConfig>();
            Res.RegisterResource<AgentsResources>();
            SystemManager.RegisterWebService(typeof(AgentsBackendService), "Sitefinity/Services/Content/Agents.svc");
            SystemManager.RegisterWebService(typeof(MailerBackendService), "Sitefinity/Services/Content/Mailer.svc");

            //register templatable controls
            ControlTemplates.RegisterTemplatableControl(typeof(MasterListView), typeof(AgentItem));
            ControlTemplates.RegisterTemplatableControl(typeof(DetailsView), typeof(AgentItem));
        }

        #endregion

        #region Module Installation

        /// <summary>
        /// Installs this module in Sitefinity system for the first time.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        public override void Install(SiteInitializer initializer)
        {
            base.Install(initializer);

            IModule agentsModule;
            SystemManager.ApplicationModules.TryGetValue(AgentsModule.ModuleName, out agentsModule);
            initializer.RegisterControlTemplate(MasterListView.layoutTemplateName, typeof(MasterListView).FullName, AgentsDefinitions.FrontendDefaultListViewName);
            initializer.RegisterControlTemplate(DetailsView.layoutTemplateName, typeof(DetailsView).FullName, AgentsDefinitions.FrontendDefaultDetailViewName);

            #region Agents permissions set

            InstallCustomPermissions(initializer);
            InstallCustomWorkflow(initializer);

            #endregion


            //initializer.Context.SaveMetaData(true);
        }

        private void InstallCustomWorkflow(SiteInitializer initializer)
        {
            WorkflowConfig workflowConfig = initializer.Context.GetConfig<WorkflowConfig>();

            if (!workflowConfig.Workflows.ContainsKey(typeof(AgentItem).FullName))
            {
                workflowConfig.Workflows.Add(
                new WorkflowElement(workflowConfig.Workflows)
                {
                    ContentType = typeof(AgentItem).FullName,
                    ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof(AgentItem)),
                    Title = "ModuleTitle",
                    ResourceClassId = typeof(AgentsResources).Name
                });
            }

            //initializer.InstallEmbeddedVirtualPath(WorkflowRelativeUrl, WorkflowEmbeddedPath, typeof(AgentsModule).Assembly);

        }

        private void InstallCustomPermissions(SiteInitializer initializer)
        {
            SecurityConfig securityConfig = initializer.Context.GetConfig<SecurityConfig>();
            ConfigElementDictionary<string, Permission> permissionSetConfig = securityConfig.Permissions;
            ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig> customPermissionsDisplaySettings = securityConfig.CustomPermissionsDisplaySettings;

            //Add the set
            if (!permissionSetConfig.ContainsKey(AgentsConstants.Security.PermissionSetName))
            {
                var agentsPermissionSet = new Permission(permissionSetConfig)
                {
                    Name = AgentsConstants.Security.PermissionSetName,
                    Title = "AgentsPermissions",
                    Description = "AgentsPermissionsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                permissionSetConfig.Add(agentsPermissionSet);

                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.View,
                    Type = SecurityActionTypes.View,
                    Title = "ViewAgents",
                    Description = "ViewAgentsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.Create,
                    Type = SecurityActionTypes.Create,
                    Title = "CreateAgents",
                    Description = "CreateAgentsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.Modify,
                    Type = SecurityActionTypes.Modify,
                    Title = "ModifyAgents",
                    Description = "ModifyAgentsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.Delete,
                    Type = SecurityActionTypes.Delete,
                    Title = "DeleteAgents",
                    Description = "DeleteAgentsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.ChangeOwner,
                    Type = SecurityActionTypes.ChangeOwner,
                    Title = "ChangeAgentsOwner",
                    Description = "ChangeAgentsOwnerDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
                agentsPermissionSet.Actions.Add(new SecurityAction(agentsPermissionSet.Actions)
                {
                    Name = AgentsConstants.Security.ChangePermissions,
                    Type = SecurityActionTypes.ChangePermissions,
                    Title = "ChangeAgentsPermissions",
                    Description = "ChangeAgentsPermissionsDescription",
                    ResourceClassId = typeof(AgentsResources).Name
                });
            }

            //Custom UI views
            if (!customPermissionsDisplaySettings.ContainsKey(AgentsConstants.Security.PermissionSetName))
            {
                var agentsCustomSet = new CustomPermissionsDisplaySettingsConfig(customPermissionsDisplaySettings)
                {
                    SetName = AgentsConstants.Security.PermissionSetName
                };
                customPermissionsDisplaySettings.Add(agentsCustomSet);

                var agentsCustomActions = new SecuredObjectCustomPermissionSet(agentsCustomSet.SecuredObjectCustomPermissionSets) { TypeName = typeof(AgentItem).FullName };
                agentsCustomSet.SecuredObjectCustomPermissionSets.Add(agentsCustomActions);

                var agentCreateAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.Create,
                    ShowActionInList = false,
                    Title = string.Empty,
                    ResourceClassId = string.Empty
                };
                agentsCustomActions.CustomSecurityActions.Add(agentCreateAction);

                var agentModifyAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.Modify,
                    ShowActionInList = true,
                    Title = "ModifyThisAgent",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                agentsCustomActions.CustomSecurityActions.Add(agentModifyAction);

                var agentViewAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.View,
                    ShowActionInList = true,
                    Title = "ViewThisAgent",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                agentsCustomActions.CustomSecurityActions.Add(agentViewAction);

                var agentDeleteAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.Delete,
                    ShowActionInList = true,
                    Title = "DeleteThisAgent",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                agentsCustomActions.CustomSecurityActions.Add(agentDeleteAction);

                var agentChangeOwnerAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.ChangeOwner,
                    ShowActionInList = true,
                    Title = "ChangeOwnerOfThisAgent",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                agentsCustomActions.CustomSecurityActions.Add(agentChangeOwnerAction);

                var agentChangePermissionsAction = new CustomSecurityAction(agentsCustomActions.CustomSecurityActions)
                {
                    Name = AgentsConstants.Security.ChangePermissions,
                    ShowActionInList = true,
                    Title = "ChangePermissionsOfThisAgent",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                agentsCustomActions.CustomSecurityActions.Add(agentChangePermissionsAction);
            }
        }

        /// <summary>
        /// Installs the pages.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallPages(SiteInitializer initializer)
        {
            var pageManager = initializer.PageManager;
            var moduleNode = pageManager.GetPageNode(SiteInitializer.ModulesNodeId);
            var id = AgentsPageGroupId;
            var agentsNode = pageManager.GetPageNodes().Where(t => t.Id == id).SingleOrDefault();
            if (agentsNode == null)
            {
                agentsNode = initializer.CreatePageNode(AgentsPageGroupId, moduleNode, Sitefinity.Pages.Model.NodeType.Group);
                agentsNode.Name = "Agents";
                agentsNode.ShowInNavigation = true;
                agentsNode.Attributes["ModuleName"] = AgentsModule.ModuleName;
                Res.SetLstring(agentsNode.Title, ResourceClassId, "PageGroupNodeTitle");
                Res.SetLstring(agentsNode.UrlName, ResourceClassId, "PageGroupNodeTitle");
                Res.SetLstring(agentsNode.Description, ResourceClassId, "PageGroupNodeDescription");
            }

            id = this.LandingPageId;
            var landingPage =
                pageManager
                .GetPageNodes()
                .SingleOrDefault(p => p.Id == id);
            if (landingPage == null)
            {
                var pageInfo = new PageDataElement()
                {
                    PageId = this.LandingPageId,
                    Name = "Agents",
                    MenuName = "AgentsLandingPageTitle",
                    UrlName = "AgentsLandingPageUrlName",
                    Description = "AgentsLandingPageDescription",
                    HtmlTitle = "AgentsLandingPageHtmlTitle",
                    ResourceClassId = ResourceClassId,
                    IncludeScriptManager = true,
                    ShowInNavigation = false,
                    EnableViewState = false,
                    TemplateName = SiteInitializer.BackendTemplateName
                };
                pageInfo.Parameters["ModuleName"] = AgentsModule.ModuleName;
                var controlPanel = new BackendContentView()
                {
                    ModuleName = AgentsModule.ModuleName,
                    ControlDefinitionName = AgentsDefinitions.BackendDefinitionName
                };
                initializer.CreatePageFromConfiguration(pageInfo, agentsNode, controlPanel);
            }
        }

        /// <summary>
        /// Installs the taxonomies.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallTaxonomies(SiteInitializer initializer)
        {
            this.InstallTaxonomy(initializer, typeof(AgentItem));
        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<AgentsConfig>();
        }

        /// <summary>
        /// Installs module's toolbox configuration.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallConfiguration(SiteInitializer initializer)
        {
            var config = initializer.Context.GetConfig<ToolboxesConfig>();

            var pageControls = config.Toolboxes["PageControls"];

            var section = pageControls
                .Sections
                .Where<ToolboxSection>(e => e.Name == ToolboxesConfig.ContentToolboxSectionName)
                .FirstOrDefault();
            var classId = typeof(AgentsResources).Name;
            if (section == null)
            {
                section = new ToolboxSection(pageControls.Sections)
                {
                    Name = ToolboxesConfig.ContentToolboxSectionName,
                    Title = "ContentToolboxSectionTitle",
                    Description = "ContentToolboxSectionDescription",
                    ResourceClassId = typeof(PageResources).Name
                };
                pageControls.Sections.Add(section);
            }
            if (!section.Tools.Any<ToolboxItem>(e => e.Name == "AgentsView"))
            {
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = "AgentsView",
                    Title = "AgentsViewTitle",
                    Description = "AgentsViewDescription",
                    ResourceClassId = classId,
                    ModuleName = AgentsModule.ModuleName,
                    CssClass = "sfAgentsViewIcn",
                    ControlType = typeof(AgentsView).AssemblyQualifiedName
                };
                section.Tools.Add(tool);
            }
        }

        /// <summary>
        /// Upgrades this module from the specified version.
        /// </summary>
        /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
        /// <param name="upgradeFrom">The version this module us upgrading from.</param>
        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        {
            if (upgradeFrom.Major == 1 && upgradeFrom.Minor < 1)
            {
                LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<AgentItem, AgentsManager>(initializer, Config.Get<AgentsConfig>());
            }
        }


        #endregion

        #region Constants

        public static readonly string WorkflowRelativeUrl = "~/Workflows/Agents.xamlx";
        private static readonly string WorkflowEmbeddedPath = "Telerik.StarterKit.Modules.Agents.AgentsWorkflow.xamlx";

        /// <summary>
        /// Name of the news module. (e.g. used in AgentsManager)
        /// </summary>
        public const string ModuleName = "Agents";

        /// <summary>
        /// Identity for the page group used by all pages in the agents module
        /// </summary>
        public static readonly Guid AgentsPageGroupId = new Guid("8022B1EB-AE5B-449F-A979-41D0FE39E248");

        /// <summary>
        /// Identity of the home (landing) page for the agents module
        /// </summary>
        public static readonly Guid HomePageId = new Guid("ADDF6765-5FF0-4BB1-9D84-D4B498FBE24B");

        /// <summary>
        /// Localization resources' class Id for agents
        /// </summary>
        public static readonly string ResourceClassId = typeof(AgentsResources).Name;

        /// <summary>
        /// Defines the configuration key that the AgentsView control will use to load its sub-views
        /// </summary>
        public const string AgentsViewConfigKey = "AgentsView";

        /// <summary>
        /// Defines the configuration key that the PublicAgentsView control will use to load its sub-views
        /// </summary>
        public const string PublicAgentsViewConfigKey = "PublicAgentsView";



        #endregion

        #region Provicate Constants

        private static readonly Type[] managerTypes = new Type[] { typeof(AgentsManager) };

        #endregion
    }
}
