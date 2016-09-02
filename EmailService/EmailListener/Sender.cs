using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using EmailServiceModels;
using AbstractService;

namespace EmailListener
{
	class Sender : IMessageEventHandler<Email>
	{
		SmtpClient smtp;
		EmailService service;

		public Sender(EmailService service)
		{
			smtp = new SmtpClient
			{
				Host = ServerSmtpCredits.ServerEmailHost,
                Port = ServerSmtpCredits.ServerEmailPort,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(ServerSmtpCredits.ServerEmail, ServerSmtpCredits.ServerEmailPassword)
            };
			this.service = service;
		}

		public void onMessage(Email mail)
		{
			using (var message = new MailMessage(ServerSmtpCredits.ServerEmail, mail.Receiver)
			{
				Subject = mail.Header,
				Body = mail.Text
			})
			{
				smtp.Send(message);
				string info = String.Format("Email has been sent to: {0}", mail.Receiver);
				service.writeToLog(info);
			}
		}
	}
}
