using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Domain
{
    public interface IEventPublisher
    {
        Task PublishEvents(IEnumerable<IEvent> events);
    }
}
