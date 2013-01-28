using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace DRCOG.Common.Interfaces
{
    public interface IEmailService
    {
        String SmtpServer { get; }

        void Send(MailMessage message);

        void Send(String from, String to, String subject, String body);

        void Send(MailAddress from, MailAddress to, String subject, String body);

        void Send(String from, String to, String subject, String body, IEnumerable<Attachment> attachments);

        void Send(MailAddress from, MailAddress to, String subject, String body, IEnumerable<Attachment> attachments);
    }
}
