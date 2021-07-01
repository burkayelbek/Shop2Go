using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using Shop2GoAPI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Shop2GoAPI.Models
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;

        public Mailer(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;

        }
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.Server);
                    }

                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}

