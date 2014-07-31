using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.StarterKit.Widgets.Facebook
{
    public class SocialPluginDesigner : ControlDesignerBase
    {
        private const string layoutTemplateName = "Telerik.StarterKit.Widgets.Facebook.Resources.Views.SocialPluginDesignerTemplate.ascx";
        private const string designerScriptName = "Telerik.StarterKit.Widgets.Facebook.Resources.Scripts.SocialPluginDesigner.js";

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
                return layoutTemplateName;
            }
        }

        #endregion

        #region Control references

        //protected virtual TextField TitleEditor
        //{
        //    get
        //    {
        //        return this.Container.GetControl<TextField>("titleEditor", true);
        //    }
        //}

        //protected virtual TextField FacebookPageUrlEditor
        //{
        //    get
        //    {
        //        return this.Container.GetControl<TextField>("facebookPageUrlEditor", true);
        //    }
        //}

        #endregion

        #region Overridden methods

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            base.DesignerMode = ControlDesignerModes.Simple;
            base.AdvancedModeIsDefault = false;
        }

        /// <summary>
        /// Gets the script descriptors.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();
            //descriptor.AddComponentProperty("titleEditor", this.TitleEditor.ClientID);
            //descriptor.AddComponentProperty("facebookPageUrlEditor", this.FacebookPageUrlEditor.ClientID);
            scriptDescriptors.Add(descriptor);
            return scriptDescriptors.ToArray();
        }

        /// <summary>
        /// Gets the script references.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            string assembly = typeof(SocialPluginDesigner).Assembly.FullName;
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            var designerScript = new ScriptReference(SocialPluginDesigner.designerScriptName, assembly);
            scripts.Add(designerScript);
            return scripts.ToArray();
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                //we use div wrapper tag to make easier common styling
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion
    }
}
