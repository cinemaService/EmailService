using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.Util;
using Apache.NMS.ActiveMQ;
using Spring.Messaging.Nms.Listener;
using EmailServiceModels;
using Spring.Messaging.Nms.Core;
using Apache.NMS.ActiveMQ.Commands;

namespace EmailListener
{
	public class MessageListener : IMessageListener
	{
		public void OnMessage(IMessage message)
		{
			try
			{
				ActiveMQObjectMessage msg = message as ActiveMQObjectMessage;
				if (msg != null)
				{
					Email  msgEmail = (Email)msg.Body;
					Sender sender = new Sender();
					sender.send(msgEmail);
					Console.WriteLine("E-mail sended to: {0} with text: {1}",msgEmail.Receiver,msgEmail.Text);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}

	class EmailService : AbstractService.AbstractService
	{
		public EmailService(string serviceName) : base(serviceName)
		{
		}

		public void listen()
		{
			try
			{ 
				ConnectionFactory factory = new ConnectionFactory(Config.Url);

				SimpleMessageListenerContainer listenerContainer = new SimpleMessageListenerContainer();
				listenerContainer.ConnectionFactory = factory;
				listenerContainer.DestinationName = Config.QueueName;
				listenerContainer.MessageListener = new MessageListener();
				listenerContainer.AfterPropertiesSet();

				Console.WriteLine("Email listener started!");
				Console.WriteLine("Press ENTER to exit.");
				Console.ReadLine();
			}
			catch (System.Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		static void Main(string[] args)
		{
			EmailService service = new EmailService("E-mail Service");
			service.listen();
		}
	}
}
