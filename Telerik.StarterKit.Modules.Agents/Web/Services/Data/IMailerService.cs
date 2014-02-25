using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.StarterKit.Modules.Agents.Web.Services.Data
{
    /// <summary>
    /// Defines the members of the mailer backend service
    /// </summary>
    [ServiceContract]
    internal interface IMailerService
    {
        /// <summary>
        /// Sends an email to the specified agent from the MailDataContext
        /// </summary>
        /// <param name="mailData">The email data context sent from the client containing the necessary information for sending the email.</param>
        /// <returns>The email data context containing the result of the execution of the method in HasSentMail property.</returns>
        [WebHelp(Comment = "Sends email to an agent.")]
        [WebInvoke(Method = "PUT", UriTemplate = "SendMail/", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        EMailDataContext SendEmail(EMailDataContext mailData);
    }
}
