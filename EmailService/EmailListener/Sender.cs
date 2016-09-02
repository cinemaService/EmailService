using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using EmailServiceModels;

namespace EmailListener
{
	class Sender
	{
		public SmtpClient smtp;

		public Sender()
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
		}

		public void send(Email mail)
		{
			using (var message = new MailMessage(ServerSmtpCredits.ServerEmail, mail.Receiver)
			{
				Subject = mail.Header,
				Body = mail.Text
			})
			{
				smtp.Send(message);
			}
		}
	}
}
