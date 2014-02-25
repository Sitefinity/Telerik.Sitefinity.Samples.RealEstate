using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace Telerik.StarterKit.Modules.Agents.Data.Implementation
{
    public class AgentsFluentMapping : OpenAccessFluentMappingBase
    {
        public AgentsFluentMapping(IDatabaseMappingContext context)
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
            var itemMapping = new MappingConfiguration<AgentItem>();
            itemMapping.HasProperty(p => p.Id).IsIdentity();
            itemMapping.MapType(p => new { }).ToTable("sfex_agent_item");

            itemMapping.HasProperty(p => p.Email);
            itemMapping.HasProperty(p => p.PhoneNumber);
            itemMapping.HasProperty(p => p.Address);
            itemMapping.HasProperty(p => p.PostalCode);

            itemMapping.HasAssociation<Telerik.Sitefinity.Security.Model.Permission>(p => p.Permissions);
            itemMapping.HasProperty(p => p.InheritsPermissions);
            itemMapping.HasAssociation<Telerik.Sitefinity.Security.Model.PermissionsInheritanceMap>(p => p.PermissionChildren);
            itemMapping.HasProperty(p => p.CanInheritPermissions);
            itemMapping.HasAssociation(p => p.Urls).WithOppositeMember("parent", "Parent").ToColumn("content_id").IsDependent().IsManaged();
            itemMapping.HasAssociation<Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecordMap>(p => p.ApprovalTrackingRecordMap);
            CommonFluentMapping.MapILifecycleDataItemFields<AgentItem>(itemMapping, this.Context);

            mappings.Add(itemMapping);
        }

        private void MapUrlData(IList<MappingConfiguration> mappings)
        {
            var urlDataMapping = new MappingConfiguration<AgentItemUrlData>();
            urlDataMapping.MapType(p => new { }).Inheritance(InheritanceStrategy.Flat).ToTable("sfex_agent_item_urls");
            mappings.Add(urlDataMapping);
        }
    }
}
