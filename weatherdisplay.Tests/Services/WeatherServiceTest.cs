using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

using weatherdisplaytest.Core.Interfaces;
using weatherdisplaytest.Core.Implementations;
using weatherdisplaytest.Models;
using Newtonsoft.Json;

namespace weatherdisplaytest.Tests.Services
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.Initialize(m => m.AddProfile<MapperProfile>());
            Mapper.AssertConfigurationIsValid();
        }


        [TestMethod]
        public void TestGetCitiesMapping()
        {


            var citiesforcountry = @"<NewDataSet> <Table> <Country>Australia</Country> <City>Sydney</City> </Table> </NewDataSet>";
            var weatherforLondon = @"{name:'London',coord:{lon:-0.13,lat:51.51},main:{temp:13.77}}";
            var weatherservice = new Mock<IWeatherService>();
            weatherservice.Setup(x => x.GetCitiesByCountry("Australia")).Returns(citiesforcountry);
            weatherservice.Setup(x => x.GetWeatherDataForCity("London")).Returns(weatherforLondon);

            Mapper.Initialize(m => m.AddProfile<MapperProfile>());
            Mapper.AssertConfigurationIsValid();

            var citiesRes = weatherservice.Object.GetCitiesByCountry("Australia");

            Assert.AreSame(citiesforcountry, citiesRes);

            var Cities = Mapper.Map<string, List<CityWeather>>(citiesRes);

            Assert.IsTrue(Cities.Count == 1);
            Assert.AreEqual("Australia",Cities[0].Country);
            Assert.AreEqual("Sydney",Cities[0].Name);

            var weather = weatherservice.Object.GetWeatherDataForCity("London");

            var jsonDes = JsonConvert.DeserializeObject<RootObject>(weather);

            var weatherData = Mapper.Map<RootObject, CityWeather>(jsonDes);

            Assert.AreEqual("London",weatherData.Name);
            Assert.AreEqual("13.77",weatherData.Temperature);
            Assert.AreEqual("Lat: 51.51 Lon: -0.13", weatherData.Location);
        }
    }
}
