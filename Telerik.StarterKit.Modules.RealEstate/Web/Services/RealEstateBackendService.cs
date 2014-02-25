using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Modules;
using Telerik.StarterKit.Modules.RealEstate.Model;
using Telerik.StarterKit.Modules.RealEstate.Web.Services.Data;
using Telerik.StarterKit.Modules.RealEstate.Data;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.StarterKit.Modules.RealEstate.Web.Services
{
    /// <summary>
    /// Services for Real Estate items that is used in the Sitefinity backend
    /// </summary>
    public class RealEstateBackendService : ContentServiceBase<RealEstateItem, RealEstateItemViewModel, RealEstateManager>
    {
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="parentId">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override IQueryable<RealEstateItem> GetChildContentItems(Guid parentId, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Get a product by primary key
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <param name="providerName">Provider to use</param>
        /// <returns>Product item</returns>
        public override RealEstateItem GetContentItem(Guid id, string providerName)
        {
            return this.GetManager(providerName).GetItem(id);
        }

        /// <summary>
        /// Get a query of all products in a provider
        /// </summary>
        /// <param name="providerName">Name of the provider to use</param>
        /// <returns>Query of all products in the provider</returns>
        public override IQueryable<RealEstateItem> GetContentItems(string providerName)
        {
            return this.GetManager(providerName).GetItems();
        }

        /// <summary>
        /// Get an instance of the products manager with a specific provider
        /// </summary>
        /// <param name="providerName">Name of the provider to use</param>
        /// <returns>Instance of the products manager</returns>
        public override RealEstateManager GetManager(string providerName)
        {
            return RealEstateManager.GetManager(providerName);
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="id">Not used</param>
        /// <param name="providerName">Not used</param>
        /// <returns>Throws exception</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override RealEstateItem GetParentContentItem(Guid id, string providerName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Convert a list of model items into a list of viewmodel items
        /// </summary>
        /// <param name="contentList">List of items</param>
        /// <param name="dataProvider">Provider to use</param>
        /// <returns>List of view model items</returns>
        public override IEnumerable<RealEstateItemViewModel> GetViewModelList(IEnumerable<RealEstateItem> contentList, ContentDataProviderBase dataProvider)
        {
            var viewModelList = new List<RealEstateItemViewModel>();

            foreach (var item in contentList)
            {
                viewModelList.Add(new RealEstateItemViewModel(item, dataProvider));
            }

            return viewModelList;
        }        
    }
}
