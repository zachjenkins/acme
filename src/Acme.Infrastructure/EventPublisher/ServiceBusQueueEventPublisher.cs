using Acme.Domain;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Infrastructure.EventPublisher
{
    public class ServiceBusQueueEventPublisher : IEventPublisher
    {
        private readonly IQueueClient queueClient;

        public ServiceBusQueueEventPublisher(IQueueClient queueClient)
        {
            this.queueClient = queueClient ?? throw new ArgumentNullException(nameof(queueClient));
        }

        public async Task PublishEvents(IEnumerable<IEvent> events)
        {
            /* Wouldn't do this IF statement in production scenario.
             * If there were a limit, custom exceptions or result messages would be used.
             * Otherwise larger numbers of messages would be batched into separate requests.
             */
            if (events.Count() > 100)
                throw new ArgumentException($"Number of events ({events.Count()}) exceeds limit of 100." );

            var messages = events.Select(ev =>
            {
                var json = JsonConvert.SerializeObject(ev);
                var bytes = Encoding.UTF8.GetBytes(json);

                var msg = new Message(bytes);
                msg.Label = ev.GetType().ToString();

                return msg;
            }).ToList();

            await queueClient.SendAsync(messages);
        }
    }
}
