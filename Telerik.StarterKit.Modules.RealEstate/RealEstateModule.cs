using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Workflow.Configuration;
using Telerik.StarterKit.Modules.RealEstate.Configuration;
using Telerik.StarterKit.Modules.RealEstate.Data;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.StarterKit.Modules.RealEstate.Web.Services;
using Telerik.StarterKit.Modules.RealEstate.Web.UI;
using Telerik.StarterKit.Modules.RealEstate.Web.UI.Public;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Lifecycle;
using System.Collections.Generic;

namespace Telerik.StarterKit.Modules.RealEstate
{
    /// <summary>
    /// Real Estate module - API entry point
    /// </summary>
    public class RealEstateModule : ContentModuleBase
    {
        #region Properties

        /// <summary>
        /// Gets the CLR types of all data managers provided by the Real Estate module.
        /// </summary>
        /// <value>An array of <see cref="Type"/> objects.</value>
        public override Type[] Managers
        {
            get { return managerTypes; }
        }

        /// <summary>
        /// Gets the identity of the home (landing) page for the Real Estate module.
        /// </summary>
        /// <value>The landing page id.</value>
        public override Guid LandingPageId
        {
            get
            {
                return RealEstateModule.HomePageId;
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

            Config.RegisterSection<RealEstateConfig>();
            Res.RegisterResource<RealEstateResources>();
            ObjectFactory.RegisterWebService(typeof(RealEstateBackendService), "Sitefinity/Services/Content/RealEstate.svc");

            //register templatable controls
            ControlTemplates.RegisterTemplatableControl(typeof(MasterListView), typeof(RealEstateItem));
            ControlTemplates.RegisterTemplatableControl(typeof(DetailsView), typeof(RealEstateItem));
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

            IModule realEstateModule;
            SystemManager.ApplicationModules.TryGetValue(RealEstateModule.ModuleName, out realEstateModule);
            initializer.RegisterControlTemplate(MasterListView.layoutTemplateName, typeof(MasterListView).FullName, RealEstateDefinitions.FrontendDefaultListViewName);
            initializer.RegisterControlTemplate(DetailsView.layoutTemplateName, typeof(DetailsView).FullName, RealEstateDefinitions.FrontendDefaultDetailViewName);

            #region Real Estate module permissions set

            this.InstallCustomPermissions(initializer);
            this.InstallCustomWorkflow(initializer);
            this.InstallCustomTaxonomies(initializer);

            #endregion


            //initializer.Context.SaveMetaData(true);
        }


        private void InstallCustomWorkflow(SiteInitializer initializer)
        {
            WorkflowConfig workflowConfig = initializer.Context.GetConfig<WorkflowConfig>();

            if (!workflowConfig.Workflows.ContainsKey(typeof(RealEstateItem).FullName))
            {
                workflowConfig.Workflows.Add(
                new WorkflowElement(workflowConfig.Workflows)
                {
                    ContentType = typeof(RealEstateItem).FullName,
                    ServiceUrl = WorkflowConfig.GetDefaultWorkflowUrl(typeof(RealEstateItem)),
                    Title = "ModuleTitle",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
            }
        }

        private void InstallCustomPermissions(SiteInitializer initializer)
        {
            SecurityConfig securityConfig = initializer.Context.GetConfig<SecurityConfig>();
            ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissionSetConfig = securityConfig.Permissions;
            ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig> CustomPermissionsDisplaySettings = securityConfig.CustomPermissionsDisplaySettings;

            //Add the set
            if (!permissionSetConfig.ContainsKey(RealEstateConstants.Security.PermissionSetName))
            {
                var realEstatePermissionSet = new Telerik.Sitefinity.Security.Configuration.Permission(permissionSetConfig)
                {
                    Name = RealEstateConstants.Security.PermissionSetName,
                    Title = "RealEstatePermissions",
                    Description = "RealEstatePermissionsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                permissionSetConfig.Add(realEstatePermissionSet);

                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.View,
                    Type = SecurityActionTypes.View,
                    Title = "ViewItems",
                    Description = "ViewItemsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.Create,
                    Type = SecurityActionTypes.Create,
                    Title = "CreateItems",
                    Description = "CreateItemsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.Modify,
                    Type = SecurityActionTypes.Modify,
                    Title = "ModifyItems",
                    Description = "ModifyItemsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.Delete,
                    Type = SecurityActionTypes.Delete,
                    Title = "DeleteItems",
                    Description = "DeleteItemsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.ChangeOwner,
                    Type = SecurityActionTypes.ChangeOwner,
                    Title = "ChangeItemOwner",
                    Description = "ChangeItemOwnerDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
                realEstatePermissionSet.Actions.Add(new SecurityAction(realEstatePermissionSet.Actions)
                {
                    Name = RealEstateConstants.Security.ChangePermissions,
                    Type = SecurityActionTypes.ChangePermissions,
                    Title = "ChangeItemPermissions",
                    Description = "ChangeItemPermissionsDescription",
                    ResourceClassId = typeof(RealEstateResources).Name
                });
            }

            //Custom UI views
            if (!CustomPermissionsDisplaySettings.ContainsKey(RealEstateConstants.Security.PermissionSetName))
            {
                var realEstateCustomSet = new CustomPermissionsDisplaySettingsConfig(CustomPermissionsDisplaySettings)
                {
                    SetName = RealEstateConstants.Security.PermissionSetName
                };
                CustomPermissionsDisplaySettings.Add(realEstateCustomSet);

                var realEstateCustomActions = new SecuredObjectCustomPermissionSet(realEstateCustomSet.SecuredObjectCustomPermissionSets) { TypeName = typeof(RealEstateItem).FullName };
                realEstateCustomSet.SecuredObjectCustomPermissionSets.Add(realEstateCustomActions);

                var itemCreateAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.Create,
                    ShowActionInList = false,
                    Title = string.Empty,
                    ResourceClassId = string.Empty
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemCreateAction);

                var itemModifyAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.Modify,
                    ShowActionInList = true,
                    Title = "ModifyThisItem",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemModifyAction);

                var itemViewAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.View,
                    ShowActionInList = true,
                    Title = "ViewThisItem",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemViewAction);

                var itemDeleteAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.Delete,
                    ShowActionInList = true,
                    Title = "DeleteThisItem",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemDeleteAction);

                var itemChangeOwnerAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.ChangeOwner,
                    ShowActionInList = true,
                    Title = "ChangeOwnerOfThisItem",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemChangeOwnerAction);

                var itemChangePermissionsAction = new CustomSecurityAction(realEstateCustomActions.CustomSecurityActions)
                {
                    Name = RealEstateConstants.Security.ChangePermissions,
                    ShowActionInList = true,
                    Title = "ChangePermissionsOfThisItem",
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                realEstateCustomActions.CustomSecurityActions.Add(itemChangePermissionsAction);
            }
        }

