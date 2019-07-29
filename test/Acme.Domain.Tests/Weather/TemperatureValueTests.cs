using Acme.Domain.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Acme.Domain.Tests.Weather
{
    [Trait("Domain.Common", "TemperatureValue")]
    public class TemperatureValueTests
    {
        //[Fact(DisplayName ="")]
        //public void FromFahrenheit_Valid_MapsFahrenheit()
        //{
        //    // Arrange
        //    var fahrenheitTemp = 72.01;

        //    // Act
        //    var temperature = TemperatureValue.FromFahrenheit(fahrenheitTemp);

        //    // Assert
        //    Assert.Equal(fahrenheitTemp, temperature.Fahrenheit);
        //}

        //[Theory(DisplayName = "FromFahrenheit creates a TemperatureValue with matching celcius value")]
        //[InlineData(32, 0)]
        //[InlineData(0, -17.7778)]
        //[InlineData(75.42, 24.122222)]
        //public void FromFahrenheit_Valid_CalculatesCelcius(double actualFahrenheit, double expectedCelsius)
        //{
        //    // Act
        //    var temperature = TemperatureValue.FromFahrenheit(actualFahrenheit);

        //    // Assert
        //    Assert.Equal(expectedCelsius, temperature.Celcius);
        //}

        //[Fact(DisplayName = "")]
        //public void FromCelcius_Valid_MapsCelsius()
        //{
        //    // Arrange
        //    var celciusTemp = 17.01;

        //    // Act
        //    var temperature = TemperatureValue.FromCelcius(celciusTemp);

        //    // Assert
        //    Assert.Equal(celciusTemp, temperature.Celcius);
        //}

        //[Theory(DisplayName = "FromFahrenheit creates a TemperatureValue with matching celcius value")]
        //[InlineData(0, 32)]
        //[InlineData(-17.7778, 0)]
        //[InlineData(24.122222, 75.42)]
        //public void FromCelsius_Valid_CalculatesFahrenheit(double inputCelcius, double expectedFahrenheit)
        //{
        //    // Act
        //    var temperature = TemperatureValue.FromCelcius(inputCelcius);

        //    // Assert
        //    Assert.Equal(expectedFahrenheit, temperature.Celcius);
        //}
    }
}
