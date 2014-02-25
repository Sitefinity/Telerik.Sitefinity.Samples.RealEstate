using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.StarterKit.Modules.Agents.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules;

namespace Telerik.StarterKit.Modules.Agents.Web.Services.Data
{
    public class AgentItemViewModel : ContentViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentItemViewModel"/> class.
        /// </summary>
        public AgentItemViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentItemViewModel"/> class.
        /// </summary>
        /// <param name="contentItem">The content item.</param>
        /// <param name="provider">The provider.</param>
        public AgentItemViewModel(AgentItem contentItem, ContentDataProviderBase provider)
            : base(contentItem, provider)
        {
            this.agentEmail = contentItem.Email;
            this.agentPhoneNumber = contentItem.PhoneNumber;
            this.agentAddress = contentItem.Address;
            this.agentPostalCode = contentItem.PostalCode;
            
        }

        #endregion
        
        #region Overrides

        /// <summary>
        /// Get live version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Live version of this.ContentItem</returns>
        protected override Content GetLive()
        {
            return this.provider.GetLiveBase<AgentItem>((AgentItem)this.ContentItem);
        }

        /// <summary>
        /// Get temp version of this.ContentItem using this.provider
        /// </summary>
        /// <returns>Temp version of this.ContentItem</returns>
        protected override Content GetTemp()
        {
            return this.provider.GetTempBase<AgentItem>((AgentItem)this.ContentItem);
        }

        #endregion

        #region Own properties

        /// <summary>
        /// Agent email
        /// </summary>
        public string Email
        {
            get { return this.agentEmail; }
            set { this.agentEmail = value; }
        }

        /// <summary>
        /// Agent phone number
        /// </summary>
        public string PhoneNumber
        {
            get { return this.agentPhoneNumber; }
            set { this.agentPhoneNumber = value; }
        }

        /// <summary>
        /// Agent address
        /// </summary>
        public string Address
        {
            get { return this.agentAddress; }
            set { this.agentAddress = value; }
        }

        /// <summary>
        /// Agent address
        /// </summary>
        public string PostalCode
        {
            get { return this.agentPostalCode; }
            set { this.agentPostalCode = value; }
        }

        #endregion

        #region Fields

        private string agentEmail;
        private string agentPhoneNumber;
        private string agentAddress;
        private string agentPostalCode;

        #endregion
    }
}
