using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Configuration;
using Telerik.StarterKit.Modules.Agents.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.StarterKit.Modules.Agents.Data
{
    /// <summary>
    /// Agents data manager
    /// </summary>
    public class AgentsManager : ContentManagerBase<AgentsDataProvider>, IContentLifecycleManager<AgentItem>, ILifecycleManager
    {
        #region Constructors - required

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentsManager"/> class.
        /// </summary>
        public AgentsManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentsManager"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public AgentsManager(string providerName)
            : base(providerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentsManager"/> class.
        /// </summary>
        /// <param name="providerName">
        /// The name of the provider. If empty string or null the default provider is set
        /// </param>
        /// <param name="transactionName">
        /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
        /// </param>
        public AgentsManager(string providerName, string transactionName)
            : base(providerName, transactionName)
        {
        }

        #endregion

        #region Static methods - required

        /// <summary>
        /// Get an instance of the agents manager using the default provider
        /// </summary>
        /// <returns>Instance of agents manager</returns>
        public static AgentsManager GetManager()
        {
            return ManagerBase<AgentsDataProvider>.GetManager<AgentsManager>();
        }

        /// <summary>
        /// Get an instance of the agents manager by explicitly specifying the required provider to use
        /// </summary>
        /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
        /// <param name="transactionName">Name of the transaction.</param>
        /// <returns>Instance of the agents manager</returns>
        public static AgentsManager GetManager(string providerName, string transactionName)
        {
            return ManagerBase<AgentsDataProvider>.GetManager<AgentsManager>(providerName, transactionName);
        }

        /// <summary>
        /// Get an instance of the agents manager by explicitly specifying the required provider to use
        /// </summary>
        /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
        /// <returns>Instance of the agents manager</returns>
        public static AgentsManager GetManager(string providerName)
        {
            return ManagerBase<AgentsDataProvider>.GetManager<AgentsManager>(providerName);
        }

        #endregion

        #region Agent Item methods

        /// <summary>
        /// Create an agent item with random id
        /// </summary>
        /// <returns>Newly created agent item in transaction</returns>       
        public AgentItem CreateAgent()
        {
            return this.Provider.CreateAgent();
        }

        /// <summary>
        /// Create an agent item with specific primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Newly created agent item in transaction</returns>        
        public AgentItem CreateAgent(Guid id)
        {
            return this.Provider.CreateAgent(id);
        }

        /// <summary>
        /// Get an agent by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Agent item in transaction</returns>
        /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When there is no item with the given primary key</exception>        
        public AgentItem GetAgent(Guid id)
        {
            return this.Provider.GetAgent(id);
        }

        /// <summary>
        /// Get a query of all agents in this provider
        /// </summary>
        /// <returns>Query of all agents</returns>        
        public IQueryable<AgentItem> GetAgents()
        {
            return this.Provider.GetAgents();
        }

        /// <summary>
        /// Delete an agent
        /// </summary>
        /// <param name="agent">Agent to delete</param>       
        public void DeleteAgent(AgentItem agent)
        {
            this.Provider.DeleteAgent(agent);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the default provider delegate.
        /// </summary>
        /// <value>The default provider delegate.</value>
        protected override GetDefaultProvider DefaultProviderDelegate
        {
            get { return () => Config.Get<AgentsConfig>().DefaultProvider; }
        }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public override string ModuleName
        {
            get { return AgentsModule.ModuleName; }
        }

        /// <summary>
        /// Gets the providers settings.
        /// </summary>
        /// <value>The providers settings.</value>
        protected override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
        {
            get { return Config.Get<AgentsConfig>().Providers; }
        }

        /// <summary>
        /// Get items by type
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <returns>Queryable</returns>
        public override IQueryable<TItem> GetItems<TItem>()
        {
            if (typeof(AgentItem).IsAssignableFrom(typeof(TItem)))
                return this.GetAgents() as IQueryable<TItem>;
            if (typeof(TItem) == typeof(UrlData) || typeof(TItem) == typeof(AgentItemUrlData))
                return this.GetUrls<AgentItemUrlData>() as IQueryable<TItem>;
            throw new NotSupportedException();
        }

        #endregion

        #region Content lifecycle

        #region AgentItem

        /// <summary>
        /// Checks in the content in the temp state. Content becomes master after the check in.
        /// </summary>
        /// <param name="item">Content in temp state that is to be checked in.</param>
        /// <returns>An item in master state.</returns>
        public virtual AgentItem CheckIn(AgentItem item)
        {
            return (AgentItem)this.Lifecycle.CheckIn(item);
        }

        /// <summary>
        /// Checks out the content in master state. Content becomes temp after the check out.
        /// </summary>
        /// <param name="item">Content in master state that is to be checked out.</param>
        /// <returns>A content that was checked out in temp state.</returns>
        public virtual AgentItem CheckOut(AgentItem item)
        {
            return (AgentItem)this.Lifecycle.CheckOut(item);
        }

        /// <summary>
        /// Copy one agent item to another for the uses of content lifecycle management
        /// </summary>
        /// <param name="source">Agent item to copy from</param>
        /// <param name="destination">Agent item to copy to</param>
        public void Copy(AgentItem source, AgentItem destination)
        {
            //this.Provider.CopyContent(source, destination);
            destination.Urls.ClearDestinationUrls(source.Urls, this.Delete);
            source.Urls.CopyTo(destination.Urls, destination);
            ////Not going through the Content property as it has logic that strips all localizable values of the Lstring.
            //destination.GetString("Content").CopyFrom(source.GetString("Content"));
            //// that's how we copy lstring-s
            //destination.GetString("WhatIsInTheBox").CopyFrom(source.GetString("WhatIsInTheBox"));
            destination.Email = source.Email;
            destination.PhoneNumber = source.PhoneNumber;
            destination.Address = source.Address;
            destination.PostalCode = source.PostalCode;
        }

        /// <summary>
        /// Edits the content in live state. Content becomes master after the edit.
        /// </summary>
        /// <param name="item">Content in live state that is to be edited.</param>
        /// <returns>A content that was edited in master state.</returns>
        public AgentItem Edit(AgentItem item)
        {
            return (AgentItem)this.Lifecycle.Edit(item);
        }

        /// <summary>
        /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
        /// </summary>
        /// <param name="item">Item to get the user ID it is locked by</param>      
        /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
        public Guid GetCheckedOutBy(AgentItem item)
        {
            return this.Lifecycle.GetCheckedOutBy(item);
        }

        /// <summary>
        /// Gets the public (live) version of <paramref name="cnt"/>, if it exists
        /// </summary>
        /// <param name="cnt">Type of the content item</param>        
        /// <returns>Public (live) version of <paramref name="cnt"/>, if it exists</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public AgentItem GetLive(AgentItem cnt)
        {
            return (AgentItem)(this.Lifecycle).GetLive(cnt);
        }

        /// <summary>
        /// Accepts a content item and returns an item in master state
        /// </summary>        
        /// <param name="cnt">Content item whose master to get</param>        
        /// <returns>
        /// If <paramref name="cnt"/> is master itself, returns cnt.
        /// Otherwize, looks up the master associated with <paramref name="cnt"/> and returns it.
        /// When there is no master, an exception will be thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">When no master can be found for <paramref name="cnt"/>.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public AgentItem GetMaster(AgentItem cnt)
        {
            return (AgentItem)(this.Lifecycle).GetMaster(cnt);
        }

        /// <summary>
        /// Get a temp for <paramref name="cnt"/>, if it exists.
        /// </summary>        
        /// <param name="cnt">Content item to get a temp for</param>        
        /// <returns>Temp version of <paramref name="cnt"/>, if it exists.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public AgentItem GetTemp(AgentItem cnt)
        {
            return (AgentItem)(this.Lifecycle).GetTemp(cnt);
        }

        /// <summary>
        /// Returns true or false, depending on whether the <paramref name="item"/> is checked out or not
        /// </summary>
        /// <param name="item">Item to test</param>        
        /// <returns>True if the item is checked out, false otherwize.</returns>
        public bool IsCheckedOut(AgentItem item)
        {
            return this.Lifecycle.IsCheckedOut(item);
        }

        /// <summary>
        /// Checks if <paramref name="item"/> is checked out by user with a specified id
        /// </summary>
        /// <param name="item">Item to test</param>
        /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item"/></param>        
        /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
        public bool IsCheckedOutBy(AgentItem item, Guid userId)
        {
            return this.Lifecycle.IsCheckedOutBy(item, userId);
        }

        /// <summary>
        /// Publishes the content in master state. Content becomes live after the publish.
        /// </summary>
        /// <param name="item">Content in master state that is to be published.</param>
        public AgentItem Publish(AgentItem item)
        {
            return (AgentItem)this.Lifecycle.Publish(item);
        }

        /// <summary>
        /// Schedule a content item - to be published from one date to another
        /// </summary>
        /// <param name="item">Content item in master state</param>
        /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
        /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>   
        public AgentItem Schedule(AgentItem item, DateTime publicationDate, DateTime? expirationDate)
        {
            return this.Provider.Schedule(item, publicationDate, expirationDate, this.Copy, this.GetAgents());
        }

        /// <summary>
        /// Unpublish a content item in live state.
        /// </summary>
        /// <param name="item">Live item to unpublish.</param>
        /// <returns>Master (draft) state.</returns>
        public AgentItem Unpublish(AgentItem item)
        {
            return (AgentItem)this.Lifecycle.Unpublish(item);
        }

        #endregion

        #region Generic

        /// <summary>
        /// Checks in the content in the temp state. Content becomes master after the check in.
        /// </summary>
        /// <param name="item">Content in temp state that is to be checked in.</param>
        /// <returns>An item in master state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content CheckIn(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.CheckIn(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Checks out the content in master state. Content becomes temp after the check out.
        /// </summary>
        /// <param name="item">Content in master state that is to be checked out.</param>
        /// <returns>A content that was checked out in temp state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content CheckOut(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.CheckOut(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Copy one item to another for the uses of content lifecycle management
        /// </summary>
        /// <param name="source">Item to copy from</param>
        /// <param name="destination">Item to copy to</param>
        public void Copy(Telerik.Sitefinity.GenericContent.Model.Content source, Telerik.Sitefinity.GenericContent.Model.Content destination)
        {

            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("destination");
            var agentSource = source as AgentItem;
            var agentDestination = destination as AgentItem;
            if (agentSource == null || agentDestination == null) throw new ArgumentException("Source and destination must be of the same type");
            this.Copy(agentSource, agentDestination);
        }

        /// <summary>
        /// Edits the content in live state. Content becomes master after the edit.
        /// </summary>
        /// <param name="item">Content in live state that is to be edited.</param>
        /// <returns>A content that was edited in master state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content Edit(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.Edit(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
        /// </summary>
        /// <param name="item">Item to get the user ID it is locked by</param>        
        /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
        public Guid GetCheckedOutBy(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.GetCheckedOutBy(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the public (live) version of <paramref name="cnt"/>, if it exists
        /// </summary>
        /// <param name="cnt">Type of the content item</param>        
        /// <returns>Public (live) version of <paramref name="cnt"/>, if it exists</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetLive(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var agent = cnt as AgentItem;
            if (agent != null)
                return this.GetLive(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Accepts a content item and returns an item in master state
        /// </summary>        
        /// <param name="cnt">Content item whose master to get</param>        
        /// <returns>
        /// If <paramref name="cnt"/> is master itself, returns cnt.
        /// Otherwize, looks up the master associated with <paramref name="cnt"/> and returns it.
        /// When there is no master, an exception will be thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">When no master can be found for <paramref name="cnt"/>.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetMaster(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var agent = cnt as AgentItem;
            if (agent != null)
                return this.GetMaster(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Get a temp for <paramref name="cnt"/>, if it exists.
        /// </summary>        
        /// <param name="cnt">Content item to get a temp for</param>        
        /// <returns>Temp version of <paramref name="cnt"/>, if it exists.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="cnt"/> is <c>null</c>.</exception>
        public Telerik.Sitefinity.GenericContent.Model.Content GetTemp(Telerik.Sitefinity.GenericContent.Model.Content cnt)
        {
            var agent = cnt as AgentItem;
            if (agent != null)
                return this.GetTemp(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns true or false, depending on whether the <paramref name="item"/> is checked out or not
        /// </summary>
        /// <param name="item">Item to test</param>        
        /// <returns>True if the item is checked out, false otherwize.</returns>
        public bool IsCheckedOut(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.IsCheckedOut(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Checks if <paramref name="item"/> is checked out by user with a specified id
        /// </summary>
        /// <param name="item">Item to test</param>
        /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item"/></param>        
        /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
        public bool IsCheckedOutBy(Telerik.Sitefinity.GenericContent.Model.Content item, Guid userId)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.IsCheckedOutBy(agent, userId);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Publishes the content in master state. Content becomes live after the publish.
        /// </summary>
        /// <param name="item">Content in master state that is to be published.</param>
        public Telerik.Sitefinity.GenericContent.Model.Content Publish(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.Publish(agent);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Schedule a content item - to be published from one date to another
        /// </summary>
        /// <param name="item">Content item in master state</param>
        /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
        /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>   
        public Telerik.Sitefinity.GenericContent.Model.Content Schedule(Telerik.Sitefinity.GenericContent.Model.Content item, DateTime publicationDate, DateTime? expirationDate)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.Schedule(agent, publicationDate, expirationDate);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Unpublish a content item in live state.
        /// </summary>
        /// <param name="item">Live item to unpublish.</param>
        /// <returns>Master (draft) state.</returns>
        public Telerik.Sitefinity.GenericContent.Model.Content Unpublish(Telerik.Sitefinity.GenericContent.Model.Content item)
        {
            var agent = item as AgentItem;
            if (agent != null)
                return this.Unpublish(agent);
            throw new NotSupportedException();
        }

        #endregion

        #endregion

        /// <summary>
        /// Gets the lifecycle decorator
        /// </summary>
        /// <value>The lifecycle.</value>
        public ILifecycleDecorator Lifecycle
        {
            get
            {
                return LifecycleFactory.CreateLifecycle<AgentItem>(this, this.Copy);
            }
        }

        /// <summary>
        /// Creates a language data instance
        /// </summary>
        /// <returns></returns>
        public LanguageData CreateLanguageData()
        {
            return this.Provider.CreateLanguageData();
        }

        /// <summary>
        /// Creates a language data instance
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public LanguageData CreateLanguageData(Guid id)
        {
            return this.Provider.CreateLanguageData(id);
        }

        /// <summary>
        /// Gets language data instance by its Id
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public LanguageData GetLanguageData(Guid id)
        {
            return this.Provider.GetLanguageData(id);
        }
    }
}
