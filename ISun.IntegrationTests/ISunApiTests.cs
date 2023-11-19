using ISun.Infrastructure.ExternalServices.Models;
using ISun.Infrastructure.ExternalServices.Services;

namespace ISun.IntegrationTests;

public class ISunApiTests
{
    private HttpClient _httpClient;
    private WeatherForecastOfCityISunHttpClient _weatherForecastOfCityISunHttpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _weatherForecastOfCityISunHttpClient = new WeatherForecastOfCityISunHttpClient(_httpClient);
    }

    [Test]
    public async Task Login_ValidInput_GetResponse()
    {
        var status = await _weatherForecastOfCityISunHttpClient.LoginISunApi();
        Assert.AreEqual(true,status);
    }
    
    [Test]
    public async Task GetCities_ExistElements_GetCitiesOfNameFromAPI()
    {
        await _weatherForecastOfCityISunHttpClient.LoginISunApi();
        var cities = await _weatherForecastOfCityISunHttpClient.GetCitiesISunApi();
        Assert.Greater(cities.Count(), 0);
    }
    
    [Test]
    public async Task GetWeatherForecast_ExistElements_GetWeatherForecastFromAPI()
    {
        await _weatherForecastOfCityISunHttpClient.LoginISunApi();
        var cities = await _weatherForecastOfCityISunHttpClient.GetCitiesISunApi();
        var weatherForecastOfCities = new List<WeatherForecastOfCityISun>();
        
        foreach (var city in cities)
        {
            weatherForecastOfCities.Add(await _weatherForecastOfCityISunHttpClient.GetWeatherForecastOfCityISunApi(city));
        }

        var weatherForecastFirstCity = weatherForecastOfCities.FirstOrDefault();
        
        Assert.Greater(weatherForecastOfCities.Count(), 0);
        Assert.IsNotNull(weatherForecastFirstCity?.City);
        Assert.IsNotNull(weatherForecastFirstCity?.Precipitation);
        Assert.IsNotNull(weatherForecastFirstCity?.Summary);
        Assert.IsNotNull(weatherForecastFirstCity?.Temperature);
        Assert.IsNotNull(weatherForecastFirstCity?.WindSpeed);
    }
}