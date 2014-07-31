using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Web.UI;

namespace Telerik.StarterKit.Modules.Agents.Web.UI.Public
{
    /// <summary>
    /// Represents a view that displays detailed information about a specified content item
    /// </summary>
    public class DetailsView : ViewBase
    {
        /// <summary>
        /// Gets the name of the embedded layout template.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Override this property to change the embedded template to be used with the dialog
        /// </remarks>
        protected override string LayoutTemplateName
        {
            get
            {
                var templateName = base.LayoutTemplateName;
                if (string.IsNullOrEmpty(templateName))
                    return layoutTemplateName;
                return templateName;
            }
        }

        /// <summary>
        /// Gets the repeater for news list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView DetailsViewControl
        {
            get
            {
                return this.Container.GetControl<RadListView>("DetailsView", true);
            }
        }

        /// <summary>
        /// Gets the repeater for email
        /// </summary>
        /// <value>Email.</value>
        protected internal virtual FieldListView EmailControl
        {
            get
            {
                return (FieldListView)this.FindControlRecursive("emailControl");
            }
        }

        /// <summary>
        /// Gets the repeater for phone number
        /// </summary>
        /// <value>Phone Number.</value>
        protected internal virtual FieldListView PhoneNumberControl
        {
            get
            {
                return (FieldListView)this.FindControlRecursive("phoneNumberControl");
            }
        }

        /// <summary>
        /// Gets the repeater for address
        /// </summary>
        /// <value>Address.</value>
        protected internal virtual FieldListView AddressControl
        {
            get
            {
                return (FieldListView)this.FindControlRecursive("addressControl");
            }
        }

        /// <summary>
        /// Gets the repeater for postal code
        /// </summary>
        /// <value>Postal Code.</value>
        protected internal virtual FieldListView PostalCodeControl
        {
            get
            {
                return (FieldListView)this.FindControlRecursive("postalCodeControl");
            }
        }

        /// <summary>
        /// Gets the reference to the hidden field which holds the email web service url.
        /// </summary>
        protected virtual HiddenField WebServiceUrlHidden
        {
            get
            {
                return this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);
            }
        }

        #region Overridden methods

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                this.AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
            }
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                base.RenderEndTag(writer);
            }
        }

        /// <summary>
        /// Gets or sets the layout template.
        /// </summary>
        public override ITemplate LayoutTemplate
        {
            get
            {
                if (this.layoutTemplate == null)
                {
                    var tempInfo = new TemplateInfo()
                    {
                        TemplatePath = this.LayoutTemplatePath,
                        TemplateName = this.LayoutTemplateName,
                        TemplateResourceInfo = typeof(DetailsView),
                        ControlType = this.GetType(),
                        Key = TemplateKey
                    };

                    this.layoutTemplate = ControlUtilities.GetTemplate(tempInfo);
                }
                return this.layoutTemplate;
            }
            set
            {
                this.layoutTemplate = value;
            }
        }


        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container">The controls container.</param>
        /// <param name="definition">The content view definition.</param>
        protected override void InitializeControls(GenericContainer container, IContentViewDefinition definition)
        {
            var detailDefinition = definition as IContentViewDetailDefinition;
            if (detailDefinition != null)
            {
                var agentsView = (AgentsView)this.Host;
                this.WebServiceUrlHidden.Value = this.webServiceUrl;

                var item = agentsView.DetailItem as AgentItem;
                if (item == null)
                {
                    if (this.IsDesignMode())
                    {
                        this.Controls.Clear();
                        this.Controls.Add(new LiteralControl("An agent item was not selected or has been deleted. Please select another one."));
                    }
                    return;
                }
                else
                {
                    this.librariesManager.Provider.SuppressSecurityChecks = true;
                    this.DetailsViewControl.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(DetailsView_ItemDataBound);
                    this.DetailsViewControl.DataSource = new AgentItem[] { item };
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the ItemDataBound event of the DetailsView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadListViewItemEventArgs"/> instance containing the event data.</param>
        private void DetailsView_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
            {
                return;
            }

            AgentItem agent = ((RadListViewDataItem)e.Item).DataItem as AgentItem;
            if (agent == null)
            {
                return;
            }

            //System.Web.UI.WebControls.Image photo = (System.Web.UI.WebControls.Image)e.Item.FindControl("Photo");
            HtmlGenericControl photo = (HtmlGenericControl)e.Item.FindControl("photo");

            var album = App.Prepare().WorkWith().Albums().Where(a => a.UrlName == "agents").Get().FirstOrDefault();
            if (album == null)
            {
                return;
            }

            var image = album.Images().Where(i => i.Title == agent.Title && i.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).SingleOrDefault();
            if (image == null)
            {
                return;
            }
            
            photo.InnerHtml = string.Format("<img src='{0}{1}' />", librariesManager.GetItemUrl(image), image.Extension);
        }

        #endregion

        #region Helper methods

        private void InitCommentsView(ContentView view)
        {
            if (view != null)
            {
                view.ControlDefinition.ProviderName = this.Host.ControlDefinition.ProviderName;
                var detailItem = this.Host.DetailItem as Telerik.Sitefinity.GenericContent.Model.Content;
                if (detailItem != null)
                {
                    view.DetailItem = detailItem;
                }
            }
        }
        private Control FindControlRecursive(string iD)
        {
            return this.FindControlRecursive(this, iD);
        }
        internal Control FindControlRecursive(Control searcher, string iD)
        {
            //First search the same naming container:
            if (searcher.NamingContainer != null)
            {
                Control searched = searcher.NamingContainer.FindControl(iD);
                if (searched != null)
                {
                    return searched;
                }
            }

            Control root;
            if (searcher.Page.Master != null)
            {
                root = searcher.Page.Master;
            }
            else
            {
                root = searcher.Page;
            }

            Control control = root.FindControl(iD);
            if (control != null)
            {
                return control;
            }

            control = searcher.Page.FindControl(iD);
            if (control != null)
            {
                return control;
            }

            if (searcher.UniqueID == iD || searcher.ClientID == iD)
            {
                return searcher;
            }

            return FindControlRecursive(iD, root);
        }
        private Control FindControlRecursive(string ID, Control root)
        {
            Control control = null;

            if (root is System.Web.UI.WebControls.DataBoundControl && !root.Visible)
                return control;

            foreach (Control ctrl in root.Controls)
            {
                if (ctrl is INamingContainer && ctrl.FindControl(ID) != null)
                {
                    control = ctrl.FindControl(ID);
                    break;
                }
                else if (ctrl.HasControls())
                {
                    control = FindControlRecursive(ID, ctrl);
                    if (control != null && (control.UniqueID == ID || control.ID == ID))
                    {
                        break;
                    }
                }
            }
            return control;
        }

        #endregion
        
        #region Private Fields & Constants

        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.Agents.Web.UI.Public.Resources.DetailsView.ascx";
        private ITemplate layoutTemplate;
        private Telerik.Sitefinity.Modules.Libraries.LibrariesManager librariesManager = new Sitefinity.Modules.Libraries.LibrariesManager();
        private readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/Mailer.svc/");
        #endregion
    }
}