        /// <summary>
        /// Installs the taxonomies.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected void InstallCustomTaxonomies(SiteInitializer initializer)
        {
            //installs the default Tags and Category taxonomies
            this.InstallTaxonomy(initializer, typeof(RealEstateItem));

            var metaMan = initializer.Context.MetadataManager;
            var taxMan = initializer.TaxonomyManager;
            var type = this.GetMetaType(initializer, typeof(RealEstateItem));

            #region Flat Types Taxonomy
            var typeFlatTaxonomy = this.GetOrCreateTaxonomy<FlatTaxonomy>(initializer, "Types", TypesTaxonomyId, "Type");

            this.CreateFlatTaxonIfDoesntExists(initializer, typeFlatTaxonomy, "Flats", "Flats");
            this.CreateFlatTaxonIfDoesntExists(initializer, typeFlatTaxonomy, "Houses", "Houses");
            this.CreateFlatTaxonIfDoesntExists(initializer, typeFlatTaxonomy, "Offices", "Offices");

            if (!type.Fields.ToList().Any(fld => fld.FieldName == "Types"))
            {
                var field = metaMan.CreateMetafield("Types");
                field.TaxonomyProvider = taxMan.Provider.Name;
                field.TaxonomyId = TypesTaxonomyId;
                field.IsSingleTaxon = false;
                type.Fields.Add(field);
            }
            #endregion

            #region Location Flat Taxonomy
            var locationFlatTaxonomy = this.GetOrCreateTaxonomy<FlatTaxonomy>(initializer, "Locations", LocationsTaxonomyId, "Location");

            this.CreateFlatTaxonIfDoesntExists(initializer, locationFlatTaxonomy, "Sofia", "Sofia");
            this.CreateFlatTaxonIfDoesntExists(initializer, locationFlatTaxonomy, "Plovdiv", "Plovdiv");
            this.CreateFlatTaxonIfDoesntExists(initializer, locationFlatTaxonomy, "Varna", "Varna");

            if (!type.Fields.ToList().Any(fld => fld.FieldName == "Locations"))
            {
                var field = metaMan.CreateMetafield("Locations");
                field.TaxonomyProvider = taxMan.Provider.Name;
                field.TaxonomyId = LocationsTaxonomyId;
                field.IsSingleTaxon = false;
                type.Fields.Add(field);
            } 
            #endregion

            #region Rooms Flat Taxonomy
            var roomsFlatTaxonomy = this.GetOrCreateTaxonomy<FlatTaxonomy>(initializer, "Rooms", RoomsTaxonomyId, "Room");

            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "1 Bedroom", "1 Bedroom");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "2 Bedrooms", "2 Bedrooms");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "3 Bedrooms", "3 Bedrooms");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "1 Bathroom", "1 Bathroom");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "2 Bathrooms", "2 Bathrooms");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "Living room", "Living room");
            this.CreateFlatTaxonIfDoesntExists(initializer, roomsFlatTaxonomy, "Separate kitchen", "Separate kitchen");
        
            if (!type.Fields.ToList().Any(fld => fld.FieldName == "Rooms"))
            {
                var field = metaMan.CreateMetafield("Rooms");
                field.TaxonomyProvider = taxMan.Provider.Name;
                field.TaxonomyId = RoomsTaxonomyId;
                field.IsSingleTaxon = false;
                type.Fields.Add(field);
            }
            #endregion

            #region Features Flat Taxonomy
            var featuresFlatTaxonomy = this.GetOrCreateTaxonomy<FlatTaxonomy>(initializer, "Features", FeaturesTaxonomyId, "Feature");
            this.CreateFlatTaxonIfDoesntExists(initializer, featuresFlatTaxonomy, "Swimming pool", "Swimming pool");
            this.CreateFlatTaxonIfDoesntExists(initializer, featuresFlatTaxonomy, "Parking", "Parking");
            this.CreateFlatTaxonIfDoesntExists(initializer, featuresFlatTaxonomy, "Garden", "Garden");
            this.CreateFlatTaxonIfDoesntExists(initializer, featuresFlatTaxonomy, "Fitness", "Fitness");
            this.CreateFlatTaxonIfDoesntExists(initializer, featuresFlatTaxonomy, "Shops", "Shops");
            
            if (!type.Fields.ToList().Any(fld => fld.FieldName == "Features"))
            {
                var field = metaMan.CreateMetafield("Features");
                field.TaxonomyProvider = taxMan.Provider.Name;
                field.TaxonomyId = FeaturesTaxonomyId;
                field.IsSingleTaxon = false;
                type.Fields.Add(field);
            }
            #endregion

            var categoryTaxonomy = taxMan.GetTaxonomy<HierarchicalTaxonomy>(TaxonomyManager.CategoriesTaxonomyId);

            #region Real Estate Item Type Categories

            var realEstateItemTypesTaxon = this.CreateHierarchicalTaxonIfDoesntExist(initializer, RealEstateModule.RealEstateItemTypesTaxonId,
                "Real Estate Item Types", "Real Estate Item Types", "Taxonomy which will classify the real estate items", categoryTaxonomy);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.ForRentItemTypeTaxonId, "For Rent", "For Rent",
               "This category holds the items for rent", realEstateItemTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.ForSaleItemTypeTaxonId, "For Sale", "For Sale",
               "This category holds the items for sale", realEstateItemTypesTaxon);

            #endregion

            #region Real Estate Photo Type Categories

            var realEstatePhotoTypesTaxon = this.CreateHierarchicalTaxonIfDoesntExist(initializer, RealEstateModule.RealEstatePhotoTypesTaxonId, 
                "Real Estate Photo Types", "Real Estate Photo Types", "Taxonomy which will classify the real estate item photos", categoryTaxonomy);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.SliderPhotoTaxonId, "Slider Photo", "Slider Photo",
               "This category holds the photos to be shown on the slider widget", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.SliderThumbnailTaxonId, "Slider Thumbnail", "Slider Thumbnail",
               "This category holds the photos to be shown on the slider's carousel as thumbnails", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.FlowListPhotoTaxonId, "Flow List Photo", "Flow List Photo",
               "This category holds the photos to be shown on the listing pages that use the flow layout", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.ThumbnailListPhotoTaxonId, "Thumbnail List Photo", "Thumbnail List Photo",
               "This category holds the photos to be shown on the listing pages that use the thumbnail layout", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.SimilarPropertiesPhotoTaxonId, "Similar Properties Photo", "Similar Properties Photo",
               "This category holds the photos to be shown on the similar properties' carousel as thumbnails", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.OverviewTabPhotoTaxonId, "Overview Tab Photo", "Overview Tab Photo",
               "This category holds the photos to be shown within the Overview tab on the details pages", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.PhotosTabPhotoTaxonId, "Photos Tab Photo", "Photos Tab Photo",
               "This category holds the photos to be shown within the Photos tab on the details pages", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.PanaromicViewTabPhotoTaxonId, "Panaromic View Tab Photo", "Panaromic View Tab Photo",
                "This category holds the photos to be shown within the Panaromic View tab on the details pages", realEstatePhotoTypesTaxon);

            this.CreateHierarchicalSubTaxonIfDoesntExist(initializer, RealEstateModule.FloorPlanTabPhotoTaxonId, "Floorplan Tab Photo", "Floorplan Tab Photo",
                "This category holds the photos to be shown within the Floorplan tab on the details pages", realEstatePhotoTypesTaxon);

            #endregion

            initializer.Context.MetadataManager.SaveChanges(false);
            //taxMan.SaveChanges();
        }

        /// <summary>
        /// Installs the pages.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallPages(SiteInitializer initializer)
        {
            var pageManager = initializer.PageManager;
            var moduleNode = pageManager.GetPageNode(SiteInitializer.ModulesNodeId);
            var id = PageGroupId;
            var realEstateNode = pageManager.GetPageNodes().Where(t => t.Id == id).SingleOrDefault();
            if (realEstateNode == null)
            {
                realEstateNode = initializer.CreatePageNode(PageGroupId, moduleNode, Sitefinity.Pages.Model.NodeType.Group);
                realEstateNode.Name = "RealEstate";
                realEstateNode.ShowInNavigation = true;
                realEstateNode.Attributes["ModuleName"] = RealEstateModule.ModuleName;
                Res.SetLstring(realEstateNode.Title, ResourceClassId, "PageGroupNodeTitle");
                Res.SetLstring(realEstateNode.UrlName, ResourceClassId, "PageGroupNodeTitle");
                Res.SetLstring(realEstateNode.Description, ResourceClassId, "PageGroupNodeDescription");
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
                    Name = "RealEstate",
                    MenuName = "LandingPageTitle",
                    UrlName = "LandingPageUrlName",
                    Description = "LandingPageDescription",
                    HtmlTitle = "LandingPageHtmlTitle",
                    ResourceClassId = ResourceClassId,
                    IncludeScriptManager = true,
                    ShowInNavigation = false,
                    EnableViewState = false,
                    TemplateName = SiteInitializer.BackendTemplateName
                };
                pageInfo.Parameters["ModuleName"] = RealEstateModule.ModuleName;
                var controlPanel = new BackendContentView()
                {
                    ModuleName = RealEstateModule.ModuleName,
                    ControlDefinitionName = RealEstateDefinitions.BackendDefinitionName
                };
                initializer.CreatePageFromConfiguration(pageInfo, realEstateNode, controlPanel);
            }
        }

        /// <summary>
        /// Installs the taxonomies.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        protected override void InstallTaxonomies(SiteInitializer initializer)
        {
            this.InstallTaxonomy(initializer, typeof(RealEstateItem));
        }

        /// <summary>
        /// Gets the module config.
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<RealEstateConfig>();
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
            var classId = typeof(RealEstateResources).Name;
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
            if (!section.Tools.Any<ToolboxItem>(e => e.Name == "RealEstateView"))
            {
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = "RealEstateView",
                    Title = "RealEstateViewTitle",
                    Description = "RealEstateViewDescription",
                    ResourceClassId = classId,
                    ModuleName = RealEstateModule.ModuleName,
                    CssClass = "sfRealEstateViewIcn",
                    ControlType = typeof(RealEstateView).AssemblyQualifiedName
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
            if (upgradeFrom.Major == 1 && upgradeFrom.Minor < 4)
            {
                LifecycleExtensions.UpgradePublishedTranslationsAndLanguageData<RealEstateItem, RealEstateManager>(initializer, Config.Get<RealEstateConfig>());
            }
        }

        #endregion

        #region Taxonomy Methods

        private MetaType GetMetaType(SiteInitializer initializer, Type type)
        {

            var metaMan = initializer.Context.MetadataManager;
            var scope = (metaMan.Provider as IOpenAccessDataProvider).GetContext();
            var result = scope.GetDirtyObjects<MetaType>().FirstOrDefault(mt => mt.ClassName == type.Name && mt.Namespace == type.Namespace);
            if (result == null)
            {
                result = metaMan.GetMetaType(type);
                if (result == null)
                {
                    result = metaMan.CreateMetaType(type);
                }
            }
            return result;
        }

        private TTaxonomy GetOrCreateTaxonomy<TTaxonomy>(SiteInitializer initializer, string taxonomyName, Guid taxonomyId, string taxonName) where TTaxonomy : class, ITaxonomy
        {
            TTaxonomy taxonomy = initializer.Context.GetSharedObject<TTaxonomy>(taxonomyName);
            if (taxonomy == null)
            {
                var taxMan = initializer.TaxonomyManager;
                taxonomy = taxMan.GetTaxonomies<TTaxonomy>().FirstOrDefault(t => t.Name == taxonomyName);
                if (taxonomy == null)
                {
                    taxonomy = taxMan.CreateTaxonomy<TTaxonomy>(taxonomyId);
                    taxonomy.Name = taxonomyName;
                    taxonomy.Title = taxonomyName;
                    taxonomy.TaxonName = taxonName;
                    ((ISecuredObject)taxonomy).CanInheritPermissions = true;
                    ((ISecuredObject)taxonomy).InheritsPermissions = true;
                    ((ISecuredObject)taxonomy).SupportedPermissionSets = new string[] { SecurityConstants.Sets.Taxonomies.SetName };
                }
                initializer.Context.SetSharedObject(taxonomyName, taxonomy);
            }
            return taxonomy;
        }

        private void CreateFlatTaxonIfDoesntExists(SiteInitializer initializer, FlatTaxonomy taxonomy, string taxonName, string taxonTitle)
        {
            if (initializer.TaxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Name == taxonName).Count() == 0)
            {
                FlatTaxon taxon = null;
                taxon = initializer.TaxonomyManager.CreateTaxon<FlatTaxon>();
                taxon.Name = taxonName;
                taxon.Title = taxonTitle;
                taxonomy.Taxa.Add(taxon);
            }
        }

        private void CreateHierarchicalSubTaxonIfDoesntExist(SiteInitializer initializer, Guid taxonId, string taxonName,
            string taxonTitle, string taxonDescription, HierarchicalTaxon parent)
        {
            var taxon = initializer.TaxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Id == taxonId).SingleOrDefault();
            if (taxon == null)
            {
                taxon = initializer.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(taxonId);
                taxon.Title = taxonTitle;
                taxon.Name = taxonName;
                taxon.Description = taxonDescription;
                taxon.Parent = parent;
                parent.Subtaxa.Add(taxon);
            }
        }

        private HierarchicalTaxon CreateHierarchicalTaxonIfDoesntExist(SiteInitializer initializer, Guid taxonId, string taxonName,
            string taxonTitle, string taxonDescription, HierarchicalTaxonomy taxonomy)
        {
            var taxon = initializer.TaxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Id == taxonId).SingleOrDefault();
            if (taxon == null)
            {
                taxon = initializer.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(taxonId);
                taxon.Title = taxonTitle;
                taxon.Name = taxonName;
                taxon.Description = taxonDescription;
                taxonomy.Taxa.Add(taxon);
            }
            return taxon;
        }

        #endregion

        #region Constants

        public static readonly string WorkflowRelativeUrl = "~/Workflows/RealEstate.xamlx";
        private static readonly string WorkflowEmbeddedPath = "Telerik.StarterKit.Modules.RealEstate.RealEstateWorkflow.xamlx";

        /// <summary>
        /// Name of the Real Estate module. (e.g. used in RealEstateManager)
        /// </summary>
        public const string ModuleName = "RealEstate";

        /// <summary>
        /// Identity for the page group used by all pages in the Real Estate module
        /// </summary>
        public static readonly Guid PageGroupId = new Guid("809EEE44-2134-4DD2-977F-12FC4938D15A");

        /// <summary>
        /// Identity of the home (landing) page for the Real Estate module
        /// </summary>
        public static readonly Guid HomePageId = new Guid("7239677E-ABE5-4036-954D-27FFA4EDA011");

        /// <summary>
        /// Localization resources' class Id for Real Estate module
        /// </summary>
        public static readonly string ResourceClassId = typeof(RealEstateResources).Name;

        /// <summary>
        /// Defines the configuration key that the RealEstateView control will use to load its sub-views
        /// </summary>
        public const string ViewConfigKey = "RealEstateView";

        /// <summary>
        /// Defines the configuration key that the PublicRealEstateView control will use to load its sub-views
        /// </summary>
        public const string PublicViewConfigKey = "PublicRealEstateView";

        public static readonly Guid TypesTaxonomyId = new Guid("CF110BF0-292D-49BB-ADBB-46961794734C");
        public static readonly Guid LocationsTaxonomyId = new Guid("7C261AA4-748A-4CE6-B05F-3E32F3882E44");
        public static readonly Guid RoomsTaxonomyId = new Guid("B1782A75-7F59-4DEC-8ECE-7C7DD6133C18");
        public static readonly Guid FeaturesTaxonomyId = new Guid("8CCEE613-4C4F-4358-9AB0-E232E635DA73");

        public static readonly Guid RealEstateItemTypesTaxonId = new Guid("84933684-4782-45C3-842B-5E03EFAF0639");
        public static readonly Guid ForRentItemTypeTaxonId = new Guid("9A2E3C89-FB55-48FC-A211-0D044D1FAE74");
        public static readonly Guid ForSaleItemTypeTaxonId = new Guid("CB92EDCA-0DE8-4B6D-817D-2B26F01B8E56");

        public static readonly Guid OverviewTabPhotoTaxonId = new Guid("1230C966-48C3-48CE-BDBB-BCC40ABB7995");
        public static readonly Guid PhotosTabPhotoTaxonId = new Guid("8A054746-6A59-4647-81B1-3E982C193334");
        public static readonly Guid PanaromicViewTabPhotoTaxonId = new Guid("D0607BE4-B31A-4024-AEDC-2D23DB413933");
        public static readonly Guid FloorPlanTabPhotoTaxonId = new Guid("14CAE7FA-D8BC-4AA3-9821-286CD7A3F96B");
        public static readonly Guid RealEstatePhotoTypesTaxonId = new Guid("7B0E6703-6624-45C4-886B-C30AB23AB5C0");
        public static readonly Guid SliderPhotoTaxonId = new Guid("7105CF2A-9232-4230-AE0A-1E7ADF3DC8CD");
        public static readonly Guid SliderThumbnailTaxonId = new Guid("04B8049C-3700-4816-9051-6A2A131F13C1");
        public static readonly Guid FlowListPhotoTaxonId = new Guid("CD4D8E48-FB1A-4C21-A301-A1E8EB1B4E13");
        public static readonly Guid ThumbnailListPhotoTaxonId = new Guid("21A3BA4A-2DB5-4996-86E8-7EF46F07FA0B");
        public static readonly Guid SimilarPropertiesPhotoTaxonId = new Guid("ac60f2d0-341f-489f-95cf-3c6bc2f32a85");

        #endregion

        #region Provicate Constants

        private static readonly Type[] managerTypes = new Type[] { typeof(RealEstateManager) };

        #endregion
    }
}
