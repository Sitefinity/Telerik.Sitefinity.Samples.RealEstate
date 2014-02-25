using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Model;
using Telerik.StarterKit.Modules.Agents.Data.Implementation;

namespace Telerik.StarterKit.Modules.Agents.Data
{
    public class AgentsFluentMetadataSource : ContentBaseMetadataSource
    {
        public AgentsFluentMetadataSource()
            : base(null)
        { }


        public AgentsFluentMetadataSource(IDatabaseMappingContext context)
            : base(context)
        {

        }

        protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
        {
            var sitefinityMappings = base.BuildCustomMappings();
            sitefinityMappings.Add(new AgentsFluentMapping(this.Context));
            return sitefinityMappings;

        }
    }
}
