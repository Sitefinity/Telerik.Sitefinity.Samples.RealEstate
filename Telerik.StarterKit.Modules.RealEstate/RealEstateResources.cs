using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.StarterKit.Modules.RealEstate
{
    /// <summary>
    /// Resource class for the procuts module
    /// </summary>
    [ObjectInfo(typeof(RealEstateResources), Title = "RealEstateResourcesTitle", Description = "RealEstateResourcesDescription")]
    public class RealEstateResources : Resource
    {
        
        #region Constructions
        
        /// <summary>
        /// Initializes new instance of <see cref="RealEstateResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public RealEstateResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="RealEstateResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public RealEstateResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// The title of this class
        /// </summary>
        [ResourceEntry("RealEstateResourcesTitle",
            Value = "Real Estate",
            Description = "The title of this class.",
            LastModified = "2010/02/11")]
        public string RealEstateResourcesTitle
        {
            get { return this["RealEstateResourcesTitle"]; }
        }

        /// <summary>
        /// The description of this class
        /// </summary>
        [ResourceEntry("RealEstateResourcesDescription",
            Value = "Contains localizable resources for Real Estate module.",
            Description = "The description of this class.",
            LastModified = "2010/12/01")]
        public string RealEstateResourcesDescription
        {
            get { return this["RealEstateResourcesDescription"]; }
        }

        #endregion

        #region Backend

        /// <summary>
        /// Title of the DesignerContentTitleText
        /// </summary>
        [ResourceEntry("DesignerContentTitleText",
            Value = "Which items to display?",
            Description = "DesignerContentTitleText",
            LastModified = "2010/12/07")]
        public string DesignerContentTitleText
        {
            get
            {
                return this["DesignerContentTitleText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseAllText
        /// </summary>
        [ResourceEntry("DesignerChooseAllText",
            Value = "Choose all",
            Description = "DesignerChooseAllText",
            LastModified = "2010/12/07")]
        public string DesignerChooseAllText
        {
            get
            {
                return this["DesignerChooseAllText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseSingleText
        /// </summary>
        [ResourceEntry("DesignerChooseSingleText",
            Value = "Choose single",
            Description = "DesignerChooseSingleText",
            LastModified = "2010/12/07")]
        public string DesignerChooseSingleText
        {
            get
            {
                return this["DesignerChooseSingleText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseSimpleFilterText
        /// </summary>
        [ResourceEntry("DesignerChooseSimpleFilterText",
            Value = "Choose simple filter",
            Description = "DesignerChooseSimpleFilterText",
            LastModified = "2010/12/07")]
        public string DesignerChooseSimpleFilterText
        {
            get
            {
                return this["DesignerChooseSimpleFilterText"];
            }
        }

        /// <summary>
        /// Title of the DesignerChooseAdvancedFilterText
        /// </summary>
        [ResourceEntry("DesignerChooseAdvancedFilterText",
            Value = "Choose advanced filter",
            Description = "DesignerChooseAdvancedFilterText",
            LastModified = "2010/12/07")]
        public string DesignerChooseAdvancedFilterText
        {
            get
            {
                return this["DesignerChooseAdvancedFilterText"];
            }
        }

        /// <summary>
        /// Title of the DesignerNoContentToSelectText
        /// </summary>
        [ResourceEntry("DesignerNoContentToSelectText",
            Value = "No content to select",
            Description = "DesignerNoContentToSelectText",
            LastModified = "2010/12/07")]
        public string DesignerNoContentToSelectText
        {
            get
            {
                return this["DesignerNoContentToSelectText"];
            }
        }

        /// <summary>
        /// Title of the DesignerContentSelectorTitleText
        /// </summary>
        [ResourceEntry("DesignerContentSelectorTitleText",
            Value = "Content selector title",
            Description = "DesignerContentSelectorTitleText",
            LastModified = "2010/12/07")]
        public string DesignerContentSelectorTitleText
        {
            get
            {
                return this["DesignerContentSelectorTitleText"];
            }
        }

        /// <summary>
        /// Title of the DesignerListSettingsSortItemstext
        /// </summary>
        [ResourceEntry("DesignerListSettingsSortItemstext",
            Value = "Sort items",
            Description = "DesignerListSettingsSortItemstext",
            LastModified = "2010/12/07")]
        public string DesignerListSettingsSortItemstext
        {
            get
            {
                return this["DesignerListSettingsSortItemstext"];
            }
        }

        /// <summary>
        /// Title of the ManageItems
        /// </summary>
        [ResourceEntry("ManageItems",
            Value = "Manage Items",
            Description = "ManageItems",
            LastModified = "2010/12/07")]
        public string ManageItems
        {
            get
            {
                return this["ManageItems"];
            }
        }


        #region Security-related resources

        /// <summary>
        /// Title of the Real Estate permissions set
        /// </summary>
        [ResourceEntry("RealEstatePermissions",
            Value = "Real Estate",
            Description = "Title of the Real Estate permissions set",
            LastModified = "2010/12/03")]
        public string RealEstatePermissions
        {
            get
            {
                return this["RealEstatePermissions"];
            }
        }

        /// <summary>
        /// Description of the Real Estate permissions set
        /// </summary>
        [ResourceEntry("RealEstatePermissionsDescription",
            Value = "Represents the Real Estate set of security actions permissions.",
            Description = "Description of the Real Estate permissions set",
            LastModified = "2010/12/03")]
        public string RealEstatePermissionsDescription
        {
            get
            {
                return this["RealEstatePermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ViewItems security action.
        /// </summary>
        [ResourceEntry("ViewItems",
            Value = "View an item",
            Description = "The title of ViewItems security action.",
            LastModified = "2010/02/11")]
        public string ViewItems
        {
            get
            {
                return this["ViewItems"];
            }
        }

        /// <summary>
        /// The description of ViewItems security action.
        /// </summary>
        [ResourceEntry("ViewItemsDescription",
            Value = "Allows or denies viewing of items",
            Description = "The description of ViewItems security action.",
            LastModified = "2010/12/03")]
        public string ViewItemsDescription
        {
            get
            {
                return this["ViewItemsDescription"];
            }
        }

        /// <summary>
        /// The title of CreateItems security action.
        /// </summary>
        [ResourceEntry("CreateItems",
            Value = "Create an item",
            Description = "The title of CreateItems security action.",
            LastModified = "2010/12/03")]
        public string CreateItems
        {
            get
            {
                return this["CreateItems"];
            }
        }

        /// <summary>
        /// The description of CreateItems security action.
        /// </summary>
        [ResourceEntry("CreateItemsDescription",
            Value = "Allows or denies creation of items",
            Description = "The description of CreateItems security action.",
            LastModified = "2010/12/03")]
        public string CreateItemsDescription
        {
            get
            {
                return this["CreateItemsDescription"];
            }
        }

        /// <summary>
        /// The title of ModifyItems security action.
        /// </summary>
        [ResourceEntry("ModifyItems",
            Value = "Modify an item",
            Description = "The title of ModifyItems security action.",
            LastModified = "2010/12/03")]
        public string ModifyItems
        {
            get
            {
                return this["ModifyItems"];
            }
        }

        /// <summary>
        /// The description of ModifyItems security action.
        /// </summary>
        [ResourceEntry("ModifyItemsDescription",
            Value = "Allows or denies modifying items",
            Description = "The description of ModifyItems security action.",
            LastModified = "2010/12/03")]
        public string ModifyItemsDescription
        {
            get
            {
                return this["ModifyItemsDescription"];
            }
        }

        /// <summary>
        /// The title of DeleteItems security action.
        /// </summary>
        [ResourceEntry("DeleteItems",
            Value = "Delete an item",
            Description = "The title of DeleteItems security action.",
            LastModified = "2010/12/03")]
        public string DeleteItems
        {
            get
            {
                return this["DeleteItems"];
            }
        }

        /// <summary>
        /// The description of DeleteItems security action.
        /// </summary>
        [ResourceEntry("DeleteItemsDescription",
            Value = "Allows or denies deleting items",
            Description = "The description of DeleteItems security action.",
            LastModified = "2010/12/03")]
        public string DeleteItemsDescription
        {
            get
            {
                return this["DeleteItemsDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeItemOwner security action.
        /// </summary>
        [ResourceEntry("ChangeItemOwner",
            Value = "Change item owner",
            Description = "The title of ChangeItemOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeItemOwner
        {
            get
            {
                return this["ChangeItemOwner"];
            }
        }

        /// <summary>
        /// The description of ChangeItemOwner security action.
        /// </summary>
        [ResourceEntry("ChangeItemOwnerDescription",
            Value = "Allows or denies changing the owner of an item.",
            Description = "The description of ChangeItemOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeItemOwnerDescription
        {
            get
            {
                return this["ChangeItemOwnerDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeItemPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeItemPermissions",
            Value = "Change item permissions",
            Description = "The title of ChangeItemPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeItemPermissions
        {
            get
            {
                return this["ChangeItemPermissions"];
            }
        }

        /// <summary>
        /// The description of ChangeItemPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeItemPermissionsDescription",
            Value = "Allows or denies changing the permissions of an item.",
            Description = "The description of ChangeItemPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeItemPermissionsDescription
        {
            get
            {
                return this["ChangeItemPermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ModifyThisItem security action: Modify action, for a specific item.
        /// </summary>
        [ResourceEntry("ModifyThisItem",
            Value = "Modify this item",
            Description = "The title of ModifyThisItem security action.",
            LastModified = "2010/12/03")]
        public string ModifyThisItem
        {
            get
            {
                return this["ModifyThisItem"];
            }
        }

        /// <summary>
        /// The title of ViewThisItem security action: View action, for a specific item.
        /// </summary>
        [ResourceEntry("ViewThisItem",
            Value = "View this item",
            Description = "The title of ViewThisItem security action.",
            LastModified = "2010/12/03")]
        public string ViewThisItem
        {
            get
            {
                return this["ViewThisItem"];
            }
        }

        /// <summary>
        /// The title of DeleteThisItem security action: Delete action, for a specific item.
        /// </summary>
        [ResourceEntry("DeleteThisItem",
            Value = "Delete this item",
            Description = "The title of DeleteThisItem security action.",
            LastModified = "2010/12/03")]
        public string DeleteThisItem
        {
            get
            {
                return this["DeleteThisItem"];
            }
        }

        /// <summary>
        /// The title of ChangeOwnerOfThisItem security action: ChangeOwner action, for a specific item.
        /// </summary>
        [ResourceEntry("ChangeOwnerOfThisItem",
            Value = "Change this item's owner",
            Description = "The title of ChangeOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeOwnerOfThisItem
        {
            get
            {
                return this["ChangeOwnerOfThisItem"];
            }
        }

        /// <summary>
        /// The title of ChangePermissionsOfThisItem security action: ChangePermissions action, for a specific item.
        /// </summary>
        [ResourceEntry("ChangePermissionsOfThisItem",
            Value = "Change this item's permissions",
            Description = "The title of ChangePermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangePermissionsOfThisItem
        {
            get
            {
                return this["ChangePermissionsOfThisItem"];
            }
        }

        #endregion

        #region Default Pages

        #region Menu page group

        /// <summary>
        /// phrase: Real Estate
        /// </summary>
        [ResourceEntry("PageGroupNodeTitle",
            Value = "Real Estate",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/03")]
        public string PageGroupNodeTitle
        {
            get { return this["PageGroupNodeTitle"]; }
        }

        /// <summary>
        /// phrase: This is the page group that contains all pages for the Real Estate module.
        /// </summary>
        [ResourceEntry("PageGroupNodeDescription",
            Value = "This is the page group that contains all pages for the Real Estate module.",
            Description = "phrase: This is the page group that contains all pages for the Real Estate module.",
            LastModified = "2010/12/03")]
        public string PageGroupNodeDescription
        {
            get { return this["PageGroupNodeDescription"]; }
        }

        #endregion

        #region Landing page

        /// <summary>
        /// phrase: Real Estate
        /// </summary>
        [ResourceEntry("LandingPageTitle",
            Value = "Real Estate",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/03")]
        public string LandingPageTitle
        {
            get { return this["LandingPageTitle"]; }
        }

        /// <summary>
        /// phrase: Items
        /// </summary>
        [ResourceEntry("LandingPageHtmlTitle",
            Value = "Real Estate",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/03")]
        public string LandingPageHtmlTitle
        {
            get { return this["LandingPageHtmlTitle"]; }
        }

        /// <summary>
        /// phrase: RealEstate
        /// </summary>
        [ResourceEntry("LandingPageUrlName",
            Value = "RealEstate",
            Description = "phrase: RealEstate",
            LastModified = "2010/12/03")]
        public string LandingPageUrlName
        {
            get { return this["LandingPageUrlName"]; }
        }

        /// <summary>
        /// phrase: Landing page for the Real Estate module
        /// </summary>
        [ResourceEntry("LandingPageDescription",
            Value = "Landing page for the Real Estate module",
            Description = "phrase: Landing page for the Real Estate module",
            LastModified = "2010/12/03")]
        public string LandingPageDescription
        {
            get { return this["LandingPageDescription"]; }
        }

        #endregion

        #endregion

        #region ContentView registration

        /// <summary>
        /// phrase: Real Estate
        /// </summary>
        [ResourceEntry("RealEstateViewTitle",
            Value = "Real Estate",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/03")]
        public string RealEstateViewTitle
        {
            get { return this["RealEstateViewTitle"]; }
        }

        /// <summary>
        /// phrase: Widget that displays real estate items
        /// </summary>
        [ResourceEntry("RealEstateViewDescription",
            Value = "Widget that displays real estate items",
            Description = "phrase: Widget that displays real estate items",
            LastModified = "2010/12/03")]
        public string RealEstateViewDescription
        {
            get { return this["RealEstateViewDescription"]; }
        }

        #endregion

        /// <summary>
        /// The title of the edit item dialog
        /// </summary>
        [ResourceEntry("EditItem",
            Value = "Edit an item",
            Description = "The title of the edit item dialog",
            LastModified = "2009/12/20")]
        public string EditItem
        {
            get { return this["EditItem"]; }
        }

        /// <summary>
        /// word: View
        /// </summary>
        [ResourceEntry("View",
            Value = "View",
            Description = "word: View",
            LastModified = "2010/01/28")]
        public string View
        {
            get
            {
                return this["View"];
            }
        }


        /// <summary>
        /// word: Delete
        /// </summary>
        [ResourceEntry("Delete",
            Value = "Delete",
            Description = "word: Delete",
            LastModified = "2010/01/25")]
        public string Delete
        {
            get { return this["Delete"]; }
        }

        /// <summary>
        /// word: <strong>Edit...</strong>
        /// </summary>
        [ResourceEntry("Edit",
            Value = "<strong>Edit...</strong>",
            Description = "word: Edit",
            LastModified = "2010/01/29")]
        public string Edit
        {
            get
            {
                return this["Edit"];
            }
        }

        /// <summary>
        /// word: Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2010/01/28")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// word: Permissions
        /// </summary>
        [ResourceEntry("Permissions",
            Value = "Permissions",
            Description = "word: Permissions",
            LastModified = "2010/01/29")]
        public string Permissions
        {
            get { return this["Permissions"]; }
        }

        /// <summary>
        /// Messsage: Create Real Estate item
        /// </summary>
        /// <value>Label of the dialog that creates a Real Estate item.</value>
        [ResourceEntry(
            "CreateItem",
            Value = "Create a Real Estate item",
            Description = "Label of the dialog that creates a Real Estate item.",
            LastModified = "2010/6/27")
        ]
        public string CreateItem
        {
            get { return this["CreateItem"]; }
        }


        /// <summary>
        /// phrase: No items have been created yet.
        /// </summary>
        [ResourceEntry("NoItems",
            Value = "No items have been created yet",
            Description = "phrase: No items have been created yet",
            LastModified = "2010/07/26")]
        public string NoItems
        {
            get
            {
                return this["NoItems"];
            }
        }


        /// <summary>
        /// phrase: What do you want to do now?
        /// </summary>
        [ResourceEntry("WhatDoYouWantToDoNow",
            Value = "What do you want to do now?",
            Description = "phrase: What do you want to do now?",
            LastModified = "2009/01/28")]
        public string WhatDoYouWantToDoNow
        {
            get
            {
                return this["WhatDoYouWantToDoNow"];
            }
        }


        /// <summary>
        /// Phrase: Items by category
        /// </summary>
        [ResourceEntry("ItemsByCategory",
            Value = "Items by category",
            Description = "Phrase: Items by category",
            LastModified = "2010/07/23")]
        public string ItemsByCategory
        {
            get
            {
                return this["ItemsByCategory"];
            }
        }


        /// <summary>
        /// Phrase: Items by tag
        /// </summary>
        [ResourceEntry("ItemsByTag",
            Value = "Items by tag",
            Description = "Phrase: Items by tag",
            LastModified = "2010/07/23")]
        public string ItemsByTag
        {
            get
            {
                return this["ItemsByTag"];
            }
        }

        /// <summary>
        /// The text of last updated items sidebar button
        /// </summary>
        [ResourceEntry("DisplayLastUpdatedItems",
            Value = "Display items last updated in...",
            Description = "The text of last updated items sidebar button",
            LastModified = "2010/08/20")]
        public string DisplayLastUpdatedItems
        {
            get
            {
                return this["DisplayLastUpdatedItems"];
            }
        }


        /// <summary>
        /// phrase: Filter items
        /// </summary>
        [ResourceEntry("FilterItems",
            Value = "Filter items",
            Description = "phrase: Filter items",
            LastModified = "2010/01/25")]
        public string FilterItems
        {
            get { return this["FilterItems"]; }
        }

        /// <summary>
        /// phrase: Manage also.
        /// </summary>
        [ResourceEntry("ManageAlso",
            Value = "Manage also",
            Description = "phrase: Manage also",
            LastModified = "2010/01/25")]
        public string ManageAlso
        {
            get { return this["ManageAlso"]; }
        }


        /// <summary>
        /// Phrase: Edit Real Estate settings 
        /// </summary>
        [ResourceEntry("EditRealEstateSettings",
            Value = "Set which real estate properties to display",
            Description = "Phrase: Edit Real Estate settings",
            LastModified = "2010/11/13")]
        public string EditRealEstateSettings
        {
            get
            {
                return this["EditRealEstateSettings"];
            }
        }

        /// <summary>
        /// phrase: Real Estate Settings.
        /// </summary>
        [ResourceEntry("Settings",
            Value = "Settings for real estate items",
            Description = "phrase: Real Estate Settings",
            LastModified = "2010/01/25")]
        public string Settings
        {
            get { return this["Settings"]; }
        }

        /// <summary>
        /// Phrase: Close date
        /// </summary>
        [ResourceEntry("CloseDateFilter",
                       Value = "Close dates",
                       Description = "The link for closing the date filter widget in the sidebar.",
                       LastModified = "2010/08/20")]
        public string CloseDateFilter
        {
            get
            {
                return this["CloseDateFilter"];
            }
        }

        /// <summary>
        /// Phrase: All items
        /// </summary>
        [ResourceEntry("AllItems",
            Value = "All items",
            Description = "Phrase: All items",
            LastModified = "2010/07/27")]
        public string AllItems
        {
            get
            {
                return this["AllItems"];
            }
        }


        /// <summary>
        /// The text of my items sidebar button
        /// </summary>
        [ResourceEntry("MyItems",
            Value = "My items",
            Description = "The text of my items sidebar button",
            LastModified = "2010/08/19")]
        public string MyItems
        {
            get
            {
                return this["MyItems"];
            }
        }

        /// <summary>
        /// phrase: Published
        /// </summary>
        [ResourceEntry("PublishedItems",
            Value = "Published",
            Description = "The text of published items sidebar button.",
            LastModified = "2010/08/19")]
        public string PublishedItems
        {
            get
            {
                return this["PublishedItems"];
            }
        }

        /// <summary>
        /// word: Drafts
        /// </summary>
        [ResourceEntry("DraftItems",
            Value = "Drafts",
            Description = "The text of draft items sidebar button.",
            LastModified = "2010/08/19")]
        public string DraftItems
        {
            get
            {
                return this["DraftItems"];
            }
        }



        /// <summary>
        /// word: Scheduled
        /// </summary>
        [ResourceEntry("ScheduledItems",
            Value = "Scheduled",
            Description = "The text of scheduled items sidebar button.",
            LastModified = "2010/08/20")]
        public string ScheduledItems
        {
            get
            {
                return this["ScheduledItems"];
            }
        }

        /// <summary>
        /// phrase: Waiting for approval
        /// </summary>
        [ResourceEntry("WaitingForApproval",
            Value = "Waiting for approval",
            Description = "The text of the 'Waiting for approval' button in the items sidebar.",
            LastModified = "2010/11/08")]
        public string WaitingForApproval
        {
            get
            {
                return this["WaitingForApproval"];
            }
        }


        /// <summary>
        /// phrase: Drafts
        /// </summary>
        [ResourceEntry("ByDateModified",
            Value = "by Date modified...",
            Description = "The text of the date filter sidebar button.",
            LastModified = "2010/08/20")]
        public string ByDateModified
        {
            get
            {
                return this["ByDateModified"];
            }
        }



        /// <summary>
        /// phrase: Comments for items.
        /// </summary>
        [ResourceEntry("CommentsForItems",
            Value = "Comments for items",
            Description = "phrase: Comments for items",
            LastModified = "2010/01/25")]
        public string CommentsForItems
        {
            get { return this["CommentsForItems"]; }
        }


        /// <summary>
        /// phrase: Permissions for items
        /// </summary>
        [ResourceEntry("PermissionsForItems",
            Value = "Permissions",
            Description = "phrase: Permissions for items",
            LastModified = "2010/01/29")]
        public string PermissionsForItems
        {
            get { return this["PermissionsForItems"]; }
        }


        /// <summary>
        /// phrase: Settings for items
        /// </summary>
        [ResourceEntry("SettingsForItems",
            Value = "Settings",
            Description = "phrase: Settings for items",
            LastModified = "2010/01/29")]
        public string SettingsForItems
        {
            get { return this["SettingsForItems"]; }
        }

        /// <summary>
        /// The title of the create new item dialog
        /// </summary>
        [ResourceEntry("CreateNewItem",
            Value = "Create a Real Estate item",
            Description = "The title of the create new item dialog",
            LastModified = "2010/07/26")]
        public string CreateNewItem
        {
            get { return this["CreateNewItem"]; }
        }

        /// <summary>
        /// Back to items
        /// </summary>
        [ResourceEntry("BackToItems",
                       Value = "Back to Items",
                       Description = "The text of the back to items link",
                       LastModified = "2010/10/13")]
        public string BackToItems
        {
            get
            {
                return this["BackToItems"];
            }
        }

        /// <summary>
        /// phrase: Real Estate
        /// </summary>
        [ResourceEntry("ModuleTitle",
            Value = "Real Estate",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/07")]
        public string ModuleTitle
        {
            get { return this["ModuleTitle"]; }
        }

        /// <summary>
        /// phrase: Real Estate
        /// </summary>
        [ResourceEntry("WhatIsInTheBox",
            Value = "What's in the box",
            Description = "phrase: Real Estate",
            LastModified = "2010/12/07")]
        public string WhatIsInTheBox
        {
            get { return this["WhatIsInTheBox"]; }
        }


        /// <summary>
        /// phrase: Title
        /// </summary>
        [ResourceEntry("lTitle",
            Value = "Title",
            Description = "phrase: Title",
            LastModified = "2010/12/09")]
        public string lTitle
        {
            get { return this["lTitle"]; }
        }


        /// <summary>
        /// phrase: Title cannot be empty
        /// </summary>
        [ResourceEntry("TitleCannotBeEmpty",
            Value = "Title cannot be empty",
            Description = "phrase: Title cannot be empty",
            LastModified = "2010/12/09")]
        public string TitleCannotBeEmpty
        {
            get { return this["TitleCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Language
        /// </summary>
        [ResourceEntry("Language",
            Value = "Language",
            Description = "phrase: Language",
            LastModified = "2010/12/09")]
        public string Language
        {
            get { return this["Language"]; }
        }


        /// <summary>
        /// phrase: Summary
        /// </summary>
        [ResourceEntry("lSummary",
            Value = "Summary",
            Description = "phrase: Summary",
            LastModified = "2010/12/09")]
        public string lSummary
        {
            get { return this["lSummary"]; }
        }


        /// <summary>
        /// phrase: Categories and Tags
        /// </summary>
        [ResourceEntry("CategoriesAndTags",
            Value = "Categories and Tags",
            Description = "phrase: Categories and Tags",
            LastModified = "2010/12/09")]
        public string CategoriesAndTags
        {
            get { return this["CategoriesAndTags"]; }
        }


        /// <summary>
        /// phrase: Click to add summary
        /// </summary>
        [ResourceEntry("ClickToAddSummary",
            Value = "Click to add summary",
            Description = "phrase: Click to add summary",
            LastModified = "2010/12/09")]
        public string ClickToAddSummary
        {
            get { return this["ClickToAddSummary"]; }
        }


        /// <summary>
        /// phrase: Additional info <em class='sfNote'>(Author, Source)</em>
        /// </summary>
        [ResourceEntry("AuthorSourceThumbnail",
            Value = "Additional info <em class='sfNote'>(Author, Source)</em>",
            Description = "phrase: Additional info <em class='sfNote'>(Author, Source)</em>",
            LastModified = "2010/12/09")]
        public string AuthorSourceThumbnail
        {
            get { return this["AuthorSourceThumbnail"]; }
        }

        /// <summary>
        /// phrase: Author
        /// </summary>
        [ResourceEntry("Author",
            Value = "Author",
            Description = "phrase: Author",
            LastModified = "2010/12/09")]
        public string Author
        {
            get { return this["Author"]; }
        }

        /// <summary>
        /// phrase: Source name
        /// </summary>
        [ResourceEntry("SourceName",
            Value = "Source name",
            Description = "phrase: Source name",
            LastModified = "2010/12/09")]
        public string SourceName
        {
            get { return this["SourceName"]; }
        }

        /// <summary>
        /// phrase: More options <em class='sfNote'>(URL, Comments)</em>
        /// </summary>
        [ResourceEntry("MoreOptionsURL",
            Value = "More options <em class='sfNote'>(URL, Comments)</em>",
            Description = "phrase: More options <em class='sfNote'>(URL, Comments)</em>",
            LastModified = "2010/12/09")]
        public string MoreOptionsURL
        {
            get { return this["MoreOptionsURL"]; }
        }

        /// <summary>
        /// phrase: URL
        /// </summary>
        [ResourceEntry("UrlName",
            Value = "URL",
            Description = "phrase: URL",
            LastModified = "2010/12/09")]
        public string UrlName
        {
            get { return this["UrlName"]; }
        }

        /// <summary>
        /// phrase: URL cannot be empty
        /// </summary>
        [ResourceEntry("UrlNameCannotBeEmpty",
            Value = "URL cannot be empty",
            Description = "phrase: URL cannot be empty",
            LastModified = "2010/12/09")]
        public string UrlNameCannotBeEmpty
        {
            get { return this["UrlNameCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Url cannot contain spaces or special characters!
        /// </summary>
        [ResourceEntry("UrlNameInvalid",
            Value = "Url cannot contain spaces or special characters!",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string UrlNameInvalid
        {
            get { return this["UrlNameInvalid"]; }
        }




        /// <summary>
        /// phrase: Which items to display?
        /// </summary>
        [ResourceEntry("WhichItemsToDisplay",
            Value = "Which items to display?",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string WhichItemsToDisplay
        {
            get { return this["WhichItemsToDisplay"]; }
        }

        /// <summary>
        /// phrase: All published items
        /// </summary>
        [ResourceEntry("AllPublishedItems",
            Value = "All published items",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string AllPublishedItems
        {
            get { return this["AllPublishedItems"]; }
        }

        /// <summary>
        /// phrase: One particular item only...
        /// </summary>
        [ResourceEntry("OneParticularItemOnly",
            Value = "One particular item only...",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string OneParticularItemOnly
        {
            get { return this["OneParticularItemOnly"]; }
        }

        /// <summary>
        /// phrase: Selection of items
        /// </summary>
        [ResourceEntry("SelectionOfItems",
            Value = "Selection of items",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string SelectionOfItems
        {
            get { return this["SelectionOfItems"]; }
        }

        /// <summary>
        /// phrase: Advanced selection
        /// </summary>
        [ResourceEntry("AdvancedSelection",
            Value = "Advanced selection",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string AdvancedSelection
        {
            get { return this["AdvancedSelection"]; }
        }

        /// <summary>
        /// phrase: No items have been created yet
        /// </summary>
        [ResourceEntry("NoItemsHaveBeenCreatedYet",
            Value = "No items have been created yet",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string NoItemsHaveBeenCreatedYet
        {
            get { return this["NoItemsHaveBeenCreatedYet"]; }
        }

        /// <summary>
        /// phrase: Select items
        /// </summary>
        [ResourceEntry("SelectItems",
            Value = "Select items",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string SelectItems
        {
            get { return this["SelectItems"]; }
        }

        /// <summary>
        /// phrase: Sort items
        /// </summary>
        [ResourceEntry("SortItems",
            Value = "Sort items",
            Description = "phrase",
            LastModified = "2009/11/06")]
        public string SortItems
        {
            get { return this["SortItems"]; }
        }
        #endregion

        #region Fields

        /// <summary>
        /// phrase: Item Number
        /// </summary>
        [ResourceEntry("ItemNumber",
            Value = "Item Number",
            Description = "phrase: Item Number",
            LastModified = "2011/03/29")]
        public string ItemNumber
        {
            get { return this["ItemNumber"]; }
        }


        /// <summary>
        /// phrase: Item number cannot be empty
        /// </summary>
        [ResourceEntry("ItemNumberCannotBeEmpty",
            Value = "Item number cannot be empty",
            Description = "phrase: Item number cannot be empty",
            LastModified = "2011/03/29")]
        public string ItemNumberCannotBeEmpty
        {
            get { return this["ItemNumberCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Description
        /// </summary>
        [ResourceEntry("Description",
            Value = "Description",
            Description = "phrase: Description",
            LastModified = "2011/06/24")]
        public string Description
        {
            get { return this["Description"]; }
        }

        /// <summary>
        /// Word: Address
        /// </summary>
        [ResourceEntry("Address",
            Value = "Address",
            Description = "Word: Address",
            LastModified = "2011/03/29")]
        public string Address
        {
            get { return this["Address"]; }
        }


        /// <summary>
        /// phrase: Address cannot be empty
        /// </summary>
        [ResourceEntry("AddressCannotBeEmpty",
            Value = "Address cannot be empty",
            Description = "phrase: Address cannot be empty",
            LastModified = "2011/03/29")]
        public string AddressCannotBeEmpty
        {
            get { return this["AddressCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Postal Code
        /// </summary>
        [ResourceEntry("PostalCode",
            Value = "Postal Code",
            Description = "phrase: Postal Code",
            LastModified = "2011/03/29")]
        public string PostalCode
        {
            get { return this["PostalCode"]; }
        }


        /// <summary>
        /// phrase: Postal code cannot be empty
        /// </summary>
        [ResourceEntry("PostalCodeCannotBeEmpty",
            Value = "Postal code cannot be empty",
            Description = "phrase: Postal code cannot be empty",
            LastModified = "2011/03/29")]
        public string PostalCodeCannotBeEmpty
        {
            get { return this["PostalCodeCannotBeEmpty"]; }
        }

        /// <summary>
        /// Word: Housing
        /// </summary>
        [ResourceEntry("Housing",
            Value = "Housing (m2)",
            Description = "Word: Housing (m2)",
            LastModified = "2011/06/23")]
        public string Housing
        {
            get { return this["Housing"]; }
        }


        /// <summary>
        /// phrase: Housing cannot be empty
        /// </summary>
        [ResourceEntry("HousingCannotBeEmpty",
            Value = "Housing cannot be empty",
            Description = "phrase: Housing cannot be empty",
            LastModified = "2011/03/29")]
        public string HousingCannotBeEmpty
        {
            get { return this["HousingCannotBeEmpty"]; }
        }

        /// <summary>
        /// Phrase: Number of Rooms
        /// </summary>
        [ResourceEntry("NumberOfRooms",
            Value = "Number of Rooms",
            Description = "Phrase: Number of Rooms",
            LastModified = "2011/06/23")]
        public string NumberOfRooms
        {
            get { return this["NumberOfRooms"]; }
        }

        /// <summary>
        /// Word: Rooms
        /// </summary>
        [ResourceEntry("Rooms",
            Value = "Rooms",
            Description = "Word: Rooms",
            LastModified = "2011/03/29")]
        public string Rooms
        {
            get { return this["Rooms"]; }
        }

        /// <summary>
        /// Word: Features
        /// </summary>
        [ResourceEntry("Features",
            Value = "Features",
            Description = "Word: Features",
            LastModified = "2011/06/24")]
        public string Features
        {
            get { return this["Features"]; }
        }

        /// <summary>
        /// phrase: List of Rooms
        /// </summary>
        [ResourceEntry("ListOfRooms",
            Value = "List of Rooms",
            Description = "phrase: List of Rooms",
            LastModified = "2011/03/29")]
        public string ListOfRooms
        {
            get { return this["ListOfRooms"]; }
        }

        /// <summary>
        /// Word: Floors
        /// </summary>
        [ResourceEntry("Floors",
            Value = "Floors",
            Description = "Word: Floors",
            LastModified = "2011/03/29")]
        public string Floors
        {
            get { return this["Floors"]; }
        }

        /// <summary>
        /// Word: Built
        /// </summary>
        [ResourceEntry("YearBuilt",
            Value = "Built (year)",
            Description = "Word: Built (year)",
            LastModified = "2011/06/23")]
        public string YearBuilt
        {
            get { return this["YearBuilt"]; }
        }

        #region Price
        /// <summary>
        /// Word: Price
        /// </summary>
        [ResourceEntry("Price",
            Value = "Price",
            Description = "Word: Price",
            LastModified = "2011/03/29")]
        public string Price
        {
            get { return this["Price"]; }
        }


        /// <summary>
        /// phrase: Price cannot be empty
        /// </summary>
        [ResourceEntry("PriceCannotBeEmpty",
            Value = "Price cannot be empty",
            Description = "phrase: Price cannot be empty",
            LastModified = "2011/03/29")]
        public string PriceCannotBeEmpty
        {
            get { return this["PriceCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Price is not valid
        /// </summary>
        [ResourceEntry("PriceIsNotValid",
            Value = "Price is not valid",
            Description = "phrase: Price is not valid",
            LastModified = "2011/03/29")]
        public string PriceIsNotValid
        {
            get { return this["PriceIsNotValid"]; }
        } 
        #endregion

        #region Payment
        /// <summary>
        /// Word: Payment
        /// </summary>
        [ResourceEntry("Payment",
            Value = "Payment",
            Description = "Word: Payment",
            LastModified = "2011/03/29")]
        public string Payment
        {
            get { return this["Payment"]; }
        }


        /// <summary>
        /// phrase: Payment cannot be empty
        /// </summary>
        [ResourceEntry("PaymentCannotBeEmpty",
            Value = "Payment cannot be empty",
            Description = "phrase: Payment cannot be empty",
            LastModified = "2011/03/29")]
        public string PaymentCannotBeEmpty
        {
            get { return this["PaymentCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Payment is not valid
        /// </summary>
        [ResourceEntry("PaymentIsNotValid",
            Value = "Payment is not valid",
            Description = "phrase: Payment is not valid",
            LastModified = "2011/03/29")]
        public string PaymentIsNotValid
        {
            get { return this["PaymentIsNotValid"]; }
        } 
        #endregion

        #region Monthly Rate
        /// <summary>
        /// Word: Monthly Rate
        /// </summary>
        [ResourceEntry("MonthlyRate",
            Value = "Monthly Rate",
            Description = "Word: Monthly Rate",
            LastModified = "2011/03/29")]
        public string MonthlyRate
        {
            get { return this["MonthlyRate"]; }
        }


        /// <summary>
        /// phrase: Monthly Rate cannot be empty
        /// </summary>
        [ResourceEntry("MonthlyRateCannotBeEmpty",
            Value = "Monthly Rate cannot be empty",
            Description = "phrase: Monthly Rate cannot be empty",
            LastModified = "2011/03/29")]
        public string MonthlyRateCannotBeEmpty
        {
            get { return this["MonthlyRateCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Monthly Rate is not valid
        /// </summary>
        [ResourceEntry("MonthlyRateIsNotValid",
            Value = "Monthly Rate is not valid",
            Description = "phrase: Monthly Rate is not valid",
            LastModified = "2011/03/29")]
        public string MonthlyRateIsNotValid
        {
            get { return this["MonthlyRateIsNotValid"]; }
        }
        #endregion

        #region Net
        /// <summary>
        /// Word: Net
        /// </summary>
        [ResourceEntry("Net",
            Value = "Net",
            Description = "Word: Net",
            LastModified = "2011/03/29")]
        public string Net
        {
            get { return this["Net"]; }
        }


        /// <summary>
        /// phrase: Net cannot be empty
        /// </summary>
        [ResourceEntry("NetCannotBeEmpty",
            Value = "Net cannot be empty",
            Description = "phrase: Net cannot be empty",
            LastModified = "2011/03/29")]
        public string NetCannotBeEmpty
        {
            get { return this["NetCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Net is not valid
        /// </summary>
        [ResourceEntry("NetIsNotValid",
            Value = "Net is not valid",
            Description = "phrase: Net is not valid",
            LastModified = "2011/03/29")]
        public string NetIsNotValid
        {
            get { return this["NetIsNotValid"]; }
        }
        #endregion

        #region Price/m2
        /// <summary>
        /// phrase: Price/m2
        /// </summary>
        [ResourceEntry("PriceSquareMeter",
            Value = "Price/m2",
            Description = "phrase: Price/m2",
            LastModified = "2011/03/29")]
        public string PriceSquareMeter
        {
            get { return this["PriceSquareMeter"]; }
        }


        /// <summary>
        /// phrase: Price/m2 cannot be empty
        /// </summary>
        [ResourceEntry("PriceSquareMeterCannotBeEmpty",
            Value = "Price/m2 cannot be empty",
            Description = "phrase: Price/m2 cannot be empty",
            LastModified = "2011/03/29")]
        public string PriceSquareMeterCannotBeEmpty
        {
            get { return this["PriceSquareMeterCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Price/m2 is not valid
        /// </summary>
        [ResourceEntry("PriceSquareMeterIsNotValid",
            Value = "Price/m2 is not valid",
            Description = "phrase: Price/m2 is not valid",
            LastModified = "2011/03/29")]
        public string PriceSquareMeterIsNotValid
        {
            get { return this["PriceSquareMeterIsNotValid"]; }
        }
        #endregion

        #region Latitude
        /// <summary>
        /// phrase: Latitude
        /// </summary>
        [ResourceEntry("Latitude",
            Value = "Latitude",
            Description = "Word: Latitude",
            LastModified = "2011/07/19")]
        public string Latitude
        {
            get { return this["Latitude"]; }
        }

        /// <summary>
        /// phrase: Latitude is not valid
        /// </summary>
        [ResourceEntry("LatitudeNotValid",
            Value = "Latitude is not valid",
            Description = "phrase: Latitude is not valid",
            LastModified = "2011/07/19")]
        public string LatitudeNotValid
        {
            get { return this["LatitudeNotValid"]; }
        } 
        #endregion

        #region Longitude
        /// <summary>
        /// phrase: Longitude
        /// </summary>
        [ResourceEntry("Longitude",
            Value = "Longitude",
            Description = "Word: Longitude",
            LastModified = "2011/07/19")]
        public string Longitude
        {
            get { return this["Longitude"]; }
        }

        /// <summary>
        /// phrase: Longitude is not valid
        /// </summary>
        [ResourceEntry("LongitudeNotValid",
            Value = "Longitude is not valid",
            Description = "phrase: Longitude is not valid",
            LastModified = "2011/07/19")]
        public string LongitudeNotValid
        {
            get { return this["LongitudeNotValid"]; }
        } 
        #endregion

        #region Agent
        /// <summary>
        /// phrase: Agent
        /// </summary>
        [ResourceEntry("Agent",
            Value = "Agent",
            Description = "Word: Agent",
            LastModified = "2011/06/23")]
        public string Agent
        {
            get { return this["Agent"]; }
        }


        #endregion

        #region Location
        /// <summary>
        /// phrase: Agent
        /// </summary>
        [ResourceEntry("Location",
            Value = "Location",
            Description = "Word: Location",
            LastModified = "2011/07/01")]
        public string Location
        {
            get { return this["Location"]; }
        }


        #endregion

        #region Flat Type
        /// <summary>
        /// phrase: Flat Type
        /// </summary>
        [ResourceEntry("FlatType",
            Value = "Flat Type",
            Description = "phrase: Flat Type",
            LastModified = "2011/07/01")]
        public string FlatType
        {
            get { return this["FlatType"]; }
        }


        #endregion

        #region Type
        /// <summary>
        /// word: Type
        /// </summary>
        [ResourceEntry("Type",
            Value = "Type",
            Description = "word: Type",
            LastModified = "2011/07/01")]
        public string Type
        {
            get { return this["Type"]; }
        }


        #endregion

        #endregion

        #region SliderSeeDetails
        /// <summary>
        /// phrase: Agent
        /// </summary>
        [ResourceEntry("SliderSeeDetails",
            Value = "More details and photos",
            Description = "phrase: More details and photos",
            LastModified = "2011/06/30")]
        public string SliderSeeDetails
        {
            get { return this["SliderSeeDetails"]; }
        }


        #endregion

        #region CarouselSeeDetails
        /// <summary>
        /// phrase: Agent
        /// </summary>
        [ResourceEntry("CarouselSeeDetails",
            Value = "View details",
            Description = "phrase: View details",
            LastModified = "2011/06/30")]
        public string CarouselSeeDetails
        {
            get { return this["CarouselSeeDetails"]; }
        }


        #endregion

        #region Thumbnails View
        /// <summary>
        /// word: Thumbnails
        /// </summary>
        [ResourceEntry("ThumbnailsView",
            Value = "Thumbnails",
            Description = "word: Thumbnails",
            LastModified = "2011/06/30")]
        public string ThumbnailsView
        {
            get { return this["ThumbnailsView"]; }
        }
        #endregion

        #region List View
        /// <summary>
        /// word: List
        /// </summary>
        [ResourceEntry("ListView",
            Value = "List",
            Description = "word: List",
            LastModified = "2011/06/30")]
        public string ListView
        {
            get { return this["ListView"]; }
        }
        #endregion

        #region Sort by
        /// <summary>
        /// phrase: Sort by
        /// </summary>
        [ResourceEntry("SortBy",
            Value = "Sort by",
            Description = "phrase: Sort by",
            LastModified = "2011/06/30")]
        public string SortBy
        {
            get { return this["SortBy"]; }
        }
        #endregion

        #region Show on Page
        /// <summary>
        /// phrase: Show on page
        /// </summary>
        [ResourceEntry("ShowOnPage",
            Value = "Show on page",
            Description = "phrase: Show on page",
            LastModified = "2011/06/30")]
        public string ShowOnPage
        {
            get { return this["ShowOnPage"]; }
        }
        #endregion


        #region Nine Items
        /// <summary>
        /// phrase: 9 items
        /// </summary>
        [ResourceEntry("NineItems",
            Value = "9 items",
            Description = "phrase: 9 items",
            LastModified = "2011/06/30")]
        public string NineItems
        {
            get { return this["NineItems"]; }
        }
        #endregion

        #region Eighteen Items
        /// <summary>
        /// phrase: 18 items
        /// </summary>
        [ResourceEntry("EighteenItems",
            Value = "18 items",
            Description = "phrase: 18 items",
            LastModified = "2011/06/30")]
        public string EighteenItems
        {
            get { return this["EighteenItems"]; }
        }
        #endregion

        #region Twentyseven Items
        /// <summary>
        /// phrase: 27 items
        /// </summary>
        [ResourceEntry("TwentysevenItems",
            Value = "27 items",
            Description = "phrase: 27 items",
            LastModified = "2011/06/30")]
        public string TwentysevenItems
        {
            get { return this["TwentysevenItems"]; }
        }
        #endregion

        /// <summary>
        /// Word: Overview
        /// </summary>
        [ResourceEntry("Overview",
            Value = "Overview",
            Description = "Word: Overview",
            LastModified = "2011/07/20")]
        public string Overview
        {
            get { return this["Overview"]; }
        }

        /// <summary>
        /// Word: Photos
        /// </summary>
        [ResourceEntry("Photos",
            Value = "Photos",
            Description = "Word: Photos",
            LastModified = "2011/07/20")]
        public string Photos
        {
            get { return this["Photos"]; }
        }

        /// <summary>
        /// Phrase: Panaromic View
        /// </summary>
        [ResourceEntry("PanaromicView",
            Value = "Panaromic View",
            Description = "Phrase: Panaromic View",
            LastModified = "2011/07/20")]
        public string PanaromicView
        {
            get { return this["PanaromicView"]; }
        }

        /// <summary>
        /// Phrase: Floor Plan
        /// </summary>
        [ResourceEntry("FloorPlan",
            Value = "Floor Plan",
            Description = "Phrase: Floor Plan",
            LastModified = "2011/07/20")]
        public string FloorPlan
        {
            get { return this["FloorPlan"]; }
        }

        /// <summary>
        /// Phrase: Map & Local Area
        /// </summary>
        [ResourceEntry("MapAndLocalArea",
            Value = "Map & Local Area",
            Description = "Phrase: Map & Local Area",
            LastModified = "2011/07/20")]
        public string MapAndLocalArea
        {
            get { return this["MapAndLocalArea"]; }
        }

        /// <summary>
        /// Phrase: Contact Our Agent
        /// </summary>
        [ResourceEntry("ContactOurAgent",
            Value = "Contact Our Agent",
            Description = "Phrase: Contact Our Agent",
            LastModified = "2011/07/20")]
        public string ContactOurAgent
        {
            get { return this["ContactOurAgent"]; }
        }

        /// <summary>
        /// Phrase: Contact Information
        /// </summary>
        [ResourceEntry("ContactInformation",
            Value = "Contact Information",
            Description = "Phrase: Contact Information",
            LastModified = "2011/07/20")]
        public string ContactInformation
        {
            get { return this["ContactInformation"]; }
        }

        /// <summary>
        /// Word: Name
        /// </summary>
        [ResourceEntry("Name",
            Value = "Name",
            Description = "Word: Name",
            LastModified = "2011/07/20")]
        public string Name
        {
            get { return this["Name"]; }
        }

        /// <summary>
        /// Word: Telephone
        /// </summary>
        [ResourceEntry("Telephone",
            Value = "Telephone",
            Description = "Word: Telephone",
            LastModified = "2011/07/20")]
        public string Telephone
        {
            get { return this["Telephone"]; }
        }

        /// <summary>
        /// Word: E-mail
        /// </summary>
        [ResourceEntry("Email",
            Value = "E-mail",
            Description = "Word: E-mail",
            LastModified = "2011/07/20")]
        public string Email
        {
            get { return this["Email"]; }
        }

        /// <summary>
        /// phrase: Publish phrase
        /// </summary>
        [ResourceEntry("Publish",
            Value = "Publish",
            Description = "phrase",
            LastModified = "2012/19/01")]
        public string Publish
        {
            get { return this["Publish"]; }
        }

        /// <summary>
        /// phrase: Publish phrase
        /// </summary>
        [ResourceEntry("Unpublish",
            Value = "Unpublish",
            Description = "phrase",
            LastModified = "2012/19/01")]
        public string Unpublish
        {
            get { return this["Unpublish"]; }
        }
    }
}
