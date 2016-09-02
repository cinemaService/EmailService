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

namespace TestPublisher
{
	public class MessageCreator : IMessageCreator
	{
		public Email mail { get; set; }

		public MessageCreator(Email mail)
		{
			this.mail = mail;
		}

		IMessage IMessageCreator.CreateMessage(ISession session)
		{
			ActiveMQObjectMessage msg = new ActiveMQObjectMessage();
			msg.Body = this.mail;
			return msg;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Email testModel = new Email();
			testModel.Receiver = "marcknap@interia.eu";
			testModel.Text = "TEST TEST TEST";
			testModel.Header = "Nagłówek";

			ConnectionFactory conFactory = new ConnectionFactory(Config.Url);
			NmsTemplate temp = new NmsTemplate(conFactory);
			temp.Send(Config.QueueName, new MessageCreator(testModel));
		}
	}
}
