using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Model;
using Telerik.StarterKit.Modules.RealEstate.Data.Implementation;

namespace Telerik.StarterKit.Modules.RealEstate.Data
{
    public class RealEstateFluentMetadataSource : ContentBaseMetadataSource
    {
        public RealEstateFluentMetadataSource()
            : base(null)
        { 
        }

        public RealEstateFluentMetadataSource(IDatabaseMappingContext context)
            : base(context)
        {
        }

        protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
        {
            var sitefinityMappings = base.BuildCustomMappings();
            sitefinityMappings.Add(new RealEstateFluentMapping(this.Context));
            return sitefinityMappings;
        }
    }
}
