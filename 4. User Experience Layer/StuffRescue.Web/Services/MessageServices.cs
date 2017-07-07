using Core.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace StuffRescue.Web.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private IConfigurationRoot _config;

        public AuthMessageSender(IOptions<AuthMessageSenderOptions> emailOptionsAccessor, IOptions<SMSoptions> smsOptionsAccessor,IConfigurationRoot config)
        {
            EmailOptions = emailOptionsAccessor.Value;
            SMSOptions = smsOptionsAccessor.Value;
            _config = config;
        }

        public AuthMessageSenderOptions EmailOptions { get; set; } //set only via Secret Manager
        public SMSoptions SMSOptions { get; }  // set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Execute(EmailOptions.SendGridKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_config[ConfigHelper.Email.Sender.From], _config[ConfigHelper.Email.Sender.Name]),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }


        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = SMSOptions.SMSAccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = SMSOptions.SMSAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            var msg = MessageResource.Create(
              to: new PhoneNumber(number),
              from: new PhoneNumber(SMSOptions.SMSAccountFrom),
              body: message);
            return Task.FromResult(0);
        }
    }
}
