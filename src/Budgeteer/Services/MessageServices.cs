using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgeteer.Services.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Budgeteer.Services
{
    public class MessageServices
    {
    }

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<EmailAccounts> options)
        {
            Options = options.Value.NoReply;
        }

        public SmtpOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var emailContainer = new Email()
            {
                SmtpConfigurationOptions = Options,
                Sender = new MailboxAddress(Options.Name, Options.Email)
            };
            emailContainer.From.Add(new MailboxAddress(Options.Name, Options.Email));
            emailContainer.To.Add(new MailboxAddress(email));
            emailContainer.Subject = subject;
            emailContainer.Body = new BodyBuilder() {HtmlBody = message }.ToMessageBody();

            return emailContainer.SendAsync();
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
