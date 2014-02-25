using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.OpenAccess;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Model;

namespace Telerik.StarterKit.Modules.Agents.Model
{
    [Persistent]
    public class AgentItemUrlData : UrlData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentItemUrlData" /> class.
        /// </summary>
        public AgentItemUrlData()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the parent product item
        /// </summary>
        /// <value>The product item</value>
        [FieldAlias("parent")]
        [NonSerializableProperty]
        public override IDataItem Parent
        {
            get
            {
                if (this.parent != null)
                    ((IDataItem)this.parent).Provider = ((IDataItem)this).Provider;
                return this.parent;
            }
            set
            {
                this.parent = (AgentItem)value;
            }
        }

        private AgentItem parent;
    }
}
