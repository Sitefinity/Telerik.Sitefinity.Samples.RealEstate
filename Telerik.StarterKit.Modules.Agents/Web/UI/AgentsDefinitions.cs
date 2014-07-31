using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Configuration;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Taxonomies;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.StarterKit.Modules.Agents.Web.UI.Public;

namespace Telerik.StarterKit.Modules.Agents.Web.UI
{
    /// <summary>
    /// This is a static class used to initialize the properties for all ContentView control views
    /// of supplied by default for the agents module.
    /// </summary>
    public class AgentsDefinitions
    {
        /// <summary>
        /// Static constructor that makes it impossible to use the definitions
        /// without the module 
        /// </summary>
        static AgentsDefinitions()
        {
            // Ensure Agents module is initialized.
            SystemManager.GetApplicationModule(AgentsModule.ModuleName);
        }

        public static ContentViewControlElement DefineAgentsBackendContentView(ConfigElement parent)
        {
            // define content view control
            var backendContentView = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = BackendDefinitionName,
                ContentType = typeof(AgentItem)
            };

            // *** define views ***

            #region agents backend list view

            var agentsGridView = new MasterGridViewElement(backendContentView.ViewsConfig)
            {
                ViewName = AgentsDefinitions.BackendListViewName,
                ViewType = typeof(MasterGridView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 50,
                ResourceClassId = typeof(AgentsResources).Name,
                SearchFields = "Title",
                SortExpression = "Title ASC",
                Title = "AgentsViewTitle",
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Agents.svc/"
            };

            #region Toolbar definition

            WidgetBarSectionElement masterViewToolbarSection = new WidgetBarSectionElement(agentsGridView.ToolbarConfig.Sections)
            {
                Name = "toolbar"
            };


            var createAgentsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "CreateAgentsWidget",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfMainAction",
                WidgetType = typeof(CommandWidget),
                PermissionSet = AgentsConstants.Security.PermissionSetName,
                ActionName = AgentsConstants.Security.Create
            };
            masterViewToolbarSection.Items.Add(createAgentsWidget);

            var deleteAgentsWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "DeleteAgentsWidget",
                ButtonType = CommandButtonType.Standard,
                CommandName = DefinitionsHelper.GroupDeleteCommandName,
                Text = "Delete",
                ResourceClassId = typeof(AgentsResources).Name,
                WidgetType = typeof(CommandWidget),
                CssClass = "sfGroupBtn"
            };
            masterViewToolbarSection.Items.Add(deleteAgentsWidget);


            masterViewToolbarSection.Items.Add(DefinitionsHelper.CreateSearchButtonWidget(masterViewToolbarSection.Items, typeof(AgentItem)));

            agentsGridView.ToolbarConfig.Sections.Add(masterViewToolbarSection);

            #endregion

            #region Sidebar definition

