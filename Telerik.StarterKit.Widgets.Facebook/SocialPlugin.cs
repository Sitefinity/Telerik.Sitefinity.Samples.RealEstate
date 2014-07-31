using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.StarterKit.Widgets.Facebook
{

    [ControlDesigner(typeof(SocialPluginDesigner))]
    public class SocialPlugin : SimpleView
    {

        #region Constants

        private const string layoutTemplateName = "Telerik.StarterKit.Widgets.Facebook.Resources.Views.SocialPluginTemplate.ascx";

        #endregion

        #region Properties

        [MultilingualProperty]
        public string Title
        {
            get;
            set;
        }

        [MultilingualProperty]
        public string FacebookPageUrl
        {
            get;
            set;
        }

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
                return layoutTemplateName;
            }
        }

        #endregion

        #region Control references

        protected virtual ITextControl ContentTitle
        {
            get
            {
                return this.Container.GetControl<ITextControl>("contentTitle", true);
            }
        }

        protected virtual HtmlGenericControl ContentIFrame
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("contentIFrame", true);
            }
        }

        protected virtual HyperLink ContentFindUsHyperlink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("contentFindUsHyperlink", true);
            }
        }

        #endregion

        #region Overridden controls

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            this.ContentTitle.Text = this.Title;

            //if Facebook Page URL is not set then the box will be invisible
            if (!this.FacebookPageUrl.IsNullOrWhitespace())
            {
                //format url to be added like query string in the real facebook link
                string formattedUrl = this.FacebookPageUrl.Replace(":", "%3A").Replace("/", "%2F");

                //format facebook url like it is shown in Like Box plugin in developers.facebook.com/docs/reference/plugins/like-box
                string likeBoxPluginUrl = string.Format(
                    @"http://www.facebook.com/plugins/likebox.php?href={0}&width=276&colorscheme=light&connections=8&stream=false&header=false&height=287",
                    formattedUrl);

                //add facebook Like Box plugin URL to iframe like src tag
                this.ContentIFrame.Attributes.Add("src", likeBoxPluginUrl);

                //set navigation URL to 'Find us on Facebook' link
                this.ContentFindUsHyperlink.NavigateUrl = this.FacebookPageUrl;
            }
            else
            {
                if (!this.IsBackend() && !this.IsDesignMode())
                {
                    this.Visible = false;
                }
            }        
        }

        #endregion
    }
}
