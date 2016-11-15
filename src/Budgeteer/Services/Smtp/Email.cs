using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Budgeteer.Services.Smtp
{
    public class Email : MimeMessage
    {
        public SmtpOptions SmtpConfigurationOptions { get; set; }

        public void Send()
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(SmtpConfigurationOptions.Server, SmtpConfigurationOptions.Port, SmtpConfigurationOptions.UseSsl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (SmtpConfigurationOptions.RequiresAuthentication)
                {
                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(SmtpConfigurationOptions.User, SmtpConfigurationOptions.Password);
                }
                
                client.Send(this);
                client.Disconnect(true);
            }
        }

        public async Task SendAsync()
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(SmtpConfigurationOptions.Server, SmtpConfigurationOptions.Port, SmtpConfigurationOptions.UseSsl).ConfigureAwait(false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (SmtpConfigurationOptions.RequiresAuthentication)
                {
                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(SmtpConfigurationOptions.User, SmtpConfigurationOptions.Password).ConfigureAwait(false);
                }

                await client.SendAsync(this).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
