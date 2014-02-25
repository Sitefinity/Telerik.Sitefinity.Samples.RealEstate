using System;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.StarterKit.Modules.RealEstate.Web.UI.Controls;
using Telerik.StarterKit.Modules.RealEstate.Web.UI.Public;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI
{
    /// <summary>
    /// This is a static class used to initialize the properties for all ContentView control views
    /// of supplied by default for the real estate module.
    /// </summary>
    public class RealEstateDefinitions
    {
        /// <summary>
        /// Static constructor that makes it impossible to use the definitions
        /// without the module 
        /// </summary>
        static RealEstateDefinitions()
        {
            // Ensure Real Estate module is initialized.
            SystemManager.GetApplicationModule(RealEstateModule.ModuleName);
        }

        public static ContentViewControlElement DefineRealEstateBackendContentView(ConfigElement parent)
        {
            // define content view control
            var backendContentView = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = BackendDefinitionName,
                ContentType = typeof(RealEstateItem)
            };

            // *** define views ***

            #region Real Estate items backend list view

            var itemsGridView = new MasterGridViewElement(backendContentView.ViewsConfig)
            {
                ViewName = RealEstateDefinitions.BackendListViewName,
                ViewType = typeof(MasterGridView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 50,
                ResourceClassId = typeof(RealEstateResources).Name,
                SearchFields = "Title",
                SortExpression = "Title ASC",
                Title = "RealEstateViewTitle",
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/RealEstate.svc/"
            };

            #region Toolbar definition

            WidgetBarSectionElement masterViewToolbarSection = new WidgetBarSectionElement(itemsGridView.ToolbarConfig.Sections)
            {
                Name = "toolbar"
            };


            var createWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "CreateWidget",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfMainAction",
                WidgetType = typeof(CommandWidget),
                PermissionSet = RealEstateConstants.Security.PermissionSetName,
                ActionName = RealEstateConstants.Security.Create
            };
            masterViewToolbarSection.Items.Add(createWidget);

            var deleteWidget = new CommandWidgetElement(masterViewToolbarSection.Items)
            {
                Name = "DeleteWidget",
                ButtonType = CommandButtonType.Standard,
                CommandName = DefinitionsHelper.GroupDeleteCommandName,
                Text = "Delete",
                ResourceClassId = typeof(RealEstateResources).Name,
                WidgetType = typeof(CommandWidget),
                CssClass = "sfGroupBtn"
            };
            masterViewToolbarSection.Items.Add(deleteWidget);


            masterViewToolbarSection.Items.Add(DefinitionsHelper.CreateSearchButtonWidget(masterViewToolbarSection.Items, typeof(RealEstateItem)));

            itemsGridView.ToolbarConfig.Sections.Add(masterViewToolbarSection);

            #endregion

            #region Sidebar definition

            var languagesSection = new LocalizationWidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
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

            WidgetBarSectionElement sidebarSection = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "Filter",
                Title = "FilterItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
                WrapperTagId = "filterSection"
            };

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "AllItems",
                CommandName = DefinitionsHelper.ShowAllItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "AllItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false,
                ButtonCssClass = "sfSel",
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "MyItems",
                CommandName = DefinitionsHelper.ShowMyItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "MyItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "DraftItems",
                CommandName = DefinitionsHelper.ShowMasterItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "DraftItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "PublishedItems",
                CommandName = DefinitionsHelper.ShowPublishedItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PublishedItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "ScheduledItems",
                CommandName = DefinitionsHelper.ShowScheduledItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "ScheduledItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "",
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            sidebarSection.Items.Add(new CommandWidgetElement(sidebarSection.Items)
            {
                Name = "WaitingForApproval",
                CommandName = DefinitionsHelper.PendingApprovalItemsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "WaitingForApproval",
                ResourceClassId = typeof(RealEstateResources).Name,
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

            var categoryFilterSection = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "ItemsByCategory",
                Title = "ItemsByCategory",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "categoryFilterSection",
                Visible = false
            };
            itemsGridView.SidebarConfig.Sections.Add(categoryFilterSection);

            var tagFilterSection = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "ItemsByTag",
                Title = "ItemsByTag",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfFilterBy sfSeparator",
                WrapperTagId = "tagFilterSection",
                Visible = false
            };
            itemsGridView.SidebarConfig.Sections.Add(tagFilterSection);

            var dateFilterSection = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "DisplayLastUpdatedItems",
                Title = "DisplayLastUpdatedItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfFilterBy sfFilterByDate sfSeparator",
                WrapperTagId = "dateFilterSection",
                Visible = false
            };
            itemsGridView.SidebarConfig.Sections.Add(dateFilterSection);

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

            categoryFilterWidget.UrlParameters.Add("itemType", typeof(RealEstateItem).AssemblyQualifiedName);
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
            tagFilterWidget.UrlParameters.Add("itemType", typeof(RealEstateItem).AssemblyQualifiedName);

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
                ResourceClassId = typeof(RealEstateResources).Name,
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
                ResourceClassId = typeof(RealEstateResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            });

            #endregion Filter by date

            WidgetBarSectionElement manageAlso = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "ManageAlso",
                Title = "ManageAlso",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfWidgetsList sfSeparator",
                WrapperTagId = "manageAlsoSection"
            };

            WidgetBarSectionElement settings = new WidgetBarSectionElement(itemsGridView.SidebarConfig.Sections)
            {
                Name = "Settings",
                Title = "Settings",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfWidgetsList sfSettings",
                WrapperTagId = "settingsSection"
            };

            CommandWidgetElement itemsPermissions = new CommandWidgetElement(settings.Items)
            {
                Name = "itemsPermissions",
                CommandName = DefinitionsHelper.PermissionsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "PermissionsForItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(itemsPermissions);

            CommandWidgetElement itemsSettings = new CommandWidgetElement(settings.Items)
            {
                Name = "itemsSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                ButtonType = CommandButtonType.SimpleLinkButton,
                Text = "SettingsForItems",
                ResourceClassId = typeof(RealEstateResources).Name,
                WidgetType = typeof(CommandWidget),
                IsSeparator = false
            };
            settings.Items.Add(itemsSettings);



            itemsGridView.SidebarConfig.Title = "ManageItems";
            itemsGridView.SidebarConfig.Sections.Add(languagesSection);
            itemsGridView.SidebarConfig.Sections.Add(sidebarSection);
            itemsGridView.SidebarConfig.Sections.Add(manageAlso);
            itemsGridView.SidebarConfig.Sections.Add(settings);

            #endregion

            #region ContextBar definition

            var translationsContextBarSection = new LocalizationWidgetBarSectionElement(itemsGridView.ContextBarConfig.Sections)
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

            itemsGridView.ContextBarConfig.Sections.Add(translationsContextBarSection);

            #endregion

            #region Grid View Mode

            var gridMode = new GridViewModeElement(itemsGridView.ViewModesConfig)
            {
                Name = "Grid"
            };
            itemsGridView.ViewModesConfig.Add(gridMode);

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
            
            DataColumnElement itemNumberColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "ItemNumber",
                HeaderText = "ItemNumber",
                ClientTemplate = "<span>{{ItemNumber}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            gridMode.ColumnsConfig.Add(itemNumberColumn);

            DataColumnElement addressColumn = new DataColumnElement(gridMode.ColumnsConfig)
            {
                Name = "Address",
                HeaderText = "Address",
                ClientTemplate = "<span>{{Address}}</span>",
                HeaderCssClass = "sfRegular",
                ItemCssClass = "sfRegular",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            gridMode.ColumnsConfig.Add(addressColumn);

            //DataColumnElement phoneNumberColumn = new DataColumnElement(gridMode.ColumnsConfig)
            //{
            //    Name = "Price",
            //    HeaderText = "PhoneNumber",
            //    ClientTemplate = "<span>{{PhoneNumber}}</span>",
            //    HeaderCssClass = "sfRegular",
            //    ItemCssClass = "sfRegular"
            //};
            //gridMode.ColumnsConfig.Add(phoneNumberColumn);


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
            FillActionMenuItems(actionsColumn.MenuItems, actionsColumn, typeof(RealEstateResources).Name);
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

            DecisionScreenElement dsElement = new DecisionScreenElement(itemsGridView.DecisionScreensConfig)
            {
                Name = "NoItemsScreen",
                DecisionType = DecisionType.NoItemsExist,
                MessageType = MessageType.Neutral,
                Displayed = false,
                Title = "WhatDoYouWantToDoNow",
                MessageText = "NoItems",
                ResourceClassId = typeof(RealEstateResources).Name
            };

            CommandWidgetElement actionCreateNew = new CommandWidgetElement(dsElement.Actions)
            {
                Name = "Create",
                ButtonType = CommandButtonType.Create,
                CommandName = DefinitionsHelper.CreateCommandName,
                Text = "CreateItem",
                ResourceClassId = typeof(RealEstateResources).Name,
                CssClass = "sfCreateItem",
                PermissionSet = RealEstateConstants.Security.PermissionSetName,
                ActionName = RealEstateConstants.Security.Create
            };
            dsElement.Actions.Add(actionCreateNew);

            itemsGridView.DecisionScreensConfig.Add(dsElement);

            #endregion

            #region Dialogs definition

            var parameters = string.Concat(
                "?ControlDefinitionName=",
                RealEstateDefinitions.BackendDefinitionName,
                "&ViewName=",
                RealEstateDefinitions.BackendInsertViewName);
            DialogElement createDialogElement = DefinitionsHelper.CreateDialogElement(
                itemsGridView.DialogsConfig,
                DefinitionsHelper.CreateCommandName,
                "ContentViewInsertDialog",
                parameters);
            itemsGridView.DialogsConfig.Add(createDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                RealEstateDefinitions.BackendDefinitionName,
                "&ViewName=",
                RealEstateDefinitions.BackendEditViewName);
            DialogElement editDialogElement = DefinitionsHelper.CreateDialogElement(
                itemsGridView.DialogsConfig,
                DefinitionsHelper.EditCommandName,
                "ContentViewEditDialog",
                parameters);
            itemsGridView.DialogsConfig.Add(editDialogElement);

            parameters = string.Concat(
                "?ControlDefinitionName=",
                RealEstateDefinitions.BackendDefinitionName,
                "&ViewName=",
                RealEstateDefinitions.BackendPreviewViewName,
                "&backLabelText=", Res.Get<RealEstateResources>().BackToItems, "&SuppressBackToButtonLabelModify=true");
            DialogElement previewDialogElement = DefinitionsHelper.CreateDialogElement(
                itemsGridView.DialogsConfig,
                DefinitionsHelper.PreviewCommandName,
                "ContentViewEditDialog",
                parameters);
            itemsGridView.DialogsConfig.Add(previewDialogElement);

            string permissionsParams = string.Concat(
                "?moduleName=", RealEstateDefinitions.ModuleName,
                "&typeName=", typeof(RealEstateItem).AssemblyQualifiedName,
                "&backLabelText=", Res.Get<RealEstateResources>().BackToItems,
                "&title=", Res.Get<RealEstateResources>().PermissionsForItems);
            DialogElement permissionsDialogElement = DefinitionsHelper.CreateDialogElement(
                itemsGridView.DialogsConfig,
                DefinitionsHelper.PermissionsCommandName,
                "ModulePermissionsDialog",
                permissionsParams);
            itemsGridView.DialogsConfig.Add(permissionsDialogElement);

            #endregion

            #region Links definition

            itemsGridView.LinksConfig.Add(new LinkElement(itemsGridView.LinksConfig)
            {
                Name = "viewSettings",
                CommandName = DefinitionsHelper.SettingsCommandName,
                NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/item"
            });

            DefinitionsHelper.CreateNotImplementedLink(itemsGridView);

            #endregion

            backendContentView.ViewsConfig.Add(itemsGridView);

            #endregion

            #region Real Estate items backend details view

            var itemEditDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = RealEstateDefinitions.BackendEditViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(RealEstateResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/RealEstate.svc/",
                IsToRenderTranslationView = true
            };

            backendContentView.ViewsConfig.Add(itemEditDetailView);

            #region Versioning Comparioson Screen

            #endregion

            var itemInsertDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "CreateNewItem",
                ViewName = RealEstateDefinitions.BackendInsertViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Write,
                ShowTopToolbar = true,
                ResourceClassId = typeof(RealEstateResources).Name,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/RealEstate.svc/",
                IsToRenderTranslationView = false
            };

            backendContentView.ViewsConfig.Add(itemInsertDetailView);


            //var previewExternalScripts = DefinitionsHelper.GetExtenalClientScripts(
            //    "Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity",
            //    "OnDetailViewLoaded");


            var itemPreviewDetailView = new DetailFormViewElement(backendContentView.ViewsConfig)
            {
                Title = "EditItem",
                ViewName = RealEstateDefinitions.BackendPreviewViewName,
                ViewType = typeof(DetailFormView),
                ShowSections = true,
                DisplayMode = FieldDisplayMode.Read,
                ShowTopToolbar = true,
                ResourceClassId = typeof(RealEstateResources).Name,
                ShowNavigation = false,
                WebServiceBaseUrl = "~/Sitefinity/Services/Content/RealEstate.svc/",
                UseWorkflow = false

            };

            backendContentView.ViewsConfig.Add(itemPreviewDetailView);

            #region Real Estate items backend forms definition

            #region Insert Form

            RealEstateDefinitions.CreateBackendSections(itemInsertDetailView, FieldDisplayMode.Write);
            RealEstateDefinitions.CreateBackendFormToolbar(itemInsertDetailView, typeof(RealEstateResources).Name, true);

            #endregion

            #region Edit Form

            RealEstateDefinitions.CreateBackendSections(itemEditDetailView, FieldDisplayMode.Write);
            RealEstateDefinitions.CreateBackendFormToolbar(itemEditDetailView, typeof(RealEstateResources).Name, false);

            #endregion

            #region Preview History Form



            #endregion

            #region Preview Form
            CreateBackendSections(itemPreviewDetailView, FieldDisplayMode.Read);
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
            HtmlTextWriterTag WrapperTagKey,
            string cssClass,
            string text,
            string resourceClassId)
        {
            return new LiteralWidgetElement(parent)
            {
                Name = name,
                WrapperTagKey = WrapperTagKey,
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
            RealEstateDefinitions.CreateBackendFormToolbar(detailView, resourceClassId, isCreateMode, "ThisItem", false, true);
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

            if (detailView.ViewName == RealEstateDefinitions.BackendEditViewName)
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

            #region Title
            var titleField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "titleFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "Title.PersistedValue" : "Title",
                DisplayMode = displayMode,
                Title = "lTitle",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
            };
            titleField.ValidatorConfig = new ValidatorDefinitionElement(titleField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "TitleCannotBeEmpty",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(titleField); 
            #endregion

            #region Language
            if (detailView.ViewName == RealEstateDefinitions.BackendEditViewName || detailView.ViewName == RealEstateDefinitions.BackendInsertViewName)
            {
                var languageChoiceField = new ChoiceFieldElement(mainSection.Fields)
                {
                    ID = "languageChoiceField",
                    FieldType = typeof(LanguageChoiceField),
                    ResourceClassId = typeof(RealEstateResources).Name,
                    Title = "Language",
                    DisplayMode = displayMode,
                    FieldName = "languageField",
                    RenderChoiceAs = RenderChoicesAs.DropDown,
                    MutuallyExclusive = true,
                    DataFieldName = "AvailableLanguages"
                };
                mainSection.Fields.Add(languageChoiceField);
            } 
            #endregion

            #region Item Number
            var itemNumberField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "itemNumberFieldControl",
                DataFieldName = "ItemNumber",
                DisplayMode = displayMode,
                Title = "ItemNumber",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
            };
            itemNumberField.ValidatorConfig = new ValidatorDefinitionElement(itemNumberField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "ItemNumberCannotBeEmpty",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(itemNumberField);
            #endregion

            #region Description
            var descriptionField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "descriptionFieldControl",
                DataFieldName = (displayMode == FieldDisplayMode.Write) ? "Description.PersistedValue" : "Description",
                DisplayMode = displayMode,
                Title = "Description",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
                Rows = 10
            };
            mainSection.Fields.Add(descriptionField);
            #endregion

            #region Address
            var addressField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "addressFieldControl",
                DataFieldName = "Address",
                DisplayMode = displayMode,
                Title = "Address",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            addressField.ValidatorConfig = new ValidatorDefinitionElement(addressField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "AddressCannotBeEmpty",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(addressField); 
            #endregion

            #region Postal Code
            var postalCodeField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "postalCodeFieldControl",
                DataFieldName = "PostalCode",
                DisplayMode = displayMode,
                Title = "PostalCode",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            postalCodeField.ValidatorConfig = new ValidatorDefinitionElement(postalCodeField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "PostalCodeCannotBeEmpty",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(postalCodeField); 
            #endregion

            #region Housing
            var housingField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "housingFieldControl",
                DataFieldName = "Housing",
                DisplayMode = displayMode,
                Title = "Housing",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            housingField.ValidatorConfig = new ValidatorDefinitionElement(housingField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "HousingCannotBeEmpty",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(housingField);
            #endregion

            #region Number of Rooms
            var roomsField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "roomsFieldControl",
                DataFieldName = "NumberOfRooms",
                DisplayMode = displayMode,
                Title = "NumberOfRooms",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(roomsField);
            #endregion

            #region Number of Floors
            var floorsField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "floorsFieldControl",
                DataFieldName = "NumberOfFloors",
                DisplayMode = displayMode,
                Title = "Floors",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(floorsField);
            #endregion

            #region Year Built
            var yearBuiltField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "yearBuiltFieldControl",
                DataFieldName = "YearBuilt",
                DisplayMode = displayMode,
                Title = "YearBuilt",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            mainSection.Fields.Add(yearBuiltField);
            #endregion

            #region Price
            var priceField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "priceFieldControl",
                DataFieldName = "Price",
                DisplayMode = displayMode,
                Title = "Price",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            priceField.ValidatorConfig = new ValidatorDefinitionElement(priceField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "PriceCannotBeEmpty",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "PriceIsNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(priceField);
            #endregion

            #region Payment
            var paymentField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "paymentFieldControl",
                DataFieldName = "Payment",
                DisplayMode = displayMode,
                Title = "Payment",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            paymentField.ValidatorConfig = new ValidatorDefinitionElement(paymentField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "PaymentCannotBeEmpty",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "PaymentIsNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(paymentField);
            #endregion

            #region Monthly Rate
            var monthlyRateField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "monthlyRateFieldControl",
                DataFieldName = "MonthlyRate",
                DisplayMode = displayMode,
                Title = "MonthlyRate",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            monthlyRateField.ValidatorConfig = new ValidatorDefinitionElement(monthlyRateField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "MonthlyRateCannotBeEmpty",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "MonthlyRateIsNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(monthlyRateField);
            #endregion

            #region Net
            var netField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "netFieldControl",
                DataFieldName = "Net",
                DisplayMode = displayMode,
                Title = "Net",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            netField.ValidatorConfig = new ValidatorDefinitionElement(netField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "NetCannotBeEmpty",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "NetIsNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(netField);
            #endregion

            #region Price Square Meter
            var priceSquareMeterField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "priceSquareMeterFieldControl",
                DataFieldName = "PriceSquareMeter",
                DisplayMode = displayMode,
                Title = "PriceSquareMeter",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            priceSquareMeterField.ValidatorConfig = new ValidatorDefinitionElement(priceSquareMeterField)
            {
                Required = true,
                MessageCssClass = "sfError",
                RequiredViolationMessage = "PriceSquareMeterCannotBeEmpty",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "PriceSquareMeterIsNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(priceSquareMeterField);
            #endregion

            #region Latitude
            var latitudeField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "latitudeFieldControl",
                DataFieldName = "Latitude",
                DisplayMode = displayMode,
                Title = "Latitude",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            latitudeField.ValidatorConfig = new ValidatorDefinitionElement(latitudeField)
            {
                Required = false,
                MessageCssClass = "sfError",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "LatitudeNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(latitudeField);
            #endregion

            #region Longitude
            var longitudeField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "longitudeFieldControl",
                DataFieldName = "Longitude",
                DisplayMode = displayMode,
                Title = "Longitude",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li
            };
            longitudeField.ValidatorConfig = new ValidatorDefinitionElement(longitudeField)
            {
                Required = false,
                MessageCssClass = "sfError",
                RegularExpression = @"^\d*\.?\d*$",
                RegularExpressionViolationMessage = "LongitudeNotValid",
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(longitudeField);
            #endregion

            #region Agent
            var agentIdField = new TextFieldDefinitionElement(mainSection.Fields)
            {
                ID = "agentIdFieldControl",
                DataFieldName = "AgentId",
                DisplayMode = displayMode,
                Title = "Agent",
                CssClass = "sfTitleField",
                ResourceClassId = typeof(RealEstateResources).Name,
                WrapperTag = HtmlTextWriterTag.Li,
                FieldType = typeof(AgentsDropDown)
            };
            mainSection.Fields.Add(agentIdField);
            #endregion

            #region Type Taxonomy
            var typeTaxonomyField = new TaxonFieldDefinitionElement(mainSection.Fields)
            {
                ID = "typeTaxonomyField",
                Title = "FlatType",
                DataFieldName = "Types",
                TaxonomyId = RealEstateModule.TypesTaxonomyId,
                FieldType = typeof(TaxonomyDropDown),
                DisplayMode = displayMode,
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(typeTaxonomyField);
            #endregion

            #region Location Taxonomy
            var locationTaxonomyField = new TaxonFieldDefinitionElement(mainSection.Fields)
            {
                ID = "locationTaxonomyField",
                Title = "Location",
                DataFieldName = "Locations",
                TaxonomyId = RealEstateModule.LocationsTaxonomyId,
                FieldType = typeof(TaxonomyDropDown),
                DisplayMode = displayMode,
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(locationTaxonomyField); 
            #endregion

            #region Rooms Taxonomy
            var roomsTaxonomyField = new TaxonFieldDefinitionElement(mainSection.Fields)
            {
                ID = "roomsTaxonomyField",
                Title = "Rooms",
                DataFieldName = "Rooms",
                TaxonomyId = RealEstateModule.RoomsTaxonomyId,
                FieldType = typeof(TaxonomyCheckBox),
                DisplayMode = displayMode,
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(roomsTaxonomyField);
            #endregion

            #region Features Taxonomy
            var featuresTaxonomyField = new TaxonFieldDefinitionElement(mainSection.Fields)
            {
                ID = "featuresTaxonomyField",
                Title = "Features",
                DataFieldName = "Features",
                TaxonomyId = RealEstateModule.FeaturesTaxonomyId,
                FieldType = typeof(TaxonomyCheckBox),
                DisplayMode = displayMode,
                ResourceClassId = typeof(RealEstateResources).Name
            };
            mainSection.Fields.Add(featuresTaxonomyField);
            #endregion

            detailView.Sections.Add(mainSection);

            #endregion

            #region Categories and Tags
            var taxonSection = new ContentViewSectionElement(detailView.Sections)
            {
                Name = "TaxonSection",
                Title = "CategoriesAndTags",
                ResourceClassId = typeof(RealEstateResources).Name,
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
                ResourceClassId = typeof(RealEstateResources).Name,
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
                    ResourceClassId = typeof(RealEstateResources).Name
                };
                var validationDef = new ValidatorDefinitionElement(urlName)
                {
                    Required = true,
                    MessageCssClass = "sfError",
                    RequiredViolationMessage = "UrlNameCannotBeEmpty",
                    RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForValidator,
                    RegularExpressionViolationMessage = "UrlNameInvalid",
                    ResourceClassId = typeof(RealEstateResources).Name
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
                    ResourceClassId = typeof(RealEstateResources).Name,
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
        public const string BackendDefinitionName = "RealEstateBackend";
        /// <summary>
        /// Name of the the list view in the backend definition
        /// </summary>
        public const string BackendListViewName = "RealEstateBackendList";
        /// <summary>
        /// Name of the backend view that edits product items
        /// </summary>
        public const string BackendEditViewName = "RealEstateBackendEdit";
        /// <summary>
        /// Name of the backend view that creates (inserts) backend products
        /// </summary>
        public const string BackendInsertViewName = "RealEstateBackendInsert";
        /// <summary>
        /// Name of the backend view that previews an item before it is saved
        /// </summary>
        public const string BackendPreviewViewName = "RealEstateBackendPreview";
        /// <summary>
        /// Name of the backend view that previes history (version) items
        /// </summary>
        public const string BackendVersionPreviewViewName = "RealEstateBackendVersionPreview";
        /// <summary>
        /// Name of the backend view that compares two history items
        /// </summary>
        public const string BackendVersionCompareViewName = "RealEstateBackendVersionComparisonView";

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
        internal static ContentViewControlElement DefineRealEstateFrontendContentView(ConfigElement parent)
        {
            // define content view control
            var controlDefinition = new ContentViewControlElement(parent)
            {
                ControlDefinitionName = RealEstateDefinitions.FrontendDefinitionName,
                ContentType = typeof(RealEstateItem)
            };

            // *** define views ***

            #region Real Estate items backend list view

            var itemsListView = new ContentViewMasterElement(controlDefinition.ViewsConfig)
            {
                ViewName = RealEstateDefinitions.FrontendListViewName,
                ViewType = typeof(MasterListView),
                AllowPaging = true,
                DisplayMode = FieldDisplayMode.Read,
                ItemsPerPage = 6,
                ResourceClassId = typeof(RealEstateResources).Name,
                FilterExpression = DefinitionsHelper.PublishedOrScheduledFilterExpression,
                SortExpression = "Title ASC"
            };

            controlDefinition.ViewsConfig.Add(itemsListView);

            #endregion

            #region Real Estate items backend details view

            var newsDetailsView = new ContentViewDetailElement(controlDefinition.ViewsConfig)
            {
                ViewName = RealEstateDefinitions.FrontendDetailViewName,
                ViewType = typeof(DetailsView),
                ShowSections = false,
                DisplayMode = FieldDisplayMode.Read,
                ResourceClassId = typeof(RealEstateResources).Name
            };

            controlDefinition.ViewsConfig.Add(newsDetailsView);

            #endregion

            //#region Dialogs definition

            //RealEstateDefinitions.CreateDialogs(controlDefinition.DialogsConfig);

            //#endregion

            return controlDefinition;
        }



        /// <summary>
        /// Frontend definitoin name
        /// </summary>
        public const string FrontendDefinitionName = "RealEstateFrontend";
        /// <summary>
        /// Detail view definition
        /// </summary>
        public const string FrontendDetailViewName = "RealEstateFrontendDetails";
        /// <summary>
        /// Master view definition
        /// </summary>
        public const string FrontendListViewName = "RealEstateFrontendList";

        /// <summary>
        /// Name of the view that displays only titles
        /// </summary>
        public const string FrontendDefaultListViewName = "List items";
        public const string FrontendDefaultDetailViewName = "Full item";

        public const string ModuleName = "RealEstate";

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
