using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers
{
    public class SliderWidgetDesigner : ControlDesignerBase
    {
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.SliderWidgetDesignerTemplate.ascx";
        internal const string designerScriptName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Controls.Scripts.SliderWidgetDesigner.js";
        private const string pageSelectorScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PageSelector.js";

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

        /// <summary>
        /// Gets a reference to the page select button
        /// </summary>
        // Page Selector add
        protected LinkButton PageSelectButtonForRent
        {
            get
            {
                return Container.GetControl<LinkButton>("pageSelectButtonForRent", true);
            }
        }

        /// <summary>
        /// Gets a reference to the page selector
        /// </summary>
        // Page Selector add
        protected PageSelector PageSelectorForRent
        {
            get
            {
                return Container.GetControl<PageSelector>("pageSelectorForRent", true);
            }
        }

        /// <summary>
        /// Gets the correct instance of the RadWindowManager class
        /// </summary>
        // Page Selector add
        protected virtual RadWindowManager RadWindowManagerForRent
        {
            get
            {
                return this.Container.GetControl<RadWindowManager>("windowManagerForRent", true);
            }
        }

        /// <summary>
        /// Gets a reference to the page select button
        /// </summary>
        // Page Selector add
        protected LinkButton PageSelectButtonForSale
        {
            get
            {
                return Container.GetControl<LinkButton>("pageSelectButtonForSale", true);
            }
        }

        /// <summary>
        /// Gets a reference to the page selector
        /// </summary>
        // Page Selector add
        protected PageSelector PageSelectorForSale
        {
            get
            {
                return Container.GetControl<PageSelector>("pageSelectorForSale", true);
            }
        }

        /// <summary>
        /// Gets the correct instance of the RadWindowManager class
        /// </summary>
        // Page Selector add
        protected virtual RadWindowManager RadWindowManagerForSale
        {
            get
            {
                return this.Container.GetControl<RadWindowManager>("windowManagerForSale", true);
            }
        }

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
            //descriptor.AddComponentProperty("numberOfItemsToShowEditor", this.NumberOfNewsToShowEditor.ClientID);
            // Page Selector
            descriptor.AddElementProperty("pageSelectButtonForRent", this.PageSelectButtonForRent.ClientID);
            descriptor.AddComponentProperty("pageSelectorForRent", this.PageSelectorForRent.ClientID);

            descriptor.AddElementProperty("pageSelectButtonForSale", this.PageSelectButtonForSale.ClientID);
            descriptor.AddComponentProperty("pageSelectorForSale", this.PageSelectorForSale.ClientID);

            scriptDescriptors.Add(descriptor);
            return scriptDescriptors.ToArray();
        }


        /// <summary>
        /// Gets the script references.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            string assembly = typeof(SliderWidgetDesigner).Assembly.FullName;
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(SliderWidgetDesigner.designerScriptName, assembly));
            //scripts.Add(new ScriptReference(pageSelectorScript, typeof(Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector).Assembly.FullName));
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
