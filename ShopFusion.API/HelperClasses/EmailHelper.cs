using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace ShopFusion.API.HelperClasses
{
	public class EmailHelper : IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage();
			message.From.Add(MailboxAddress.Parse("donotreply@test.com"));
			message.To.Add(MailboxAddress.Parse(email));
			message.Subject = subject;
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = htmlMessage };

			using (var smtpClient = new SmtpClient())
			{
				smtpClient.Connect("smtp.freesmtpservers.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
				await smtpClient.SendAsync(message);
				await smtpClient.DisconnectAsync(true);
			}
		}
	}
}
