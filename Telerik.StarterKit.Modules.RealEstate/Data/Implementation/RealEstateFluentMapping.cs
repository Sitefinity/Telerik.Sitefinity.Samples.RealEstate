using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace Telerik.StarterKit.Modules.RealEstate.Data.Implementation
{
    public class RealEstateFluentMapping : OpenAccessFluentMappingBase
    {
        public RealEstateFluentMapping(IDatabaseMappingContext context)
            : base(context)
        {
        }

        public override IList<MappingConfiguration> GetMapping()
        {
            var mappings = new List<MappingConfiguration>();
            MapItem(mappings);
            MapUrlData(mappings);
            return mappings;
        }

        private void MapItem(IList<MappingConfiguration> mappings)
        {
            var itemMapping = new MappingConfiguration<RealEstateItem>();
            itemMapping.HasProperty(p => p.Id).IsIdentity();
            itemMapping.MapType(p => new { }).ToTable("sfex_realestate_item");

            itemMapping.HasProperty(p => p.ItemNumber);
            itemMapping.HasProperty(p => p.Address);
            itemMapping.HasProperty(p => p.PostalCode);
            itemMapping.HasProperty(p => p.Housing);
            itemMapping.HasProperty(p => p.NumberOfRooms);
            itemMapping.HasProperty(p => p.NumberOfFloors);
            itemMapping.HasProperty(p => p.YearBuilt);
            itemMapping.HasProperty(p => p.Price);
            itemMapping.HasProperty(p => p.Payment);
            itemMapping.HasProperty(p => p.MonthlyRate);
            itemMapping.HasProperty(p => p.Net);
            itemMapping.HasProperty(p => p.PriceSquareMeter);
            itemMapping.HasProperty(p => p.Latitude);
            itemMapping.HasProperty(p => p.Longitude);
            itemMapping.HasProperty(p => p.AgentId);


            itemMapping.HasAssociation<Telerik.Sitefinity.Security.Model.Permission>(p => p.Permissions);
            itemMapping.HasProperty(p => p.InheritsPermissions);
            itemMapping.HasAssociation<Telerik.Sitefinity.Security.Model.PermissionsInheritanceMap>(p => p.PermissionChildren);
            itemMapping.HasProperty(p => p.CanInheritPermissions);
            itemMapping.HasAssociation(p => p.Urls).WithOppositeMember("parent", "Parent").ToColumn("content_id").IsDependent().IsManaged();
            itemMapping.HasAssociation<Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecordMap>(p => p.ApprovalTrackingRecordMap);
            CommonFluentMapping.MapILifecycleDataItemFields<RealEstateItem>(itemMapping, this.Context);
            mappings.Add(itemMapping);
        }

        private void MapUrlData(IList<MappingConfiguration> mappings)
        {
            var urlDataMapping = new MappingConfiguration<RealEstateItemUrlData>();
            urlDataMapping.MapType(p => new { }).Inheritance(InheritanceStrategy.Flat).ToTable("sfex_realestate_item_urls");
            mappings.Add(urlDataMapping);
        }
    }
}
