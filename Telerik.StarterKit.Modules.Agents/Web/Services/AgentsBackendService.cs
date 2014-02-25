using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.StarterKit.Modules.Agents.Web.Services.Data;
using Telerik.StarterKit.Modules.Agents.Data;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.StarterKit.Modules.Agents.Web.Services
{
    /// <summary>
    /// Services for agent items that is used in the Sitefinity backend
    /// </summary>
    public class AgentsBackendService : ContentServiceBase<AgentItem, AgentItemViewModel, AgentsManager>
    {
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="parentId">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override IQueryable<AgentItem> GetChildContentItems(Guid parentId, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Get a product by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="providerName">Provider to use</param>
        /// <returns>Product item</returns>
        public override AgentItem GetContentItem(Guid id, string providerName)
        {
            return this.GetManager(providerName).GetAgent(id);
        }

        /// <summary>
        /// Get a query of all products in a provider
        /// </summary>
        /// <param name="providerName">Name of the provider to use</param>
        /// <returns>Query of all products in the provider</returns>
        public override IQueryable<AgentItem> GetContentItems(string providerName)
        {
            return this.GetManager(providerName).GetAgents();
        }

        /// <summary>
        /// Get an instance of the products manager with a specific provider
        /// </summary>
        /// <param name="providerName">Name of the provider to use</param>
        /// <returns>Instance of the products manager</returns>
        public override AgentsManager GetManager(string providerName)
        {
            return AgentsManager.GetManager(providerName);
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="id">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override AgentItem GetParentContentItem(Guid id, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Convert a list of model items into a list of viewmodel items
        /// </summary>
        /// <param name="contentList">List of agent items</param>
        /// <param name="dataProvider">Provider to use</param>
        /// <returns>List of view model items</returns>
        public override IEnumerable<AgentItemViewModel> GetViewModelList(IEnumerable<AgentItem> contentList, ContentDataProviderBase dataProvider)
        {
            var viewModelList = new List<AgentItemViewModel>();

            foreach (var agent in contentList)
            {
                viewModelList.Add(new AgentItemViewModel(agent, dataProvider));
            }

            return viewModelList;
        }        
    }
}
