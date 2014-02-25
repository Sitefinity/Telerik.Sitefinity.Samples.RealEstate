using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.OpenAccess;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Model;

namespace Telerik.StarterKit.Modules.RealEstate.Model
{
    [Persistent]
    public class RealEstateItemUrlData : UrlData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateItemUrlData" /> class.
        /// </summary>
        public RealEstateItemUrlData()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the parent real estate item
        /// </summary>
        /// <value>The real estate item</value>
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
                this.parent = (RealEstateItem)value;
            }
        }

        private RealEstateItem parent;
    }
}
