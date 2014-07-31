using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.StarterKit.Modules.Agents.Data;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Web.UI;

namespace Telerik.StarterKit.Modules.Agents.Web.UI.Public
{
    /// <summary>
    /// Represents master view that displays a collection content items as list.
    /// </summary>
    public class MasterListView : ViewBase
    {
        #region Properties
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
        /// Gets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected AgentsManager Manager
        {
            get
            {
                if (this.manager == null)
                    this.manager = AgentsManager.GetManager(this.Host.ControlDefinition.ProviderName);

                return this.manager;
            }
        }

        protected LibrariesManager LibMan
        {
            get
            {
                if (this.libManager == null)
                    this.libManager = LibrariesManager.GetManager();
                return this.libManager;
            }
        }

        #endregion

        #region Control References

        /// <summary>
        /// Gets the repeater for agents list.
        /// </summary>
        /// <value>The repeater.</value>
        protected internal virtual RadListView AgentsListControl
        {
            get
            {
                return this.Container.GetControl<RadListView>("AgentsList", true);
            }
        }

        /// <summary>
        /// Gets the pager.
        /// </summary>
        /// <value>The pager.</value>
        protected internal virtual Pager Pager
        {
            get
            {
                return this.Container.GetControl<Pager>("pager", true);
            }
        }

        #endregion

        #region Methods

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
                        TemplateResourceInfo = typeof(MasterListView),
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
            var masterDefinition = definition as IContentViewMasterDefinition;
            if (masterDefinition != null)
            {
                var query = this.Manager.GetAgents();
                if (masterDefinition.AllowUrlQueries.HasValue && masterDefinition.AllowUrlQueries.Value)
                {
                    query = this.EvaluateUrl(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                    query = this.EvaluateUrl(query, "Taxonomy", "", typeof(AgentItem), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                int? totalCount = 0;
                int? itemsToSkip = 0;
                if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value)
                {
                    itemsToSkip = this.GetItemsToSkipCount(masterDefinition.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
                }

                CultureInfo uiCulture = null;
                if (Config.Get<ResourcesConfig>().Multilingual)
                {
                    uiCulture = System.Globalization.CultureInfo.CurrentUICulture;
                }
                var filterExpression = DefinitionsHelper.GetFilterExpression(masterDefinition);
                query = Telerik.Sitefinity.Data.DataProviderBase.SetExpressions(
                    query,
                    filterExpression,
                    masterDefinition.SortExpression,
                    uiCulture,
                    itemsToSkip,
                    masterDefinition.ItemsPerPage,
                    ref totalCount);

                this.IsEmptyView = (totalCount == 0);

                if (totalCount == 0)
                {
                    this.AgentsListControl.Visible = false;
                }
                else
                {
                    this.ConfigurePager(totalCount.Value, masterDefinition);
                    this.librariesManager.Provider.SuppressSecurityChecks = true;
                    this.AgentsListControl.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(AgentsListControl_ItemDataBound);
                    this.AgentsListControl.DataSource = query.ToList();
                }
            }
        }

        void AgentsListControl_ItemDataBound(object sender, RadListViewItemEventArgs e)
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
            HtmlGenericControl photoDiv = (HtmlGenericControl)e.Item.FindControl("photoDiv");

            var album = this.LibMan.GetAlbums().Where(a => a.Title == "Agents").SingleOrDefault();
                //; App.WorkWith().Albums().Where(a => a.Title == "Agents").Get().FirstOrDefault();
            if (album == null)
            {
                return;
            }
            var agentTitle = agent.Title.ToString();
            var image = this.LibMan.GetImages().Where(i => i.Title == agentTitle 
                    && i.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live
                    && i.Parent == album).SingleOrDefault();
            //var image = album.Images().Where(i => i.Title == agent.Title.ToString() && i.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live).SingleOrDefault();
            if (image == null)
            {
                return;
            }

            photoDiv.InnerHtml = string.Format("<img src='{0}{1}' />", librariesManager.GetItemUrl(image), image.Extension);

            //Telerik.Sitefinity.Web.UI.ContentUI.DetailsViewHyperLink link1 = (Telerik.Sitefinity.Web.UI.ContentUI.DetailsViewHyperLink)e.Item.FindControl("DetailsViewHyperLink1");
            //Telerik.Sitefinity.Web.UI.ContentUI.DetailsViewHyperLink link2 = (Telerik.Sitefinity.Web.UI.ContentUI.DetailsViewHyperLink)e.Item.FindControl("DetailsViewHyperLink2");

            //link1.NavigateUrl = link1.NavigateUrl + "?popup=true";
            //link2.NavigateUrl = link2.NavigateUrl + "?popup=true";
        }


        /// <summary>
        /// Configures the pager.
        /// </summary>
        /// <param name="vrtualItemCount">The virtual item count.</param>
        /// <param name="masterDefinition">The master definition.</param>
        protected virtual void ConfigurePager(int virtualItemCount, IContentViewMasterDefinition masterDefinition)
        {
            if (masterDefinition.AllowPaging.HasValue &&
                masterDefinition.AllowPaging.Value &&
                masterDefinition.ItemsPerPage.GetValueOrDefault() > 0)
            {
                this.Pager.VirtualItemCount = virtualItemCount;
                this.Pager.PageSize = masterDefinition.ItemsPerPage.Value;
                this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
            }
            else
            {
                this.Pager.Visible = false;
            }
        }
        #endregion

        #region Private fields

        private AgentsManager manager;
        private LibrariesManager libManager;
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.Agents.Web.UI.Public.Resources.MasterListView.ascx";
        private ITemplate layoutTemplate;
        Telerik.Sitefinity.Modules.Libraries.LibrariesManager librariesManager = new Sitefinity.Modules.Libraries.LibrariesManager();
        #endregion
    }
}
