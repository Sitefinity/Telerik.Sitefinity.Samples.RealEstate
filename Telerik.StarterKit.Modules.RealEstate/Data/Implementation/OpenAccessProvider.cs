using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using System.Reflection;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Security.Model;
using Telerik.OpenAccess;

namespace Telerik.StarterKit.Modules.RealEstate.Data.Implementation
{
    [ContentProviderDecorator(typeof(OpenAccessContentDecorator))]
    public class OpenAccessProvider : RealEstateDataProvider, IOpenAccessDataProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="OpenAccessProvider"/> class.
        /// </summary>
        static OpenAccessProvider()
        {
            peristentAssemblies = new Assembly[] { typeof(RealEstateItem).Assembly };
        }

        #endregion

        #region RealEstateItem methods

        /// <summary>
        /// Create an item with random id
        /// </summary>
        /// <returns>Newly created item in transaction</returns>
        public override RealEstateItem CreateItem()
        {
            return this.CreateItem(Guid.NewGuid());
        }

        /// <summary>
        /// Create an item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created item in transaction</returns>
        public override RealEstateItem CreateItem(Guid id)
        {
            var item = new RealEstateItem();
            item.Id = id;
            item.ApplicationName = this.ApplicationName;
            item.Owner = SecurityManager.GetCurrentUserId();
            var dateValue = DateTime.UtcNow;
            item.DateCreated = dateValue;
            item.PublicationDate = dateValue;
            ((IDataItem)item).Provider = this;

            // item permissions inherit form the security root
            var securityRoot = this.GetSecurityRoot();
            if (securityRoot != null)
            {
                this.providerDecorator.CreatePermissionInheritanceAssociation(securityRoot, item);
            }
            else
            {
                var msg = Res.Get<SecurityResources>().NoSecurityRoot;
                msg = string.Format(msg, typeof(RealEstateItem).AssemblyQualifiedName);
                throw new InvalidOperationException(msg);
            }

            // items with empty guid are used in the UI to get a "blank" data item
            // -> i.e. to fill a data item with default values
            // if this is the case, we leave the item out of the transaction
            if (id != Guid.Empty)
            {
                this.GetContext().Add(item);
            }
            return item;
        }

        /// <summary>
        /// Get an item by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>
        public override RealEstateItem GetItem(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be Empty Guid");

            // Always use this method. Do NOT change it to query. Catch the exception if the Id can be wrong.
            var item = this.GetContext().GetItemById<RealEstateItem>(id.ToString());
            ((IDataItem)item).Provider = this;
            return item;
        }

        /// <summary>
        /// Get a query of all items in this provider
        /// </summary>
        /// <returns>Query of all items</returns>
        public override IQueryable<RealEstateItem> GetItems()
        {
            var appName = this.ApplicationName;

            var query =
                SitefinityQuery
                .Get<RealEstateItem>(this, MethodBase.GetCurrentMethod())
                .Where(b => b.ApplicationName == appName);

            return query;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="product">Item to delete</param>
        public override void DeleteItem(RealEstateItem item)
        {
            var scope = this.GetContext();
            //remove the item from the parent list of inheritors
            //var securityRoot = this.GetSecurityRoot();
            //if (securityRoot != null)
            //{
            //    List<PermissionsInheritanceMap> parentInheritors = securityRoot.PermissionChildren.Where(c => c.ChildObjectId == item.Id).ToList();
            //    for (int inheritor = 0; inheritor < parentInheritors.Count(); inheritor++)
            //    {
            //        securityRoot.PermissionChildren.Remove(parentInheritors[inheritor]);
            //    }
            //}
            //remove the relevant permissions
            this.providerDecorator.DeletePermissions(item);
            this.ClearLifecycle(item, this.GetItems());
            if (scope != null)
            {
                scope.Remove(item);
            }
        }

        #endregion

        #region IOpenAccessDataProvider

        /// <summary>
        /// Gets or sets the database instance for this provider.
        /// </summary>
        /// <value>The database.</value>
        public Database Database
        {
            get;
            set;
        }

        /// <summary>
        /// The list of all assemblies with persistent classes inside.
        /// Only this list of assemblies will be used, it must be complete!
        /// </summary>
        /// <returns></returns>
        /// <value>The persistent assemblies.</value>
        public Assembly[] GetPersistentAssemblies()
        {
            return peristentAssemblies;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use implicit transactions.
        /// The recommended value for this property is true.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use implicit transactions]; otherwise, <c>false</c>.
        /// </value>
        public bool UseImplicitTransactions
        {
            get { return true; }
        }

        public OpenAccessProviderContext Context
        {
            get;
            set;
        }

        #endregion

        #region Fields

        private static Assembly[] peristentAssemblies;

        #endregion


        public TransactionMode TransactionConcurrency
        {
            get { return TransactionMode.OPTIMISTIC_NO_LOST_UPDATES; }
        }

        #region IOpenAccessMetadataProvider Members

        public OpenAccess.Metadata.MetadataSource GetMetaDataSource(IDatabaseMappingContext context)
        {
            return new RealEstateFluentMetadataSource(context);
        }

        #endregion
    }
}
