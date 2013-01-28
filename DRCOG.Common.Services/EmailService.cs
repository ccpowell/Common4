using System;
using System.Net.Mail;
using DRCOG.Common.DesignByContract;
using System.Collections.Generic;
using DRCOG.Common.Interfaces;
using DRCOG.Common.Util;

namespace DRCOG.Common.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(String smtpServer)
        {
            Check.Require(!String.IsNullOrEmpty(smtpServer), "The smtp server is required.");
            SmtpServer = smtpServer;
            SmtpClient = new SmtpClient(SmtpServer);
        }

        protected virtual SmtpClient SmtpClient { get; set; }
        public virtual String SmtpServer { get; protected set; }
        
        public virtual void Send(MailMessage message)
        {
            RetryUtility.RetryAction(() => SmtpClient.Send(message), 3, 1000);
        }

        public virtual void Send(String from, String to, String subject, String body)
        {
            MailMessage message = new MailMessage(from, to, subject, body);
            Send(message);
        }

        public virtual void Send(MailAddress from, MailAddress to, String subject, String body)
        {
            Send(from, to, subject, body, null);
        }

        public virtual void Send(String from, String to, String subject, String body, IEnumerable<Attachment> attatchments)
        {
            MailAddress mailFrom = new MailAddress(from);
            MailAddress mailTo = new MailAddress(to);
            Send(mailFrom, mailTo, subject, body, null);
        }

        public virtual void Send(MailAddress from, MailAddress to, String subject, String body, IEnumerable<Attachment> attatchments)
        {
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.Subject = subject;
            
            if (attatchments != null)
            {
                foreach (Attachment attatchment in attatchments)
                {
                    message.Attachments.Add(attatchment);
                }
            }

            Send(message);
        }
    }
}
