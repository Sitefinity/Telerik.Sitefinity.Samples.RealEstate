using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Taxonomies;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Designers
{
    public class RealEstateViewContentSelectorsDesignerView : ContentSelectorsDesignerView
    {
        internal const string layoutTemplateName = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.Resources.RealEstateViewContentSelectorsDesignerViewTemplate.ascx";

        protected override string LayoutTemplateName
        {
            get
            {
                return null;
            }
        }

        public override string LayoutTemplatePath
        {
            get
            {
                var path = "~/SFRealEstate/" + layoutTemplateName;
                return path;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override void InitializeControls(Sitefinity.Web.UI.GenericContainer container)
        {
            //base.InitializeControls(container);

            ////this.ContentSelector.ServiceUrl = "~/Sitefinity/Services/Content/RealEstate.svc";
            //this.FilterSelector.SetTaxonomyId("Types", RealEstateModule.TypesTaxonomyId);

            if (this.ChooseAllText != null)
            {
                this.RadioChoiceAll.Text = this.ChooseAllText;
            }
            if (this.ChooseSingleText != null)
            {
                this.RadioChoiceSingle.Text = this.ChooseSingleText;
            }
            if (this.ChooseSimpleFilterText != null)
            {
                this.RadioChoiceSimpleFilter.Text = this.ChooseSimpleFilterText;
            }
            if (this.ChooseAdvancedFilterText != null)
            {
                this.RadioChoiceAdvancedFilter.Text = this.ChooseAdvancedFilterText;
            }
            if (this.ContentTitleText != null)
            {
                this.ChoicesTitleLiteral.Text = this.ContentTitleText;
            }
            if (this.SingleSelectorButtonText != null)
            {
                this.SelectContentButton.Text = this.SingleSelectorButtonText;
            }
            if (this.SelectedContentTitleText != null)
            {
                this.SelectedContentLabel.Text = this.SelectedContentTitleText;
            }
            else
            {
                this.SelectedContentLabel.Style.Add("display", "none");
            }
            this.ContentSelector.ServiceUrl = "~/Sitefinity/Services/Content/RealEstate.svc/";
            string str = "Visible == true AND Status == Live";
            string uICulture = this.GetUICulture();
            this.ContentSelector.UICulture = uICulture;
            if (this.AddCultureToFilter && !string.IsNullOrEmpty(uICulture))
            {
                str = str + string.Format(" AND Culture == {0}", uICulture);
            }
            this.ContentSelector.ItemsFilter = str;
            this.FilterSelector.SetTaxonomyId("Categories", TaxonomyManager.CategoriesTaxonomyId);
            this.FilterSelector.SetTaxonomyId("Types", RealEstateModule.TypesTaxonomyId);

        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
            string assembly = typeof(ContentSelectorsDesignerView).Assembly.GetName().ToString();
            scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
            scriptReferences.Add(new ScriptReference(script, this.GetType().Assembly.GetName().ToString()));
            scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", assembly));
            return scriptReferences;

        }

        public override IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
            descriptor.AddProperty("_selectContentButton", this.SelectContentButton.ClientID);
            descriptor.AddElementProperty("selectedContentTitle", this.SelectedContentLabel.ClientID);
            descriptor.AddComponentProperty("contentSelector", this.ContentSelector.ClientID);
            descriptor.AddComponentProperty("filterSelector", this.FilterSelector.ClientID);
            if (this.NarrowSelectionButton != null)
            {
                descriptor.AddElementProperty("btnNarrowSelection", this.NarrowSelectionButton.ClientID);
            }
            if (this.NarrowSelectionContainer != null)
            {
                descriptor.AddElementProperty("narrowSelection", this.NarrowSelectionContainer.ClientID);
            }
            return new ScriptControlDescriptor[] { descriptor };

        }

        internal const string script = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Controls.Scripts.RealEstateViewContentSelectorsDesignerView.js";
    }
}
