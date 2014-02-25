using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Telerik.StarterKit.Modules.Agents.Web.Services.Data
{
    /// <summary>
    /// This class represents the message data that will be send
    /// </summary>
    [DataContract]
    public class EMailDataContext
    {
        /// <summary>
        /// Gets or sets the id of the agent whom we want to send the message
        /// </summary>
        [DataMember]
        public Guid AgentId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the sender
        /// </summary>
        [DataMember]
        public string FromName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email of the sender
        /// </summary>
        [DataMember]
        public string FromEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of property for which a mail is sent
        /// </summary>
        [DataMember]
        public string Property
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message body
        /// </summary>
        [DataMember]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Determines if a copy of the message should be send to the sender
        /// </summary>
        [DataMember]
        public bool SendCopy
        {
            get;
            set;
        }

        /// <summary>
        /// Used to determine in the front end if the message has been sent successfully
        /// </summary>
        [DataMember]
        public bool HasSentMail
        {
            get;
            set;
        }
    }
}
