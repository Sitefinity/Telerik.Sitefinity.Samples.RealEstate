using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using System.Reflection;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Security.Model;
using Telerik.OpenAccess;

namespace Telerik.StarterKit.Modules.Agents.Data.Implementation
{
    [ContentProviderDecorator(typeof(OpenAccessContentDecorator))]
    public class OpenAccessProvider : AgentsDataProvider, IOpenAccessDataProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="OpenAccessProvider"/> class.
        /// </summary>
        static OpenAccessProvider()
        {
            peristentAssemblies = new Assembly[] { typeof(AgentItem).Assembly };
        }

        #endregion

        #region AgentItem methods

        /// <summary>
        /// Create an agent item with random id
        /// </summary>
        /// <returns>Newly created agent item in transaction</returns>
        public override AgentItem CreateAgent()
        {
            return this.CreateAgent(Guid.NewGuid());
        }

        /// <summary>
        /// Create an agent item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created agent item in transaction</returns>
        public override AgentItem CreateAgent(Guid id)
        {
            var agent = new AgentItem();
            agent.Id = id;
            agent.ApplicationName = this.ApplicationName;
            agent.Owner = SecurityManager.GetCurrentUserId();
            var dateValue = DateTime.UtcNow;
            agent.DateCreated = dateValue;
            agent.PublicationDate = dateValue;
            ((IDataItem)agent).Provider = this;

            // agent permissions inherit form the security root
            var securityRoot = this.GetSecurityRoot();
            if (securityRoot != null)
            {
                this.providerDecorator.CreatePermissionInheritanceAssociation(securityRoot, agent);
            }
            else
            {
                var msg = Res.Get<SecurityResources>().NoSecurityRoot;
                msg = string.Format(msg, typeof(AgentItem).AssemblyQualifiedName);
                throw new InvalidOperationException(msg);
            }

            // items with empty guid are used in the UI to get a "blank" data item
            // -> i.e. to fill a data item with default values
            // if this is the case, we leave the item out of the transaction
            if (id != Guid.Empty)
            {
                this.GetContext().Add(agent);
            }
            return agent;
        }

        /// <summary>
        /// Get an agent by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Agent item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>
        public override AgentItem GetAgent(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be Empty Guid");

            // Always use this method. Do NOT change it to query. Catch the exception if the Id can be wrong.
            var agent = this.GetContext().GetItemById<AgentItem>(id.ToString());
            ((IDataItem)agent).Provider = this;
            return agent;
        }

        /// <summary>
        /// Get a query of all agents in this provider
        /// </summary>
        /// <returns>Query of all agents</returns>
        public override IQueryable<AgentItem> GetAgents()
        {
            var appName = this.ApplicationName;

            var query =
                SitefinityQuery
                .Get<AgentItem>(this, MethodBase.GetCurrentMethod())
                .Where(b => b.ApplicationName == appName);

            return query;
        }

        /// <summary>
        /// Delete an agent
        /// </summary>
        /// <param name="product">Agent to delete</param>
        public override void DeleteAgent(AgentItem agent)
        {
            var scope = this.GetContext();
            //remove the item from the parent list of inheritors
            //var securityRoot = this.GetSecurityRoot();
            //if (securityRoot != null)
            //{
            //    List<PermissionsInheritanceMap> parentInheritors = securityRoot.PermissionChildren.Where(c => c.ChildObjectId == agent.Id).ToList();
            //    for (int inheritor = 0; inheritor < parentInheritors.Count(); inheritor++)
            //    {
            //        securityRoot.PermissionChildren.Remove(parentInheritors[inheritor]);
            //    }
            //}
            //remove the relevant permissions
            this.providerDecorator.DeletePermissions(agent);
            this.ClearLifecycle(agent, this.GetAgents());
            if (scope != null)
            {
                scope.Remove(agent);
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
            return new AgentsFluentMetadataSource(context);
        }

        #endregion
    }
}
