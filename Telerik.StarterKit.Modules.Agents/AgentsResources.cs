using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.StarterKit.Modules.Agents
{
    /// <summary>
    /// Resource class for the procuts module
    /// </summary>
    [ObjectInfo(typeof(AgentsResources), Title = "AgentsResourcesTitle", Description = "AgentsResourcesDescription")]
    public class AgentsResources : Resource
    {
        
        #region Constructions
        
        /// <summary>
        /// Initializes new instance of <see cref="AgentsResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public AgentsResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="AgentsResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public AgentsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// The title of this class
        /// </summary>
        [ResourceEntry("AgentsResourcesTitle",
            Value = "Agents",
            Description = "The title of this class.",
            LastModified = "2010/02/11")]
        public string AgentsResourcesTitle
        {
            get { return this["AgentsResourcesTitle"];  }
        }

        /// <summary>
        /// The description of this class
        /// </summary>
        [ResourceEntry("AgentsResourcesDescription",
            Value = "Contains localizable resources for Agents module.",
            Description = "The description of this class.",
            LastModified = "2010/12/01")]
        public string AgentsResourcesDescription
        {
            get { return this["AgentsResourcesDescription"];  }
        }

        #endregion


        /// <summary>
        /// Title of the DesignerContentTitleText
        /// </summary>
        [ResourceEntry("DesignerContentTitleText",
            Value = "Which agents to display?",
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
            Value = "Sort agents",
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
        /// Title of the ManageAgents
        /// </summary>
        [ResourceEntry("ManageAgents",
            Value = "Manage Agents",
            Description = "ManageAgents",
            LastModified = "2010/12/07")]
        public string ManageAgents
        {
            get
            {
                return this["ManageAgents"];
            }
        }


        #region Security-related resources

        /// <summary>
        /// Title of the Agents permissions set
        /// </summary>
        [ResourceEntry("AgentsPermissions",
            Value = "Agents",
            Description = "Title of the Agents permissions set",
            LastModified = "2010/12/03")]
        public string AgentsPermissions
        {
            get
            {
                return this["AgentsPermissions"];
            }
        }

        /// <summary>
        /// Description of the Agents permissions set
        /// </summary>
        [ResourceEntry("AgentsPermissionsDescription",
            Value = "Represents the Agents set of security actions permissions.",
            Description = "Description of the Agents permissions set",
            LastModified = "2010/12/03")]
        public string AgentsPermissionsDescription
        {
            get
            {
                return this["AgentsPermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ViewAgents security action.
        /// </summary>
        [ResourceEntry("ViewAgents",
            Value = "View an agent",
            Description = "The title of ViewAgents security action.",
            LastModified = "2010/02/11")]
        public string ViewAgents
        {
            get
            {
                return this["ViewAgents"];
            }
        }

        /// <summary>
        /// The description of ViewAgents security action.
        /// </summary>
        [ResourceEntry("ViewAgentsDescription",
            Value = "Allows or denies viewing of agents",
            Description = "The description of ViewAgents security action.",
            LastModified = "2010/12/03")]
        public string ViewAgentsDescription
        {
            get
            {
                return this["ViewAgentsDescription"];
            }
        }

        /// <summary>
        /// The title of CreateAgents security action.
        /// </summary>
        [ResourceEntry("CreateAgents",
            Value = "Create an agent",
            Description = "The title of CreateAgents security action.",
            LastModified = "2010/12/03")]
        public string CreateAgents
        {
            get
            {
                return this["CreateAgents"];
            }
        }

        /// <summary>
        /// The description of CreateAgents security action.
        /// </summary>
        [ResourceEntry("CreateAgentsDescription",
            Value = "Allows or denies creation of agents",
            Description = "The description of CreateAgents security action.",
            LastModified = "2010/12/03")]
        public string CreateAgentsDescription
        {
            get
            {
                return this["CreateAgentsDescription"];
            }
        }

        /// <summary>
        /// The title of CreateAgents security action.
        /// </summary>
        [ResourceEntry("ModifyAgents",
            Value = "Modify an agent",
            Description = "The title of ModifyAgents security action.",
            LastModified = "2010/12/03")]
        public string ModifyAgents
        {
            get
            {
                return this["ModifyAgents"];
            }
        }

        /// <summary>
        /// The description of ModifyAgents security action.
        /// </summary>
        [ResourceEntry("ModifyAgentsDescription",
            Value = "Allows or denies modifying agents",
            Description = "The description of ModifyAgents security action.",
            LastModified = "2010/12/03")]
        public string ModifyAgentsDescription
        {
            get
            {
                return this["ModifyAgentsDescription"];
            }
        }

        /// <summary>
        /// The title of DeleteAgents security action.
        /// </summary>
        [ResourceEntry("DeleteAgents",
            Value = "Delete an agent",
            Description = "The title of DeleteAgents security action.",
            LastModified = "2010/12/03")]
        public string DeleteAgents
        {
            get
            {
                return this["DeleteAgents"];
            }
        }

        /// <summary>
        /// The description of DeleteAgents security action.
        /// </summary>
        [ResourceEntry("DeleteAgentsDescription",
            Value = "Allows or denies deleting agents",
            Description = "The description of DeleteAgents security action.",
            LastModified = "2010/12/03")]
        public string DeleteAgentsDescription
        {
            get
            {
                return this["DeleteAgentsDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeAgentsOwner security action.
        /// </summary>
        [ResourceEntry("ChangeAgentsOwner",
            Value = "Change agent owner",
            Description = "The title of ChangeAgentsOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeAgentsOwner
        {
            get
            {
                return this["ChangeAgentsOwner"];
            }
        }

        /// <summary>
        /// The description of ChangeAgentsOwner security action.
        /// </summary>
        [ResourceEntry("ChangeAgentsOwnerDescription",
            Value = "Allows or denies changing the owner of an agent.",
            Description = "The description of ChangeAgentsOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeAgentsOwnerDescription
        {
            get
            {
                return this["ChangeAgentsOwnerDescription"];
            }
        }

        /// <summary>
        /// The title of ChangeAgentsPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeAgentsPermissions",
            Value = "Change agent permissions",
            Description = "The title of ChangeAgentsPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeAgentsPermissions
        {
            get
            {
                return this["ChangeAgentsPermissions"];
            }
        }

        /// <summary>
        /// The description of ChangeAgentsPermissions security action.
        /// </summary>
        [ResourceEntry("ChangeAgentsPermissionsDescription",
            Value = "Allows or denies changing the permissions of an agent.",
            Description = "The description of ChangeAgentsPermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangeAgentsPermissionsDescription
        {
            get
            {
                return this["ChangeAgentsPermissionsDescription"];
            }
        }

        /// <summary>
        /// The title of ModifyThisAgent security action: Modify action, for a specific agent item.
        /// </summary>
        [ResourceEntry("ModifyThisAgent",
            Value = "Modify this agent",
            Description = "The title of ModifyThisAgent security action.",
            LastModified = "2010/12/03")]
        public string ModifyThisAgent
        {
            get
            {
                return this["ModifyThisAgent"];
            }
        }

        /// <summary>
        /// The title of ViewThisAgent security action: View action, for a specific agent item.
        /// </summary>
        [ResourceEntry("ViewThisAgent",
            Value = "View this agent",
            Description = "The title of ViewThisAgent security action.",
            LastModified = "2010/12/03")]
        public string ViewThisAgent
        {
            get
            {
                return this["ViewThisAgent"];
            }
        }

        /// <summary>
        /// The title of DeleteThisAgent security action: Delete action, for a specific agent item.
        /// </summary>
        [ResourceEntry("DeleteThisAgent",
            Value = "Delete this agent",
            Description = "The title of DeleteThisAgent security action.",
            LastModified = "2010/12/03")]
        public string DeleteThisAgent
        {
            get
            {
                return this["DeleteThisAgent"];
            }
        }

        /// <summary>
        /// The title of ChangeOwnerOfThisAgent security action: ChangeOwner action, for a specific agent item.
        /// </summary>
        [ResourceEntry("ChangeOwnerOfThisAgent",
            Value = "Change this agent's owner",
            Description = "The title of ChangeOwner security action.",
            LastModified = "2010/12/03")]
        public string ChangeOwnerOfThisAgent
        {
            get
            {
                return this["ChangeOwnerOfThisAgent"];
            }
        }

        /// <summary>
        /// The title of ChangePermissionsOfThisAgent security action: ChangePermissions action, for a specific agent item.
        /// </summary>
        [ResourceEntry("ChangePermissionsOfThisAgent",
            Value = "Change this agent's permissions",
            Description = "The title of ChangePermissions security action.",
            LastModified = "2010/12/03")]
        public string ChangePermissionsOfThisAgent
        {
            get
            {
                return this["ChangePermissionsOfThisAgent"];
            }
        }

        #endregion

        #region Default Pages

        #region Menu page group

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("PageGroupNodeTitle",
            Value="Agents",
            Description="phrase: Agents",
            LastModified="2010/12/03")]
        public string PageGroupNodeTitle
        {
            get { return this["PageGroupNodeTitle"]; }
        }

        /// <summary>
        /// phrase: This is the page group that contains all pages for the agents module.
        /// </summary>
        [ResourceEntry("PageGroupNodeDescription",
            Value = "This is the page group that contains all pages for the agents module.",
            Description = "phrase: This is the page group that contains all pages for the agents module.",
            LastModified = "2010/12/03")]
        public string PageGroupNodeDescription
        {
            get { return this["PageGroupNodeDescription"]; }
        }

        #endregion

        #region Landing page

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("AgentsLandingPageTitle",
            Value = "Agents",
            Description = "phrase: Agents",
            LastModified = "2010/12/03")]
        public string AgentsLandingPageTitle
        {
            get { return this["AgentsLandingPageTitle"]; }
        }

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("AgentsLandingPageHtmlTitle",
            Value = "Agents",
            Description = "phrase: Agents",
            LastModified = "2010/12/03")]
        public string AgentsLandingPageHtmlTitle
        {
            get { return this["AgentsLandingPageHtmlTitle"]; }
        }

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("AgentsLandingPageUrlName",
            Value = "Agents",
            Description = "phrase: Agents",
            LastModified = "2010/12/03")]
        public string AgentsLandingPageUrlName
        {
            get { return this["AgentsLandingPageUrlName"]; }
        }

        /// <summary>
        /// phrase: Landing page for the Agents module
        /// </summary>
        [ResourceEntry("AgentsLandingPageDescription",
            Value = "Landing page for the Agents module",
            Description = "phrase: Landing page for the Agents module",
            LastModified = "2010/12/03")]
        public string AgentsLandingPageDescription
        {
            get { return this["AgentsLandingPageDescription"]; }
        }

        #endregion

        #endregion

        #region ContentView registration
        
        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("AgentsViewTitle",
            Value = "Agents",
            Description = "phrase: Agents",
            LastModified = "2010/12/03")]
        public string AgentsViewTitle
        {
            get { return this["AgentsViewTitle"]; }
        }
     
        /// <summary>
        /// phrase: Widget that displays agent items
        /// </summary>
        [ResourceEntry("AgentsViewDescription",
            Value = "Widget that displays agent items",
            Description = "phrase: Widget that displays agent items",
            LastModified = "2010/12/03")]
        public string AgentsViewDescription
        {
            get { return this["AgentsViewDescription"]; }
        }

        #endregion

        /// <summary>
        /// The title of the edit item dialog
        /// </summary>
        [ResourceEntry("EditItem",
            Value = "Edit an agent",
            Description = "The title of the edit item dialog",
            LastModified = "2009/12/20")]
        public string EditItem
        {
            get { return this["EditItem"]; }
        }
        
        [ResourceEntry("Email",
        Value = "Email",
        Description = "Email",
        LastModified = "2010/12/03")]
        public string Email
        {
            get
            {
                return this["Email"];
            }
        }

        /// <summary>
        /// phrase: Title cannot be empty
        /// </summary>
        [ResourceEntry("EmailCannotBeEmpty",
            Value = "Email cannot be empty",
            Description = "phrase: Email cannot be empty",
            LastModified = "2010/12/09")]
        public string EmailCannotBeEmpty
        {
            get { return this["EmailCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: The email is not valid
        /// </summary>
        [ResourceEntry("EmailMustBeValid",
        Value = "The email is not valid",
        Description = "The email must be valid",
        LastModified = "2010/12/09")]
        public string EmailMustBeValid
        {
            get
            {
                return this["EmailMustBeValid"];
            }
        }

        [ResourceEntry("PhoneNumber",
        Value = "Phone Number",
        Description = "phrase: Phone Number",
        LastModified = "2010/12/03")]
        public string PhoneNumber
        {
            get
            {
                return this["PhoneNumber"];
            }
        }

        [ResourceEntry("Telephone",
        Value = "Tel.",
        Description = "phrase: Tel.",
        LastModified = "2010/02/11")]
        public string Telephone
        {
            get
            {
                return this["Telephone"];
            }
        }

        [ResourceEntry("Address",
        Value = "Address",
        Description = "word: Address",
        LastModified = "2010/12/03")]
        public string Address
        {
            get
            {
                return this["Address"];
            }
        }

        [ResourceEntry("PostalCode",
        Value = "Postal Code",
        Description = "phrase: Postal Code",
        LastModified = "2010/12/03")]
        public string PostalCode
        {
            get
            {
                return this["PostalCode"];
            }
        }

        [ResourceEntry("Name",
        Value = "Name",
        Description = "word: Name",
        LastModified = "2010/12/03")]
        public string Name
        {
            get
            {
                return this["Name"];
            }
        }

        [ResourceEntry("Property",
        Value = "Property",
        Description = "word: Property",
        LastModified = "2010/12/03")]
        public string Property
        {
            get
            {
                return this["Property"];
            }
        }

        [ResourceEntry("Message",
        Value = "Message",
        Description = "word: Message",
        LastModified = "2010/12/03")]
        public string Message
        {
            get
            {
                return this["Message"];
            }
        }

        [ResourceEntry("SendACopyContactForm",
        Value = "Send a copy of this email to yourself",
        Description = "Send a copy text on the contact form",
        LastModified = "2010/02/11")]
        public string SendACopyContactForm
        {
            get
            {
                return this["SendACopyContactForm"];
            }
        }

        [ResourceEntry("SendButtonTextContactForm",
        Value = "Send",
        Description = "Send button text on the contact form",
        LastModified = "2010/02/11")]
        public string SendButtonTextContactForm
        {
            get
            {
                return this["SendButtonTextContactForm"];
            }
        }

        [ResourceEntry("MessageSentContactForm",
        Value = "Your message has been sent.",
        Description = "Message to show when contact form is sent.",
        LastModified = "2010/02/11")]
        public string MessageSentContactForm
        {
            get
            {
                return this["MessageSentContactForm"];
            }
        }

        [ResourceEntry("MessageNotSentContactForm",
        Value = "An error occured while sending your message. Please try again.",
        Description = "Message to show when contact form was NOT sent.",
        LastModified = "2010/02/11")]
        public string MessageNotSentContactForm
        {
            get
            {
                return this["MessageNotSentContactForm"];
            }
        }

        [ResourceEntry("ContactAgent",
        Value = "Contact agent",
        Description = "Contact agent",
        LastModified = "2010/02/11")]
        public string ContactAgent
        {
            get
            {
                return this["ContactAgent"];
            }
        }

        [ResourceEntry("AgentsListTitle",
        Value = "Our Agents",
        Description = "Agents list title",
        LastModified = "2010/02/11")]
        public string AgentsListTitle
        {
            get
            {
                return this["AgentsListTitle"];
            }
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
        /// Messsage: Create agents item
        /// </summary>
        /// <value>Label of the dialog that creates an agent item.</value>
        [ResourceEntry(
            "CreateItem",
            Value = "Create an agent item",
            Description = "Label of the dialog that creates an agent item.",
            LastModified = "2010/6/27")
        ]
        public string CreateItem
        {
            get { return this["CreateItem"]; }
        }


        /// <summary>
        /// phrase: No agents items have been created yet.
        /// </summary>
        [ResourceEntry("NoAgentItems",
            Value = "No agent items have been created yet",
            Description = "phrase: No agent items have been created yet",
            LastModified = "2010/07/26")]
        public string NoAgentItems
        {
            get
            {
                return this["NoAgentItems"];
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
        /// Phrase: Agents items by category
        /// </summary>
        [ResourceEntry("AgentItemsByCategory",
            Value = "Agent items by category",
            Description = "Phrase: Agent items by category",
            LastModified = "2010/07/23")]
        public string AgentItemsByCategory
        {
            get
            {
                return this["AgentItemsByCategory"];
            }
        }


        /// <summary>
        /// Phrase: Agents items by tag
        /// </summary>
        [ResourceEntry("AgentItemsByTag",
            Value = "Agent items by tag",
            Description = "Phrase: Agent items by tag",
            LastModified = "2010/07/23")]
        public string AgentItemsByTag
        {
            get
            {
                return this["AgentItemsByTag"];
            }
        }

        /// <summary>
        /// The text of last updated agents sidebar button
        /// </summary>
        [ResourceEntry("DisplayLastUpdatedAgents",
            Value = "Display agent items last updated in...",
            Description = "The text of last updated agents sidebar button",
            LastModified = "2010/08/20")]
        public string DisplayLastUpdatedAgents
        {
            get
            {
                return this["DisplayLastUpdatedAgents"];
            }
        }


        /// <summary>
        /// phrase: Filter agents
        /// </summary>
        [ResourceEntry("FilterAgents",
            Value = "Filter agents",
            Description = "phrase: Filter agents",
            LastModified = "2010/01/25")]
        public string FilterAgents
        {
            get { return this["FilterAgents"]; }
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
        /// Phrase: Edit News settings 
        /// </summary>
        [ResourceEntry("EditAgentsSettings",
            Value = "Set which agents to display",
            Description = "Phrase: Edit News settings",
            LastModified = "2010/11/13")]
        public string EditAgentsSettings
        {
            get
            {
                return this["EditAgentsSettings"];
            }
        }

        /// <summary>
        /// phrase: Agents Settings.
        /// </summary>
        [ResourceEntry("Settings",
            Value = "Settings for agents",
            Description = "phrase: Agents Settings",
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
        /// Phrase: All agents
        /// </summary>
        [ResourceEntry("AllAgents",
            Value = "All agents",
            Description = "Phrase: All agents",
            LastModified = "2010/07/27")]
        public string AllAgents
        {
            get
            {
                return this["AllAgents"];
            }
        }


        /// <summary>
        /// The text of my agents sidebar button
        /// </summary>
        [ResourceEntry("MyAgents",
            Value = "My agents",
            Description = "The text of my agents sidebar button",
            LastModified = "2010/08/19")]
        public string MyAgents
        {
            get
            {
                return this["MyAgents"];
            }
        }

        /// <summary>
        /// phrase: Published
        /// </summary>
        [ResourceEntry("PublishedAgents",
            Value = "Published",
            Description = "The text of published agents sidebar button.",
            LastModified = "2010/08/19")]
        public string PublishedAgents
        {
            get
            {
                return this["PublishedAgents"];
            }
        }

        /// <summary>
        /// word: Drafts
        /// </summary>
        [ResourceEntry("DraftAgents",
            Value = "Drafts",
            Description = "The text of draft agents sidebar button.",
            LastModified = "2010/08/19")]
        public string DraftAgents
        {
            get
            {
                return this["DraftAgents"];
            }
        }



        /// <summary>
        /// word: Scheduled
        /// </summary>
        [ResourceEntry("ScheduledAgents",
            Value = "Scheduled",
            Description = "The text of scheduled agents sidebar button.",
            LastModified = "2010/08/20")]
        public string ScheduledAgents
        {
            get
            {
                return this["ScheduledAgents"];
            }
        }

        /// <summary>
        /// phrase: Waiting for approval
        /// </summary>
        [ResourceEntry("WaitingForApproval",
            Value = "Waiting for approval",
            Description = "The text of the 'Waiting for approval' button in the agents sidebar.",
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
        /// phrase: Comments for agents.
        /// </summary>
        [ResourceEntry("CommentsForAgents",
            Value = "Comments for agents",
            Description = "phrase: Comments for agents",
            LastModified = "2010/01/25")]
        public string CommentsForAgents
        {
            get { return this["CommentsForAgents"]; }
        }


        /// <summary>
        /// phrase: Permissions for agents
        /// </summary>
        [ResourceEntry("PermissionsForAgents",
            Value = "Permissions",
            Description = "phrase: Permissions for agents",
            LastModified = "2010/01/29")]
        public string PermissionsForAgents
        {
            get { return this["PermissionsForAgents"]; }
        }


        /// <summary>
        /// phrase: Settings for agents
        /// </summary>
        [ResourceEntry("SettingsForAgents",
            Value = "Settings",
            Description = "phrase: Settings for agents",
            LastModified = "2010/01/29")]
        public string SettingsForAgents
        {
            get { return this["SettingsForAgents"]; }
        }

        /// <summary>
        /// The title of the create new item dialog
        /// </summary>
        [ResourceEntry("CreateNewItem",
            Value = "Create an agent item",
            Description = "The title of the create new item dialog",
            LastModified = "2010/07/26")]
        public string CreateNewItem
        {
            get { return this["CreateNewItem"]; }
        }

        /// <summary>
        /// Back to agents
        /// </summary>
        [ResourceEntry("BackToItems",
                       Value = "Back to Agents",
                       Description = "The text of the back to agents link",
                       LastModified = "2010/10/13")]
        public string BackToItems
        {
            get
            {
                return this["BackToItems"];
            }
        }

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("ModuleTitle",
            Value = "Agents",
            Description = "phrase: Agents",
            LastModified = "2010/12/07")]
        public string ModuleTitle
        {
            get { return this["ModuleTitle"]; }
        }

        /// <summary>
        /// phrase: Agents
        /// </summary>
        [ResourceEntry("WhatIsInTheBox",
            Value = "What's in the box",
            Description = "phrase: Agents",
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
        /// phrase: Name cannot be empty
        /// </summary>
        [ResourceEntry("NameCannotBeEmpty",
            Value = "Name cannot be empty",
            Description = "phrase: Name cannot be empty",
            LastModified = "2010/02/11")]
        public string NameCannotBeEmpty
        {
            get { return this["NameCannotBeEmpty"]; }
        }

        /// <summary>
        /// phrase: Message cannot be empty
        /// </summary>
        [ResourceEntry("MessageCannotBeEmpty",
            Value = "Message cannot be empty",
            Description = "phrase: Message cannot be empty",
            LastModified = "2010/02/11")]
        public string MessageCannotBeEmpty
        {
            get { return this["MessageCannotBeEmpty"]; }
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
        public string UrlNameInvalid {
            get { return this["UrlNameInvalid"]; }
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
