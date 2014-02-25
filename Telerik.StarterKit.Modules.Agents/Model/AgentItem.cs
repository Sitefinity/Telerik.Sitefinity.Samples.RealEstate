using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.OpenAccess;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Versioning.Serialization.Extensions;

namespace Telerik.StarterKit.Modules.Agents.Model
{
    /// <summary>
    /// Agent item model
    /// </summary>
    [DataContract(Namespace = "http://sitefinity.com/samples/agentcatalogue", Name = "AgentItem")]
    [ManagerType("Telerik.StarterKit.Modules.Agents.Data.AgentsManager, Telerik.StarterKit.Modules.Agents")]
    [Persistent(IdentityField = "contentId")]
    public class AgentItem
        : Content
        , IApprovalWorkflowItem
        , ISecuredObject
        , ILocatable
        , ISitefinityCustomTypeSerialization
        , ILifecycleDataItemGeneric
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentItem" /> class.
        /// </summary>
        public AgentItem()
        {
            // set default values
            this.inheritsPermissions = true;
            this.canInheritPermissions = true;
            this.supportedPermissionSets = new string[] { AgentsConstants.Security.PermissionSetName, SecurityConstants.Sets.Comments.SetName };
        }

        /// <summary>
        /// Static initializer
        /// </summary>
        static AgentItem()
        {
            // set default values
            permissionsetObjectTitleResKeys = new Dictionary<string, string>() 
            {
                { AgentsConstants.Security.PermissionSetName, "AgentActionPermissionsListTitle" }
            };
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Agent email
        /// </summary>
        [DataMember]
        [FieldAlias("email")]
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        /// <summary>
        /// Agent phone number
        /// </summary>
        [DataMember]
        [FieldAlias("phoneNumber")]
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        /// <summary>
        /// Agent address
        /// </summary>
        [DataMember]
        [FieldAlias("address")]
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        /// <summary>
        /// Agent address
        /// </summary>
        [DataMember]
        [FieldAlias("postalCode")]
        public string PostalCode
        {
            get { return this.postalCode; }
            set { this.postalCode = value; }
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
        public virtual IList<AgentItemUrlData> Urls
        {
            get
            {
                if (this.urls == null)
                    this.urls = new ProviderTrackedList<AgentItemUrlData>(this, "Urls");
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

            ((AgentItem)obj).Urls.Clear();
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

        private string email;
        private string phoneNumber;
        private string address;
        private string postalCode;

        #region Sitefinity infrastructure
        [Transient]
        private Lstring content;
        // persistent fields
        private ApprovalTrackingRecordMap approvalTrackingRecordMap;
        [Depend]
        private ProviderTrackedList<AgentItemUrlData> urls;
        private bool canInheritPermissions;
        private static IDictionary<string, string> permissionsetObjectTitleResKeys;
        private ProviderTrackedList<Permission> permissions;
        private bool inheritsPermissions;
        private IList<PermissionsInheritanceMap> permissionChildren;

        // transient fields
        [Transient]
        private Lstring approvalWorkflowState;
        [Transient]
        private string[] supportedPermissionSets = new string[] { AgentsConstants.Security.PermissionSetName, SecurityConstants.Sets.Comments.SetName };
        private TrackedList<string> publishedTranslations;
        private TrackedList<LanguageData> languageData;

        #endregion

        #endregion

        #region ILocatable Members


        public void ClearUrls(bool excludeDefault = false)
        {
            this.urls.ClearUrls(excludeDefault);
        }

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
