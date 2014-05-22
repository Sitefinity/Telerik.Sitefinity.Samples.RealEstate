using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.Sitefinity.Security;
using System.Collections;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Data.Linq;
using Telerik.StarterKit.Modules.Agents.Data;

namespace Telerik.StarterKit.Modules.RealEstate.Data
{
    /// <summary>
    /// Base provider class for the Real Estate module
    /// </summary>
    public abstract class RealEstateDataProvider : ContentDataProviderBase, ILanguageDataProvider, IOpenAccessDataProvider
    {
        #region RealEstateItem methods

        /// <summary>
        /// Create an item with random id
        /// </summary>
        /// <returns>Newly created item in transaction</returns>
        [MethodPermission(RealEstateConstants.Security.PermissionSetName, RealEstateConstants.Security.Create)]
        public abstract RealEstateItem CreateItem();
        /// <summary>
        /// Create an item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created item in transaction</returns>
        [MethodPermission(RealEstateConstants.Security.PermissionSetName, RealEstateConstants.Security.Create)]
        public abstract RealEstateItem CreateItem(Guid id);
        /// <summary>
        /// Get an item by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>
        [ValuePermission(RealEstateConstants.Security.PermissionSetName, RealEstateConstants.Security.View)]
        public abstract RealEstateItem GetItem(Guid id);
        /// <summary>
        /// Get a query of all items in this provider
        /// </summary>
        /// <returns>Query of all items</returns>
        [EnumeratorPermission(RealEstateConstants.Security.PermissionSetName, RealEstateConstants.Security.View)]
        public abstract IQueryable<RealEstateItem> GetItems();
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="item">Item to delete</param>
        [ParameterPermission("item", RealEstateConstants.Security.PermissionSetName, RealEstateConstants.Security.Delete)]
        public abstract void DeleteItem(RealEstateItem item);

        #endregion

        #region Item Methods