            var languagesSection = new LocalizationWidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "Languages",
                Title = "Languages",
                ResourceClassId = typeof(LocalizationResources).Name,
                CssClass = "sfFirst sfSeparator sfLangSelector",
                WrapperTagId = "languagesSection"
            };

            languagesSection.Items.Add(new LanguagesDropDownListWidgetElement(languagesSection.Items)
            {
                Name = "Languages",
                Text = "Languages",
                ResourceClassId = typeof(LocalizationResources).Name,
                CssClass = "",
                WidgetType = typeof(LanguagesDropDownListWidget),
                IsSeparator = false,
                LanguageSource = LanguageSource.Frontend,
                AddAllLanguagesOption = false,
                CommandName = DefinitionsHelper.ChangeLanguageCommandName
            });

            WidgetBarSectionElement sidebarSection = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "Filter",
                Title = "FilterAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
                WrapperTagId = "filterSection"
            };

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "AllAgents",
                CommandName = DefinitionsHelper.ShowAllItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "AllAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                ButtonCssClass = "sfSel",
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "MyAgents",
                CommandName = DefinitionsHelper.ShowMyItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "MyAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "DraftAgents",
                CommandName = DefinitionsHelper.ShowMasterItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "DraftAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "PublishedAgents",
                CommandName = DefinitionsHelper.ShowPublishedItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PublishedAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "ScheduledAgents",
                CommandName = DefinitionsHelper.ShowScheduledItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ScheduledAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "PendingApprovalAgents",
                CommandName = DefinitionsHelper.PendingApprovalItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "WaitingForApproval",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new LiteralWidgetElement(sidebarSection.Items)
            {
                Name = "Separator",
                WrapperTagKey = HtmlTextWriterTag.Li,
                WidgetType = typeof(LiteralWidget),
                CssClass = "sfSeparator",
                Text = "&nbsp;",
                IsSeparator = true
            });

            var categoryFilterSection = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "Category",
                Title = "AgentItemsByCategory",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "categoryFilterSection",
                Visible = false
            };
            agentsGridView.SidebarConfig.Sections.Add(categoryFilterSection);

            var tagFilterSection = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "ByTag",
                Title = "AgentItemsByTag",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "tagFilterSection",
                Visible = false
            };
            agentsGridView.SidebarConfig.Sections.Add(tagFilterSection);

            var dateFilterSection = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "UpdatedAgents",
                Title = "DisplayLastUpdatedAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfFilterBy sfFilterByDate sfSeparator",
                WrapperTagId = "dateFilterSection",
                Visible = false
            };
            agentsGridView.SidebarConfig.Sections.Add(dateFilterSection);

            categoryFilterSection.Items.Add(new CommandWidgetElement(categoryFilterSection.Items)
            {
                Name = "CloseCategories",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    categoryFilterSection.WrapperTagId, tagFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseCategories",
                ResourceClassId = typeof(Labels).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            var categoryFilterWidget = new DynamicCommandWidgetElement(categoryFilterSection.Items)
            {
                Name = "CategoryFilter",
                CommandName = "filterByCategory",
                PageSize = 10,
                WidgetType = typeof(DynamicCommandWidget),
                IsSeparator = false,
                BindTo = BindCommandListTo.HierarchicalData,
                BaseServiceUrl = String.Format("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/{0}/", TaxonomyManager.CategoriesTaxonomyId),
                ChildItemsServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/subtaxa/",
                PredecessorServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/predecessor/",
                ClientItemTemplate = @"<a href='javascript:void(0);' class='sf_binderCommand_filterByCategory'>{{ Title }}</a> <span class='sfCount'>({{ItemsCount}})</span>"
            };

            categoryFilterWidget.UrlParameters.Add("itemType", typeof(AgentItem).AssemblyQualifiedName);
            categoryFilterSection.Items.Add(categoryFilterWidget);

            tagFilterSection.Items.Add(new CommandWidgetElement(tagFilterSection.Items)
            {
                Name = "CloseTags",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    tagFilterSection.WrapperTagId, categoryFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseTags",
                ResourceClassId = typeof(Labels).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            var clientTemplateBuilder = new System.Text.StringBuilder();
            clientTemplateBuilder.Append(@"<a href=""javascript:void(0);"" class=""sf_binderCommand_filterByTag");
            clientTemplateBuilder.Append(@""">{{Title}}</a> <span class='sfCount'>({{ItemsCount}})</span>");
            var tagFilterWidget = new DynamicCommandWidgetElement(tagFilterSection.Items)
            {
                Name = "TagFilter",
                CommandName = "filterByTag",
                PageSize = 10,
                WidgetType = typeof(DynamicCommandWidget),
                IsSeparator = false,
                BindTo = BindCommandListTo.Client,
                BaseServiceUrl = String.Format("~/Sitefinity/Services/Taxonomies/FlatTaxon.svc/{0}/", TaxonomyManager.TagsTaxonomyId),
                ResourceClassId = typeof(Labels).Name,
                MoreLinkText = "ShowMoreTags",
                MoreLinkCssClass = "sfShowMore",
                LessLinkText = "ShowLessTags",
                LessLinkCssClass = "sfShowMore",
                SelectedItemCssClass = "sfSel",
                ClientItemTemplate = clientTemplateBuilder.ToString()
            };
            tagFilterWidget.UrlParameters.Add("itemType", typeof(AgentItem).AssemblyQualifiedName);

            tagFilterSection.Items.Add(tagFilterWidget);

            // TODO: get all taxonomies used by the productsItem type and create a command item for them
            // and respective link
            //DefinitionsHelper.CreateTaxonomyLinks(sidebarSection);
            DefinitionsHelper.CreateTaxonomyLink(
                TaxonomyManager.CategoriesTaxonomyId,
                DefinitionsHelper.HideSectionsExceptCommandName,
                DefinitionsHelper.ConstructDisplaySectionsCommandArgument(categoryFilterSection.WrapperTagId),
                sidebarSection);

            DefinitionsHelper.CreateTaxonomyLink(
                TaxonomyManager.TagsTaxonomyId,
                DefinitionsHelper.HideSectionsExceptCommandName,
                DefinitionsHelper.ConstructDisplaySectionsCommandArgument(tagFilterSection.WrapperTagId),
                sidebarSection);

            #region Filter by date

            var closeDateFilterWidget = (new CommandWidgetElement(dateFilterSection.Items)
            {
                Name = "CloseDateFilter",
                CommandName = DefinitionsHelper.ShowSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(
                    tagFilterSection.WrapperTagId, categoryFilterSection.WrapperTagId, dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "CloseDateFilter",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfCloseFilter",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });
            dateFilterSection.Items.Add(closeDateFilterWidget);

            var dateFilterWidget = new DateFilteringWidgetDefinitionElement(dateFilterSection.Items)
            {
                Name = "DateFilter",
                WidgetType = typeof(DateFilteringWidget),
                IsSeparator = false,
                PropertyNameToFilter = "LastModified"
            };

            DefinitionsHelper.GetPredefinedDateFilteringRanges(dateFilterWidget.PredefinedFilteringRanges);

            dateFilterSection.Items.Add(dateFilterWidget);

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "FilterByDate",
                CommandName = DefinitionsHelper.HideSectionsExceptCommandName,
                CommandArgument = DefinitionsHelper.ConstructDisplaySectionsCommandArgument(dateFilterSection.WrapperTagId),
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ByDateModified",
                ResourceClassId = typeof(AgentsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            #endregion Filter by date

            WidgetBarSectionElement manageAlso = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "ManageAlso",
                Title = "ManageAlso",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfWidgetsList sfSeparator",
                WrapperTagId = "manageAlsoSection"
            };

            WidgetBarSectionElement settings = new WidgetBarSectionElement(agentsGridView.SidebarConfig.Sections)
            {
                Name = "Settings",
                Title = "Settings",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfWidgetsList sfSettings",
                WrapperTagId = "settingsSection"
            };

            CommandWidgetElement agentsPermissions = new CommandWidgetElement(settings.Items)
            {
                Name = "agentsPermissions",
                CommandName = DefinitionsHelper.PermissionsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PermissionsForAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(agentsPermissions);

            CommandWidgetElement agentsSettings = new CommandWidgetElement(settings.Items)
            {
                Name = "agentsSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "SettingsForAgents",
                ResourceClassId = typeof(AgentsResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(agentsSettings);



            agentsGridView.SidebarConfig.Title = "ManageAgents";
            agentsGridView.SidebarConfig.Sections.Add(languagesSection);
            agentsGridView.SidebarConfig.Sections.Add(sidebarSection);
            agentsGridView.SidebarConfig.Sections.Add(manageAlso);
            agentsGridView.SidebarConfig.Sections.Add(settings);

            #endregion

            #region ContextBar definition

            var translationsContextBarSection = new LocalizationWidgetBarSectionElement(agentsGridView.ContextBarConfig.Sections)
            {
                Name = "Languages",
                WrapperTagKey = HtmlTextWriterTag.Div,
                CssClass = "sfContextWidgetWrp",
                MinLanguagesCountTreshold = DefinitionsHelper.LanguageItemsPerRow
            };

            translationsContextBarSection.Items.Add(new CommandWidgetElement(translationsContextBarSection.Items)
            {
                Name = "ShowMoreTranslations",
                CommandName = DefinitionsHelper.ShowMoreTranslationsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ShowAllTranslations",
                ResourceClassId = typeof(LocalizationResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                CssClass = "sfShowHideLangVersions",
                WrapperTagKey = HtmlTextWriterTag.Div
            });

            translationsContextBarSection.Items.Add(new CommandWidgetElement(translationsContextBarSection.Items)
            {
                Name = "HideMoreTranslations",
                CommandName = DefinitionsHelper.HideMoreTranslationsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ShowBasicTranslationsOnly",
                ResourceClassId = typeof(LocalizationResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                CssClass = "sfDisplayNone sfShowHideLangVersions",
                WrapperTagKey = HtmlTextWriterTag.Div
            });

            agentsGridView.ContextBarConfig.Sections.Add(translationsContextBarSection);

            #endregion

            #region Grid View Mode

            var gridMode = new GridViewModeElement(agentsGridView.ViewModesConfig)
            {
                Name = "Grid"
            };
            agentsGridView.ViewModesConfig.Add(gridMode);

            DataColumnElement titleColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Title",
                HeaderText = "Title",
                HeaderCssClass = "sfTitleCol",
                ItemCssClass = "sfTitleCol",
                ClientTemplate = @"<a sys:href='javascript:void(0);' sys:class=""{{ 'sf_binderCommand_edit sfItemTitle sf' + UIStatus.toLowerCase()}}"">
                    <strong>{{Title}}</strong>
                    <span class='sfStatusLocation'>{{Status}}</span></a>",
                ResourceClassId = typeof(Labels).Name
            };
            gridMode.ColumnsConfig.Add(titleColumn);


            DataColumnElement emailColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Email",
                HeaderText = "Email",
                ClientTemplate = "<span>{{Email}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular",
                ResourceClassId = typeof(AgentsResources).Name
            };
            gridMode.ColumnsConfig.Add(emailColumn);

            DataColumnElement phoneNumberColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "PhoneNumber",
                HeaderText = "PhoneNumber",
                ClientTemplate = "<span>{{PhoneNumber}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular",
                ResourceClassId = typeof(AgentsResources).Name
            };
            gridMode.ColumnsConfig.Add(phoneNumberColumn);


            var translationsColumn = new DynamicColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Translations",
                HeaderText = "Translations",
                DynamicMarkupGenerator = typeof(LanguagesColumnMarkupGenerator),
                ItemCssClass = "sfLanguagesCol",
                HeaderCssClass = "sfLanguagesCol",
                ResourceClassId = typeof(Labels).Name
            };
            translationsColumn.GeneratorSettingsElement = new LanguagesColumnMarkupGeneratorElement(translationsColumn)
            {
                LanguageSource = LanguageSource.Frontend,
                ItemsInGroupCount = DefinitionsHelper.LanguageItemsPerRow,
                ContainerTag = "div",
                GroupTag = "div",
                ItemTag = "div",
                ContainerClass = string.Empty,
                GroupClass = string.Empty,
                ItemClass = string.Empty
            };
            gridMode.ColumnsConfig.Add(translationsColumn);

            ActionMenuColumnElement actionsColumn = new ActionMenuColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Actions",
                HeaderText = "Actions",
                HeaderCssClass = "sfMoreActions",
                ItemCssClass = "sfMoreActions",
                ResourceClassId = typeof(Labels).Name
            };
            FillActionMenuItems(actionsColumn.MenuItems, actionsColumn, typeof(AgentsResources).Name);
            gridMode.ColumnsConfig.Add(actionsColumn);

            DataColumnElement authorColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Author",
                HeaderText = "Author",
                ClientTemplate = "<span>{{Author}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular",
                ResourceClassId = typeof(Labels).Name
            };
            gridMode.ColumnsConfig.Add(authorColumn);

            DataColumnElement dateColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Date",
                HeaderText = "Date",
                ClientTemplate = "<span>{{ (DateCreated) ? DateCreated.sitefinityLocaleFormat('dd MMM, yyyy hh:mm:ss'): '-' }}</span>",
                HeaderCssClass = "sfDate",
                ItemCssClass = "sfDate",
                ResourceClassId = typeof(Labels).Name
            };
            gridMode.ColumnsConfig.Add(dateColumn);

            #endregion

            #region DecisionScreens definition

            DecisionScreenElement dsElement = new DecisionScreenElement(agentsGridView.DecisionScreensConfig)
            {
                Name = "NoItemsExistScreen",
                DecisionType = DecisionType.NoItemsExist,
                MessageType = MessageType.Neutral,
                Displayed = false,
                Title = "WhatDoYouWantToDoNow",
                MessageText = "NoAgentItems",
                ResourceClassId = typeof(AgentsResources).Name
            };

            CommandWidgetElement actionCreateNew = new CommandWidgetElement(dsElement.Actions)
            {
                Name = "Create",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfCreateItem",
                PermissionSet = AgentsConstants.Security.PermissionSetName,
                ActionName = AgentsConstants.Security.Create
            };
            dsElement.Actions.Add(actionCreateNew);

            agentsGridView.DecisionScreensConfig.Add(dsElement);

            #endregion

            #region Dialogs definition

            var parameters = string.Concat(
                "?ControlDefinitionName=",
                AgentsDefinitions.BackendDefinitionName,
                "&ViewName=",
                AgentsDefinitions.BackendInsertViewName);
            DialogElement createDialogElement = DefinitionsHelper.CreateDialogElement(
                agentsGridView.DialogsConfig,
                DefinitionsHelper.CreateCommandName,
                "ContentViewInsertDialog",
                parameters);
            agentsGridView.DialogsConfig.Add(createDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                AgentsDefinitions.BackendDefinitionName,
                "&ViewName=",
                AgentsDefinitions.BackendEditViewName);
            DialogElement editDialogElement = DefinitionsHelper.CreateDialogElement(
                agentsGridView.DialogsConfig,
                DefinitionsHelper.EditCommandName,
                "ContentViewEditDialog",
                parameters);
            agentsGridView.DialogsConfig.Add(editDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                AgentsDefinitions.BackendDefinitionName,
                "&ViewName=",
                AgentsDefinitions.BackendPreviewViewName,
                "&backLabelText=", Res.Get<AgentsResources>().BackToItems, "&SuppressBackToButtonLabelModify=true");
            DialogElement previewDialogElement = DefinitionsHelper.CreateDialogElement(
                agentsGridView.DialogsConfig,
                DefinitionsHelper.PreviewCommandName,
                "ContentViewEditDialog",
                parameters);
            agentsGridView.DialogsConfig.Add(previewDialogElement);

            string permissionsParams = string.Concat(
                "?moduleName=", AgentsDefinitions.ModuleName,
                "&typeName=", typeof(AgentItem).AssemblyQualifiedName,
                "&backLabelText=", Res.Get<AgentsResources>().BackToItems,
                "&title=", Res.Get<AgentsResources>().PermissionsForAgents);
            DialogElement permissionsDialogElement = DefinitionsHelper.CreateDialogElement(
                agentsGridView.DialogsConfig,
                DefinitionsHelper.PermissionsCommandName,
                "ModulePermissionsDialog",
                permissionsParams);
            agentsGridView.DialogsConfig.Add(permissionsDialogElement);

            #endregion

            #region Links definition

            agentsGridView.LinksConfig.Add(new LinkElement(agentsGridView.LinksConfig)
            {
                Name = "viewSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/agents"
            });

            DefinitionsHelper.CreateNotImplementedLink(agentsGridView);

            #endregion

            backendContentView.ViewsConfig.Add(agentsGridView);

            #endregion

            #region agents backend details view

            var agentsEditDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = AgentsDefinitions.BackendEditViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(AgentsResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Agents.svc/",
                IsToRenderTranslationView = true
            };

            backendContentView.ViewsConfig.Add(agentsEditDetailView);

            #region Versioning Comparioson Screen

            #endregion

            var agentsInsertDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "CreateNewItem",
                ViewName = AgentsDefinitions.BackendInsertViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(AgentsResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Agents.svc/",
                IsToRenderTranslationView = false
            };

            backendContentView.ViewsConfig.Add(agentsInsertDetailView);


            //var previewExternalScripts = DefinitionsHelper.GetExtenalClientScripts(
            //    "Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity",
            //    "OnDetailViewLoaded");


            var agentsPreviewDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = AgentsDefinitions.BackendPreviewViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Read,
                ShowTopToolbar = true,
                ResourceClassId = typeof(AgentsResources).Name,
                ShowNavigation = false,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/Agents.svc/",
                UseWorkflow = false
            };

            backendContentView.ViewsConfig.Add(agentsPreviewDetailView);

            #region agents backend forms definition

            #region Insert Form

            AgentsDefinitions.CreateBackendSections(agentsInsertDetailView, FieldDisplayMode.Write);
            AgentsDefinitions.CreateBackendFormToolbar(agentsInsertDetailView, typeof(AgentsResources).Name, true);

            #endregion

            #region Edit Form

            AgentsDefinitions.CreateBackendSections(agentsEditDetailView, FieldDisplayMode.Write);
            AgentsDefinitions.CreateBackendFormToolbar(agentsEditDetailView, typeof(AgentsResources).Name, false);

            #endregion

            #region Preview History Form



            #endregion

            #region Preview Form
            CreateBackendSections(agentsPreviewDetailView, FieldDisplayMode.Read);
            //TODO: add the preview screen toolbar widgets -->Edit,etc...

            #endregion

            #endregion

            #endregion

            return backendContentView;
        }

        /// <summary>
        /// Creates the action menu widget element.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="wrapperTagName">Name of the wrapper tag.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <returns></returns>
        public static CommandWidgetElement CreateActionMenuCommand(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag wrapperTagKey,
            string commandName,
            string text,
            string resourceClassId)
        {
            return new CommandWidgetElement(parent)
            {
                Name = name,
                WrapperTagKey = wrapperTagKey,
                CommandName = commandName,
                Text = text,
                ResourceClassId = resourceClassId,
                WidgetType = typeof(CommandWidget)
            };
        }

        /// <summary>
        /// Creates the action menu command.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="wrapperTagKey">The wrapper tag key.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class id.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        public static CommandWidgetElement CreateActionMenuCommand(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag wrapperTagKey,
            string commandName,
            string text,
            string resourceClassId,
            string cssClass)
        {
            var commandWidgetElement = DefinitionsHelper.CreateActionMenuCommand(parent, name, wrapperTagKey, commandName, text, resourceClassId);
            commandWidgetElement.CssClass = cssClass;
            return commandWidgetElement;
        }

        /// <summary>
        /// Creates the actions menu separator.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="wrapperTagName">Name of the wrapper tag.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="text">The text.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <returns></returns>
        public static WidgetElement CreateActionMenuSeparator(
            ConfigElement parent,
            string name,
            HtmlTextWriterTag wrapperTagKey,
            string cssClass,
            string text,
            string resourceClassId)
        {
            return new LiteralWidgetElement(parent)
            {
                Name = name,
                WrapperTagKey = wrapperTagKey,
                CssClass = cssClass,
                Text = text,
                ResourceClassId = resourceClassId,
                WidgetType = typeof(LiteralWidget),
                IsSeparator = true
            };
        }

        /// <summary>
        /// Fills the action menu items.
        /// </summary>
        /// <param name="menuItems">The menu items.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        public static void FillActionMenuItems(ConfigElementList<WidgetElement> menuItems, ConfigElement parent, string resourceClassId)
        {
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "View", HtmlTextWriterTag.Li, PreviewCommandName, "View", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Delete", HtmlTextWriterTag.Li, DeleteCommandName, "Delete", resourceClassId, "sfDeleteItm"));
            menuItems.Add(    
                CreateActionMenuCommand(menuItems, "Publish", HtmlTextWriterTag.Li, PublishCommandName, "Publish", resourceClassId, "sfPublishItm"));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Unpublish", HtmlTextWriterTag.Li, UnpublishCommandName, "Unpublish", resourceClassId, "sfUnpublishItm"));
            menuItems.Add(
                CreateActionMenuSeparator(menuItems, "Separator", HtmlTextWriterTag.Li, "sfSeparator", "Edit", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Content", HtmlTextWriterTag.Li, EditCommandName, "Content", resourceClassId));
            menuItems.Add(
                CreateActionMenuCommand(menuItems, "Permissions", HtmlTextWriterTag.Li, PermissionsCommandName, "Permissions", resourceClassId));
        }

        /// <summary>
        /// Creates the toolbar in the backend details form.
        /// </summary>
        /// <param name="detailView">The detail view.</param>
        /// <param name="resourceClassId">The resource class pageId.</param>
        /// <param name="isCreateMode">if set to <c>true</c> the form is in Create mode.</param>
        private static void CreateBackendFormToolbar(DetailFormViewElement detailView, string resourceClassId, bool isCreateMode)
        {
            AgentsDefinitions.CreateBackendFormToolbar(detailView, resourceClassId, isCreateMode, "ThisItem", false, true);
        }

        private static void CreateBackendFormToolbar(DetailFormViewElement detailView, string resourceClassId, bool isCreateMode, string itemName, bool addRevisionHistory, bool showPreview, string backToItems = "BackToItems")
        {
            var toolbarSectionElement = new WidgetBarSectionElement(detailView.Toolbar.Sections)
            {
                Name = "BackendForm",
                WrapperTagKey = HtmlTextWriterTag.Div,
                CssClass = "sfWorkflowMenuWrp"
            };

            // Create 
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "SaveChangesWidgetElement",
                ButtonType = CommandButtonType.Save,
                CommandName = DefinitionsHelper.SaveCommandName,
                Text = (isCreateMode) ? String.Concat("Create", itemName) : "SaveChanges",
                ResourceClassId = resourceClassId,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });

            // Preview
            if (showPreview == true)
            {
                toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
                {
                    Name = "PreviewWidgetElement",
                    ButtonType = CommandButtonType.Standard,
                    CommandName = DefinitionsHelper.PreviewCommandName,
                    Text = "Preview",
                    ResourceClassId = typeof(Labels).Name,
                    WrapperTagKey = HtmlTextWriterTag.Span,
                    WidgetType = typeof(CommandWidget)
                });
            }
            if (!isCreateMode)
            {
                var actionsMenuWidget = new ActionMenuWidgetElement(toolbarSectionElement.Items)
                {
                    Name = "moreActions",
                    Text = "MoreActionsLink",
                    ResourceClassId = resourceClassId,
                    WrapperTagKey = HtmlTextWriterTag.Div,
                    WidgetType = typeof(ActionMenuWidget),
                    CssClass = "sfInlineBlock sfAlignMiddle"
                };
                actionsMenuWidget.MenuItems.Add(new CommandWidgetElement(actionsMenuWidget.MenuItems)
                {
                    Name = DeleteCommandName,
                    Text = "DeleteThisItem",
                    CommandName = DefinitionsHelper.DeleteCommandName,
                    ResourceClassId = resourceClassId,
                    WidgetType = typeof(CommandWidget),
                    CssClass = "sfDeleteItm"
                });

                actionsMenuWidget.MenuItems.Add(new CommandWidgetElement(actionsMenuWidget.MenuItems)
                {
                    Name = PermissionsCommandName,
                    ButtonType = CommandButtonType.SimpleLinkButton,
                    Text = "SetPermissions",
                    CommandName = DefinitionsHelper.PermissionsCommandName,
                    ResourceClassId = resourceClassId,
                    WidgetType = typeof(CommandWidget)
                });
                toolbarSectionElement.Items.Add(actionsMenuWidget);
            }

            // Cancel
            toolbarSectionElement.Items.Add(new CommandWidgetElement(toolbarSectionElement.Items)
            {
                Name = "CancelWidgetElement",
                ButtonType = CommandButtonType.Cancel,
                CommandName = DefinitionsHelper.CancelCommandName,
                Text = backToItems,
                ResourceClassId = resourceClassId,
                WrapperTagKey = HtmlTextWriterTag.Span,
                WidgetType = typeof(CommandWidget)
            });


            detailView.Toolbar.Sections.Add(toolbarSectionElement);
        }

        private static void CreateBackendSections(DetailFormViewElement detailView, FieldDisplayMode displayMode)
        {
            #region Toolbar section

            if (detailView.ViewName == AgentsDefinitions.BackendEditViewName)
            {
                var toolbarSection = new ContentViewSectionElement(detailView.Sections)
                {
                    Name = DefinitionsHelper.ToolbarSectionName
                };

                var languageListFieldElement = new LanguageListFieldElement(toolbarSection.Fields)
                {
                    ID = "languageListField",
                    FieldType = typeof(LanguageListField),
                    ResourceClassId = typeof(LocalizationResources).Name,
                    Title = "OtherTranslationsColon",
                    DisplayMode = displayMode,
                    FieldName = "languageListField",
                    DataFieldName = "AvailableLanguages"
                };
                toolbarSection.Fields.Add(languageListFieldElement);

                detailView.Sections.Add(toolbarSection);
            }

            #endregion

            #region Main section

            var mainSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "MainSection",
                CssClass = "sfFirstForm"
            };

            var titleField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "titleFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "Title.PersistedValue" : "Title",
                DisplayMode = displayMode,
                Title = "lTitle",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(AgentsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
            };
            titleField.ValidatorConfig = new ValidatorDefinitionElement(titleField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "TitleCannotBeEmpty",
                ResourceClassId = typeof(AgentsResources).Name
            };
            mainSection.Fields.Add(titleField);

            if (detailView.ViewName == AgentsDefinitions.BackendEditViewName || detailView.ViewName == AgentsDefinitions.BackendInsertViewName)
            {
                var languageChoiceField = new ChoiceFieldElement(mainSection.Fields)
                {
                    ID = "languageChoiceField",
                    FieldType = typeof(LanguageChoiceField),
                    ResourceClassId = typeof(AgentsResources).Name,
                    Title = "Language",
                    DisplayMode = displayMode,
                    FieldName = "languageField",
                    RenderChoiceAs = RenderChoicesAs.DropDown,
                    MutuallyExclusive = true,
                    DataFieldName = "AvailableLanguages"
                };
                mainSection.Fields.Add(languageChoiceField);
            }

            var emailField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "emailFieldControl",
                DataFieldName = "Email",
                DisplayMode = displayMode,
                Title = "Email",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(AgentsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            emailField.ValidatorConfig = new ValidatorDefinitionElement(emailField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "EmailCannotBeEmpty",
                RegularExpression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
                RegularExpressionViolationMessage = "EmailMustBeValid",
                ResourceClassId = typeof(AgentsResources).Name
            };
            mainSection.Fields.Add(emailField);

            var phoneNumberField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "phoneNumberFieldControl",
                DataFieldName = "PhoneNumber",
                DisplayMode = displayMode,
                Title = "PhoneNumber",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(AgentsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(phoneNumberField);

            var addressField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "addressFieldControl",
                DataFieldName = "Address",
                DisplayMode = displayMode,
                Title = "Address",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(AgentsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(addressField);

            var postalCodeField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "postalCodeFieldControl",
                DataFieldName = "PostalCode",
                DisplayMode = displayMode,
                Title = "PostalCode",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(AgentsResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(postalCodeField);

            detailView.Sections.Add(mainSection);

            #endregion

            #region Categories and Tags
            var taxonSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "TaxonSection",
                Title = "CategoriesAndTags",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfExpandableForm",
                ExpandableDefinitionConfig =
                {
                    Expanded = false
                }
            };

            var categories = DefinitionTemplates.CategoriesFieldWriteMode(taxonSection.Fields);
            categories.DisplayMode = displayMode;
            taxonSection.Fields.Add(categories);

            var tags = DefinitionTemplates.TagsFieldWriteMode(taxonSection.Fields);
            tags.DisplayMode = displayMode;
            tags.CssClass = "sfFormSeparator";
            tags.ExpandableDefinition.Expanded = true;
            tags.Description = "TagsFieldInstructions";
            taxonSection.Fields.Add(tags);

            detailView.Sections.Add(taxonSection);
            #endregion

            #region More options section

            var moreOptionsSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "MoreOptionsSection",
                Title = "MoreOptionsURL",
                ResourceClassId = typeof(AgentsResources).Name,
                CssClass = "sfExpandableForm",
                ExpandableDefinitionConfig =
                {
                    Expanded = false
                }
            };

            if (displayMode == FieldDisplayMode.Write)
            {
                var urlName = new MirrorTextFieldElement(moreOptionsSection.Fields)
                {
                    Title = "UrlName",
                    ID = "urlName",
                    MirroredControlId = titleField.ID,
                    DataFieldName = "UrlName.PersistedValue",
                    DisplayMode = displayMode,
                    RegularExpressionFilter = DefinitionsHelper.UrlRegularExpressionFilter,
                    WrapperTag = HtmlTextWriterTag.Li,
                    ReplaceWith = "-",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                var validationDef = new ValidatorDefinitionElement(urlName)
                {
                    Required = true,
                    MessageCssClass = "sfError",
                    RequiredViolationMessage = "UrlNameCannotBeEmpty",
                    RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForValidator,
                    RegularExpressionViolationMessage = "UrlNameInvalid",
                    ResourceClassId = typeof(AgentsResources).Name
                };
                urlName.ValidatorConfig = validationDef;

                moreOptionsSection.Fields.Add(urlName);
            }

            detailView.Sections.Add(moreOptionsSection);

            #endregion

            #region Sidebar

            if (displayMode == FieldDisplayMode.Write)
            {
                var sidebar = new ContentViewSectionElement(detailView.Sections)
                {
                    Name = DefinitionsHelper.SidebarSectionName,
                    CssClass = "sfItemReadOnlyInfo"
                };

                sidebar.Fields.Add(new ContentWorkflowStatusInfoFieldElement(sidebar.Fields)
                {
                    DisplayMode = displayMode,
                    FieldName = "NewsWorkflowStatusInfoField",
                    ResourceClassId = typeof(AgentsResources).Name,
                    WrapperTag = HtmlTextWriterTag.Li,
                    FieldType = typeof(ContentWorkflowStatusInfoField)
                });

                detailView.Sections.Add(sidebar);
            }

            #endregion
        }

        #region Constants

        #region Backend

        /// <summary>
        /// Name of the backend definition (all backend interface)
        /// </summary>
        public const string BackendDefinitionName = "AgentsBackend";
        /// <summary>
        /// Name of the the list view in the backend definition
        /// </summary>
        public const string BackendListViewName = "AgentsBackendList";
        /// <summary>
        /// Name of the backend view that edits product items
        /// </summary>
        public const string BackendEditViewName = "AgentsBackendEdit";
        /// <summary>
        /// Name of the backend view that creates (inserts) backend products
        /// </summary>
        public const string BackendInsertViewName = "AgentsBackendInsert";
        /// <summary>
        /// Name of the backend view that previews an item before it is saved
        /// </summary>
        public const string BackendPreviewViewName = "AgentsBackendPreview";
        /// <summary>
        /// Name of the backend view that previes history (version) items
        /// </summary>
        public const string BackendVersionPreviewViewName = "AgentsBackendVersionPreview";
        /// <summary>
        /// Name of the backend view that compares two history items
        /// </summary>
        public const string BackendVersionCompareViewName = "AgentsBackendVersionComparisonView";

        /// <summary>
        /// Query string parameter for revision history dialog
        /// </summary>
        private const string ComparisonViewHistoryScreenQueryParameter = "VersionComparisonView";

        #endregion


        #region Frontend Definitions

        /// <summary>
        /// Defines the ContentView control for News on the frontend
        /// </summary>
        /// <param name="parent">The parent configuration element.</param>
        /// <returns>A configured instance of <see cref="ContentViewControlElement"/>.</returns>
        internal static ContentViewControlElement DefineAgentsFrontendContentView(ConfigElement parent)
        {
            // define content view control
            var controlDefinition = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = AgentsDefinitions.FrontendDefinitionName,
                ContentType = typeof(AgentItem)
            };

            // *** define views ***

            #region Agents backend list view

            var agentsListView = new ContentViewMasterElement(controlDefinition.ViewsConfig)
            {
                ViewName = AgentsDefinitions.FrontendListViewName,
                ViewType = typeof(MasterListView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 6,
                ResourceClassId = typeof(AgentsResources).Name,
                FilterExpression = DefinitionsHelper.PublishedOrScheduledFilterExpression,
                SortExpression = "Title ASC"
            };

            controlDefinition.ViewsConfig.Add(agentsListView);

            #endregion

            #region Agents backend details view

            var newsDetailsView = new ContentViewDetailElement(controlDefinition.ViewsConfig)
            {
                ViewName = AgentsDefinitions.FrontendDetailViewName,
                ViewType = typeof(DetailsView),
                ShowSections = false,
                DisplayMode = FieldDisplayMode.Read,
                ResourceClassId = typeof(AgentsResources).Name
            };

            controlDefinition.ViewsConfig.Add(newsDetailsView);

            #endregion

            //#region Dialogs definition

            //AgentsDefinitions.CreateDialogs(controlDefinition.DialogsConfig);

            //#endregion

            return controlDefinition;
        }



        /// <summary>
        /// Frontend definitoin name
        /// </summary>
        public const string FrontendDefinitionName = "AgentsFrontend";
        /// <summary>
        /// Detail view definition
        /// </summary>
        public const string FrontendDetailViewName = "AgentsFrontendDetails";
        /// <summary>
        /// Master view definition
        /// </summary>
        public const string FrontendListViewName = "AgentsFrontendList";

        /// <summary>
        /// Name of the view that displays only titles
        /// </summary>
        public const string FrontendDefaultListViewName = "List agents";
        public const string FrontendDefaultDetailViewName = "Full agent";

        public const string ModuleName = "Agents";

        #endregion

        #region Commands
        /// <summary>
        /// Common name for the command used to create a new item.
        /// </summary>
        public const string CreateCommandName = "create";

        /// <summary>
        /// Common name for the command used to save an item.
        /// </summary>
        public const string SaveCommandName = "save";

        /// <summary>
        /// Common name for a command used to cancel an operation.
        /// </summary>
        public const string CancelCommandName = "cancel";

        /// <summary>
        /// Common name used for a command that saves an item and resets the entry form.
        /// </summary>
        public const string SaveAndContinueCommandName = "saveAndContinue";

        /// <summary>
        /// Common name used for a command that publishes an item.
        /// </summary>
        public const string PublishCommandName = "publish";

        /// <summary>
        /// Common name used for a command that unpublishes an item.
        /// </summary>
        public const string UnpublishCommandName = "unpublish";

        /// <summary>
        /// Common name used for a command that performs a batch publish
        /// </summary>
        public const string GroupPublishCommandName = "groupPublish";

        /// <summary>
        /// Common name used for a command that edits an item.
        /// </summary>
        public const string EditCommandName = "edit";
        /// <summary>
        /// Common name used for a command that performs a batch unpublish
        /// </summary>
        public const string GroupUnpublishCommandName = "groupUnpublish";

        /// <summary>
        /// Common name used for a command that delets an item.
        /// </summary>
        public const string DeleteCommandName = "delete";

        public const string HistoryCommandName = "history";

        /// <summary>
        /// Common name used for a command that opens an item in a preview mode.
        /// </summary>
        public const string PreviewCommandName = "preview";

        /// <summary>
        /// Common name used for a command that shows the permissions for a given secured
        /// object.
        /// </summary>
        public const string PermissionsCommandName = "permissions";
        #endregion

        #endregion

    }
}
