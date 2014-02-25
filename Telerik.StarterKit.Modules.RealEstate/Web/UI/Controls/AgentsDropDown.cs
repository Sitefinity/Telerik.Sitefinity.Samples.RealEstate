using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI.Fields;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.StarterKit.Modules.Agents.Data;

namespace Telerik.StarterKit.Modules.RealEstate.Web.UI.Controls
{
    public class AgentsDropDown : ChoiceField
    {
        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
            }
        }

        public override void Configure(IFieldDefinition definition)
        {
            base.Configure(definition);
            var aManager = AgentsManager.GetManager();
            var agents = aManager.GetAgents().OrderBy(a => a.Title).Where(a => a.Status == Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);
            if (agents != null)
            {
                this.RenderChoicesAs = Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.DropDown; //
                // or you can use Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.CheckBoxes for multiple choice

                this.Choices.Clear();
                foreach (var agent in agents)
                {
                    var choice = new ChoiceItem();
                    choice.Value = agent.Id.ToString();
                    choice.Text = agent.Title;
                    choice.Enabled = true;
                    this.Choices.Add(choice);
                }
            }
        }

        public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var assemblyName = typeof(AgentsDropDown).Assembly.FullName;
            var scripts = new List<ScriptReference>(base.GetScriptReferences())
                              {
                                  new ScriptReference(AgentsDropDown.script, assemblyName),
                              };
            return scripts;

        }

        #region Private Fields and constants

        internal const string script = "Telerik.StarterKit.Modules.RealEstate.Web.UI.Controls.Scripts.AgentsDropDown.js";

        #endregion
    }
}
