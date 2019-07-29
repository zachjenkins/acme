using System;

namespace Acme.Domain
{
    public class WeatherUpdateTriggerEvent : IEvent
    {
        public string ZipCode { get; private set; }
        public DateTime EventTimestamp { get; private set; }

        public WeatherUpdateTriggerEvent(string zipCode)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentNullException(nameof(zipCode));

            ZipCode = zipCode;
            EventTimestamp = DateTime.UtcNow;
        }
    }
}
