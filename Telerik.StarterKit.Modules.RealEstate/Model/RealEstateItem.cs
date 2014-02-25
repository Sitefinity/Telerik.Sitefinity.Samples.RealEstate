using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.OpenAccess;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Versioning.Serialization.Extensions;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.StarterKit.Modules.RealEstate.Model
{
    /// <summary>
    /// Real Estate item model
    /// </summary>
    [DataContract(Namespace = "http://sitefinity.com/samples/realestatecatalogue", Name = "RealEstateItem")]
    [ManagerType("Telerik.StarterKit.Modules.RealEstate.Data.RealEstateManager, Telerik.StarterKit.Modules.RealEstate")]
    [Persistent(IdentityField = "contentId")]
    public class RealEstateItem
        : Content
        , IApprovalWorkflowItem
        , ISecuredObject
        , ILocatable
        , ISitefinityCustomTypeSerialization
        , ILifecycleDataItemGeneric
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateItem" /> class.
        /// </summary>
        public RealEstateItem()
        {
            // set default values
            this.inheritsPermissions = true;
            this.canInheritPermissions = true;
            this.supportedPermissionSets = new string[] { RealEstateConstants.Security.PermissionSetName, SecurityConstants.Sets.Comments.SetName };
        }

        /// <summary>
        /// Static initializer
        /// </summary>
        static RealEstateItem()
        {
            // set default values
            permissionsetObjectTitleResKeys = new Dictionary<string, string>() 
            {
                { RealEstateConstants.Security.PermissionSetName, "ItemActionPermissionsListTitle" }
            };
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Item number
        /// </summary>
        [DataMember]
        [FieldAlias("itemNumber")]
        public string ItemNumber
        {
            get { return this.itemNumber; }
            set { this.itemNumber = value; }
        }

        /// <summary>
        /// Item address
        /// </summary>
        [DataMember]
        [FieldAlias("address")]
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        /// <summary>
        /// Item address
        /// </summary>
        [DataMember]
        [FieldAlias("postalCode")]
        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
        }

        /// <summary>
        /// Housing
        /// </summary>
        [DataMember]
        [FieldAlias("housing")]
        public string Housing
        {
            get { return this.housing; }
            set { this.housing = value; }
        }

        /// <summary>
        /// Number of Rooms
        /// </summary>
        [DataMember]
        [FieldAlias("numberOfRooms")]
        public string NumberOfRooms
        {
            get { return this.numberOfRooms; }
            set { this.numberOfRooms = value; }
        }

        /// <summary>
        /// Number of Floors
        /// </summary>
        [DataMember]
        [FieldAlias("numberOfFloors")]
        public string NumberOfFloors
        {
            get { return this.numberOfFloors; }
            set { this.numberOfFloors = value; }
        }

        /// <summary>
        /// The year that the property was built
        /// </summary>
        [DataMember]
        [FieldAlias("yearBuilt")]
        public string YearBuilt
        {
            get { return this.yearBuilt; }
            set { this.yearBuilt = value; }
        }

        /// <summary>
        /// Price
        /// </summary>
        [DataMember]
        [FieldAlias("price")]
        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        /// <summary>
        /// Payment
        /// </summary>
        [DataMember]
        [FieldAlias("payment")]
        public double Payment
        {
            get { return this.payment; }
            set { this.payment = value; }
        }

        /// <summary>
        /// Monthly Rate
        /// </summary>
        [DataMember]
        [FieldAlias("monthlyRate")]
        public double MonthlyRate
        {
            get { return this.monthlyRate; }
            set { this.monthlyRate = value; }
        }

        /// <summary>
        /// Net
        /// </summary>
        [DataMember]
        [FieldAlias("net")]
        public double Net
        {
            get { return this.net; }
            set { this.net = value; }
        }

        /// <summary>
        /// Price / m2
        /// </summary>
        [DataMember]
        [FieldAlias("priceSquareMeter")]
        public double PriceSquareMeter
        {
            get { return this.priceSquareMeter; }
            set { this.priceSquareMeter = value; }
        }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember]
        [FieldAlias("latitude")]
        public float Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember]
        [FieldAlias("longitude")]
        public float Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }

        /// <summary>
        /// Agent ID
        /// </summary>
        [DataMember]
        [FieldAlias("agentId")]
        public Guid AgentId
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }

        #endregion

        #region Sitefinity infrastucture

        #region IApprovalWorkflowItem members

        /// <summary>
        /// Gets or sets the approval tracking records
        /// </summary>
        [FieldAlias("approvalTrackingRecordMap")]
        [NonSerializableProperty]
        public ApprovalTrackingRecordMap ApprovalTrackingRecordMap
        {
            get
            {
                return this.approvalTrackingRecordMap;
            }
            set
            {
                this.approvalTrackingRecordMap = value;
            }

        }

        /// <summary>
        /// Gets or sets the current state of the item in the approval workflow.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Note that item can be in different states depending on the culture.
        /// </remarks>
        [DataMember]
        [Database(DBType = "VARCHAR", DBSqlType = "NVARCHAR")]
        public virtual Lstring ApprovalWorkflowState
        {
            get
            {
                if (this.approvalWorkflowState == null)
                    this.approvalWorkflowState = this.GetString("ApprovalWorkflowState");
                return this.approvalWorkflowState;
            }
            set
            {
                this.approvalWorkflowState = value;
                this.SetString("ApprovalWorkflowState", this.approvalWorkflowState);
            }
        }

        #endregion

        #region ILocatable members

        /// <summary>
        /// Gets or sets a value indicating whether to auto generate an unique URL.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if to auto generate an unique URL otherwise, <c>false</c>.
        /// </value>
        [NonSerializableProperty]
        public bool AutoGenerateUniqueUrl
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a collection of URL data for this item.
        /// </summary>
        /// <value>The collection of URL data.</value>
        [FieldAlias("urls")]
        [NonSerializableProperty]
        public virtual IList<RealEstateItemUrlData> Urls
        {
            get
            {
                if (this.urls == null)
                    this.urls = new ProviderTrackedList<RealEstateItemUrlData>(this, "Urls");
                this.urls.SetCollectionParent(this);
                return this.urls;
            }
        }

        /// <summary>
        /// Gets a collection of URL data for this item.
        /// </summary>
        /// <value>The collection of URL data.</value>
        [NonSerializableProperty]
        IEnumerable<UrlData> ILocatable.Urls
        {
            get
            {
                return this.Urls.Cast<UrlData>();
            }
        }

        #endregion

        #region ISitefinityCustomTypeSerialization members

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializedObject">The serialized object.</param>
        public void Serialize(object obj, out Dictionary<string, object> serializedObject)
        {
            serializedObject = obj.GetSerializationPropertyValueCollection();
            if (Urls.Count > 0)
            {
                var urls = obj.GetListSerializationItemsItems("Urls");
                serializedObject.Add("URLS", urls);
            }
        }

        /// <summary>
        /// Deserializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="serializedObject">The serialized object.</param>
        public void Deserialize(ref object obj, Dictionary<string, object> serializedObject)
        {
            VersioningUtilities.SetSerializationPropertyValueCollection(ref obj, serializedObject);

            ((RealEstateItem)obj).Urls.Clear();
            if (serializedObject.ContainsKey("URLS"))
            {
                VersioningUtilities.SetListDeserializedItems(ref obj, "Urls", "Parent", this, serializedObject["URLS"]);
            }
        }

        #endregion

        #region ISecuredObject

        /// <summary>
        /// Gets or sets a value indicating whether this instance can inherit permissions.
        /// </summary>
        /// <value></value>
        [FieldAlias("canInheritPermissions")]
        [NonSerializableProperty]
        public bool CanInheritPermissions
        {
            get
            {
                return this.canInheritPermissions;
            }
            set
            {
                this.canInheritPermissions = value;
            }
        }

        /// <summary>
        /// Indicates if this profile inherits permissions
        /// </summary>
        [FieldAlias("inheritsPermissions")]
        [NonSerializableProperty]
        public bool InheritsPermissions
        {
            get
            {
                return this.inheritsPermissions;
            }
            set
            {
                this.inheritsPermissions = value;
            }
        }

        /// <summary>
        /// Gets or sets a list of PermissionsInheritanceMap objects,
        /// each containing a mapping to a child secured object which inherits permissions directly from this object.
        /// </summary>
        /// <value></value>
        [FieldAlias("permissionChildren")]
        [NonSerializableProperty]
        public IList<PermissionsInheritanceMap> PermissionChildren
        {
            get
            {
                if (this.permissionChildren == null)
                    this.permissionChildren = new TrackedList<PermissionsInheritanceMap>();
                return this.permissionChildren;
            }
            set
            {
                this.permissionChildren = value;
            }
        }

        /// <summary>
        /// A set of permissions for the profile, as a secured object (as IList)
        /// </summary>
        [FieldAlias("permissions")]
        [NonSerializableProperty]
        public IList<Permission> Permissions
        {
            get
            {
                if (this.permissions == null)
                    this.permissions = new ProviderTrackedList<Permission>(this, "Permissions");
                this.permissions.SetCollectionParent(this);
                return this.permissions;
            }
        }

        /// <summary>
        /// Gets a dictionary:
        /// Key is a name of a permission set supported by this provider,
        /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
        /// </summary>
        /// <value>The permission set object titles.</value>
        [NonSerializableProperty]
        public virtual IDictionary<string, string> PermissionsetObjectTitleResKeys
        {
            get
            {
                return permissionsetObjectTitleResKeys;
            }
            set
            {
                permissionsetObjectTitleResKeys = value;
            }
        }

        /// <summary>
        /// Gets the permission sets relevant to this specific secured object.
        /// </summary>
        /// <value>The supported permission sets.</value>
        [NonSerializableProperty]
        public string[] SupportedPermissionSets
        {
            get
            {
                return supportedPermissionSets;
            }
            set
            {
                supportedPermissionSets = value;
            }
        }

        #endregion

        #endregion

        #region Fields

        private string itemNumber;
        private string address;
        private string postalCode;
        private string housing;
        private string numberOfRooms;
        private string numberOfFloors;
        private string yearBuilt;
        private double price;
        private double payment;
        private double monthlyRate;
        private double net;
        private double priceSquareMeter;
        private float latitude;
        private float longitude;
        private Guid agentId;

        #region Sitefinity infrastructure
        [Transient]
        private Lstring content;
        // persistent fields
        private ApprovalTrackingRecordMap approvalTrackingRecordMap;
        [Depend]
        private ProviderTrackedList<RealEstateItemUrlData> urls;
        private bool canInheritPermissions;
        private static IDictionary<string, string> permissionsetObjectTitleResKeys;
        private ProviderTrackedList<Permission> permissions;
        private bool inheritsPermissions;
        private IList<PermissionsInheritanceMap> permissionChildren;

        // transient fields
        [Transient]
        private Lstring approvalWorkflowState;
        [Transient]
        private string[] supportedPermissionSets = new string[] { RealEstateConstants.Security.PermissionSetName, SecurityConstants.Sets.Comments.SetName };
        private TrackedList<string> publishedTranslations;
        private TrackedList<LanguageData> languageData;
        #endregion

        #endregion

        #region ILocatable Members


        /// <summary>
        /// Clears the Urls collection for this item.
        /// </summary>
        /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
        public void ClearUrls(bool excludeDefault = false)
        {
            this.urls.ClearUrls(excludeDefault);
        }

        /// <summary>
        /// Removes all urls that satisfy the condition that is checked in the predicate function.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public void RemoveUrls(Func<UrlData, bool> predicate)
        {
            this.urls.RemoveUrls(predicate);
        }

        #endregion

        #region ILifecycleDataItemGeneric Members

        /// <summary>
        /// Gets a list of available translations (set for the Master and Live items).
        /// </summary>
        [Searchable(false)]
        [NonSerializableProperty]
        public virtual IList<string> PublishedTranslations
        {
            get
            {
                if (this.publishedTranslations == null)
                    this.publishedTranslations = new TrackedList<string>();
                return this.publishedTranslations;
            }
        }

        /// <summary>
        /// Collection of culture specific data - publication date, scheduled date
        /// </summary>
        [Searchable(false)]
        [NonSerializableProperty]
        public IList<LanguageData> LanguageData
        {
            get
            {
                if (this.languageData == null)
                    this.languageData = new TrackedList<LanguageData>();
                return this.languageData;
            }
        }

        #endregion
    }
}
