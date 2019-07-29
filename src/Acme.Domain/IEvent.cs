using System;

namespace Acme.Domain
{
    public interface IEvent
    {
        DateTime EventTimestamp { get; }
    }
}
