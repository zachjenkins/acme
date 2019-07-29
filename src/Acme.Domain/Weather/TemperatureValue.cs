namespace Acme.Domain.Weather
{
    public class TemperatureValue
    {
        public double Kelvin { get; set; }
        public double Celcius => GetCelciusFromKelvin(Kelvin);
        public double Fahrenheit => GetFahrenheitFromKelvin(Kelvin);

        public TemperatureValue(double kelvinTemp)
        {
            Kelvin = kelvinTemp;
        }

        private static double GetFahrenheitFromKelvin(double kelvinTemp)
            => (kelvinTemp - 273.15) * 9 / 5 + 32;

        private static double GetCelciusFromKelvin(double kelvinTemp)
            => kelvinTemp - 273.15;

        public static bool operator ==(TemperatureValue a, TemperatureValue b) => a.Kelvin == b.Kelvin;

        public static bool operator !=(TemperatureValue a, TemperatureValue b) => a.Kelvin != b.Kelvin;

        public static bool operator >(TemperatureValue a, TemperatureValue b) => a.Kelvin > b.Kelvin;

        public static bool operator <(TemperatureValue a, TemperatureValue b) => a.Kelvin < b.Kelvin;

        public static bool operator >=(TemperatureValue a, TemperatureValue b) => a.Kelvin >= b.Kelvin;

        public static bool operator <=(TemperatureValue a, TemperatureValue b) => a.Kelvin <= b.Kelvin;
    }
}
