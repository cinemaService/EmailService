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
using AbstractService;

namespace EmailListener
{
	class EmailService : AService<Email>
	{
		public EmailService(string serviceName) : base(serviceName)
		{
		}

		public void listen()
		{
			Sender sender = new Sender(this);
			base.listen(sender, Config.Url, Config.QueueName);
		}
		static void Main(string[] args)
		{
			EmailService service = new EmailService("E-mail Service");
			service.listen();
		}
	}
}
