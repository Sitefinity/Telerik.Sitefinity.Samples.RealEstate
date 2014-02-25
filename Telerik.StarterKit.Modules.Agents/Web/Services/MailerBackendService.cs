using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.StarterKit.Modules.Agents.Web.Services.Data;
using Telerik.StarterKit.Modules.Agents.Data;
using Telerik.StarterKit.Modules.Agents.Model;
using System.Net.Mail;
using System.Net;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.StarterKit.Modules.Agents.Web.Services
{
    /// <summary>
    /// Web service for sending emails to agents.
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MailerBackendService : IMailerService
    {
        #region Public methods

        /// <summary>
        /// Sends an email to the specified agent from the MailDataContext
        /// </summary>
        /// <param name="mailData">The email data context sent from the client containing the necessary information for sending the email.</param>
        /// <returns>The email data context containing the result of the execution of the method in HasSentMail property.</returns>
        public EMailDataContext SendEmail(EMailDataContext mailData)
        {
            return this.SendEmailInternal(mailData);
        }

        #endregion

        #region Private methods

        private EMailDataContext SendEmailInternal(EMailDataContext mailData)
        {
            if (mailData == null)
                throw new ArgumentNullException("Mail data cannot be null.");

            if (mailData.AgentId == null)
                throw new ArgumentNullException("AgentId cannot be null");

            AgentsManager manager = AgentsManager.GetManager();
            AgentItem agent = manager.GetAgent(mailData.AgentId);

            string fromEmail = mailData.FromEmail;
            string fromName = mailData.FromName;

            try
            {
                // Replace some of the variables depending on your settings 
                SmtpClient smtpClient = new SmtpClient();
                var smtpSettings = Config.Get<SystemConfig>().SmtpSettings;
                smtpClient.Host = smtpSettings.Host;
                smtpClient.Port = smtpSettings.Port;
                smtpClient.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);

                MailAddress senderMail = new MailAddress(fromEmail, fromName);
                MailAddress agentMail = new MailAddress(agent.Email, agent.Title);

                MailMessage message = new MailMessage(senderMail, agentMail);
                message.Subject = "Real estate contact form response.";
                message.Body = mailData.Message;
                message.IsBodyHtml = false;

                smtpClient.Send(message);

                if (mailData.SendCopy)
                {
                    MailAddress realEstateMail = new MailAddress(smtpSettings.DefaultSenderEmailAddress, "Real Estate");
                    MailMessage copy = new MailMessage(realEstateMail, senderMail);
                    copy.Subject = "Copy from message sent to " + agent.Email;
                    copy.Body = mailData.Message;
                    smtpClient.Send(copy);
                }
                mailData.HasSentMail = true;
            }
            catch (Exception)
            {
                mailData.HasSentMail = false;
            }
            return mailData;
        }

        #endregion
    }
}