        /// <summary>
        /// Creates new data item.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="id">The pageId.</param>
        /// <returns></returns>
        public override object CreateItem(Type itemType, Guid id)
        {
            if (itemType == null)
                throw new ArgumentNullException("itemType");

            if (itemType == typeof(RealEstateItem))
            {
                return this.CreateItem(id);
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Gets the data item with the specified ID.
        /// An exception should be thrown if an item with the specified ID does not exist.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="id">The ID of the item to return.</param>
        /// <returns></returns>
        public override object GetItem(Type itemType, Guid id)
        {
            if (itemType == null)
                throw new ArgumentNullException("itemType");

            if (itemType == typeof(RealEstateItem))
                return this.GetItem(id);

            if (itemType == typeof(Comment))
            {
                return this.GetComment(id);
            }

            return base.GetItem(itemType, id);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="orderExpression">The order expression.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression"/></param>
        /// <returns></returns>
        public override IEnumerable GetItems(Type itemType, string filterExpression, string orderExpression, int skip, int take, ref int? totalCount)
        {
            if (itemType == null)
                throw new ArgumentNullException("itemType");

            if (itemType == typeof(RealEstateItem))
            {
                return SetExpressions(this.GetItems(), filterExpression, orderExpression, skip, take, ref totalCount);
            }

            if (itemType == typeof(Comment))
            {
                return SetExpressions(this.GetComments(), filterExpression, orderExpression, skip, take, ref totalCount);
            }

            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Marks the provided persistent item for deletion.
        /// The item is deleted form the storage when the transaction is committed.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        public override void DeleteItem(object item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var itemType = item.GetType();
            this.providerDecorator.DeletePermissions(item);

            if (itemType == typeof(RealEstateItem))
            {
                this.DeleteItem((RealEstateItem)item);
                return;
            }

            if (itemType == typeof(Comment))
            {
                this.Delete((Comment)item);
                return;
            }

            throw GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets a unique key for each data provider base.
        /// </summary>
        /// <value></value>
        public override string RootKey
        {
            get { return "RealEstateDataProvider"; }
        }

        /// <summary>
        /// Gets the items by taxon.
        /// </summary>
        /// <param name="taxonId">The taxon id.</param>
        /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="orderExpression">The order expression.</param>
        /// <param name="skip">Items to skip.</param>
        /// <param name="take">Items to take.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public override IEnumerable GetItemsByTaxon(Guid taxonId, bool isSingleTaxon, string propertyName, Type itemType, string filterExpression, string orderExpression, int skip, int take, ref int? totalCount)
        {
            if (itemType == typeof(RealEstateItem))
            {
                this.CurrentTaxonomyProperty = propertyName;
                int? internalTotCount = null;
                IQueryable<RealEstateItem> query =
                    (IQueryable<RealEstateItem>)this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref internalTotCount);
                if (isSingleTaxon)
                {
                    var query0 = from i in query
                                 where i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId
                                 select i;
                    query = query0;
                }
                else
                {
                    var query1 = from i in query
                                 where (i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty)).Any(t => t == taxonId)
                                 select i;
                    query = query1;
                }

                if (totalCount.HasValue)
                {
                    totalCount = query.Count();
                }

                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);
                return query;
            }
            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        public override object GetItemOrDefault(Type itemType, Guid id)
        {
            if (itemType == typeof(Comment))
            {
                return this.GetComments().Where(c => c.Id == id).FirstOrDefault();
            }

            if (itemType == typeof(RealEstateItem))
                return this.GetItems().Where(n => n.Id == id).FirstOrDefault();

            return base.GetItemOrDefault(itemType, id);
        }

        /// <summary>
        /// Override this method in order to return the type of the Parent object of the specified content type.
        /// If the type has no parent type, return null.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public override Type GetParentTypeFor(Type contentType)
        {
            return null;
        }

        /// <summary>
        /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData"/> implementation for the specified content type.
        /// </summary>
        /// <param name="itemType">Type of the content item.</param>
        /// <returns></returns>
        public override Type GetUrlTypeFor(Type itemType)
        {
            if (itemType == typeof(RealEstateItem))
                return typeof(RealEstateItemUrlData);
            throw GetInvalidItemTypeException(itemType, this.GetKnownTypes());
        }

        /// <summary>
        /// Get a list of types served by this manager
        /// </summary>
        /// <returns></returns>
        public override Type[] GetKnownTypes()
        {
            if (knownTypes == null)
            {
                knownTypes = new Type[]
                {
                    typeof(RealEstateItem)
                };
            }
            return knownTypes;
        }

        /// <summary>
        /// Gets the permission sets relevant to this specific secured object.
        /// To be overridden by relevant providers (which involve security roots)
        /// </summary>
        /// <value>The supported permission sets.</value>
        public override string[] SupportedPermissionSets
        {
            get
            {
                return this.supportedPermissionSets;
            }
            set
            {
                this.supportedPermissionSets = value;
            }
        }

        public override string GetUrlFormat(ILocatable item)
        {
            return "/[UrlName]";//default: "/[PublicationDate, {0:yyyy'/'MM'/'dd}]/[UrlName]
        }

        #endregion

        #region Fields

        private static Type[] knownTypes;
        private string[] supportedPermissionSets = new string[] { RealEstateConstants.Security.PermissionSetName, SecurityConstants.Sets.Comments.SetName };

        #endregion

        #region ILanguageDataProvider methods

        /// <summary>
        /// Creates a language data item
        /// </summary>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData CreateLanguageData()
        {
            return this.CreateLanguageData(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a language data item
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData CreateLanguageData(Guid id)
        {
            var languageData = new LanguageData(this.ApplicationName, id);
            ((IDataItem)languageData).Provider = this;

            if (id != Guid.Empty)
            {
                this.GetContext().Add(languageData);
            }
            return languageData;
        }

        /// <summary>
        /// Gets language data item by its id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Telerik.Sitefinity.Lifecycle.LanguageData GetLanguageData(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Argument 'id' cannot be empty GUID.");

            var languageData = this.GetContext().GetItemById<LanguageData>(id.ToString());
            ((IDataItem)languageData).Provider = this;
            return languageData;
        }

        /// <summary>
        /// Gets a query of all language data items
        /// </summary>
        /// <returns></returns>
        public IQueryable<Telerik.Sitefinity.Lifecycle.LanguageData> GetLanguageData()
        {
            var appName = this.ApplicationName;
            return SitefinityQuery.Get<LanguageData>(this).Where(c => c.ApplicationName == appName);
        }

        #endregion

        public Sitefinity.Data.OpenAccessProviderContext Context
        {
            get;
            set;
        }

        public OpenAccess.Metadata.MetadataSource GetMetaDataSource(IDatabaseMappingContext context)
        {
            return new AgentsFluentMetadataSource(context);
        }
    }
}
