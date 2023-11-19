using ISun.Infrastructure.ExternalServices.Interfaces;
using ISun.Infrastructure.ExternalServices.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ISun.Infrastructure.ExternalServices.Services;

public class WeatherForecastOfCityISunHttpClient : ISunApiClientFactory, IWeatherForecastOfCityISunHttpClient
{
    private readonly HttpClient _httpClient;
    private const string USERNAME = "isun";
    private const string PASSWORD = "passwrod";
    
    public WeatherForecastOfCityISunHttpClient(HttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WeatherForecastOfCityISun> GetWeatherForecastOfCityISunApi(string city)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/weathers/{city}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<WeatherForecastOfCityISun>();
        }

        throw new HttpRequestException($"API request (GetForecastOfCityISunApi) failed with status code {response.StatusCode}");
    }
    
    public async Task<IEnumerable<string>> GetCitiesISunApi()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/cities");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<string>>();
        }

        throw new HttpRequestException($"API request (GetCitiesISunApi) failed with status code {response.StatusCode}");
    }
    
    public async Task<bool> LoginISunApi()
    {
        var user = new
        {
            username = USERNAME,
            password = PASSWORD
        };
        
        HttpContent content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("api/authorize", content);

        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsAsync<LoginResponse>();
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", res.Token);
            
            return response.IsSuccessStatusCode;
        }

        throw new HttpRequestException($"API request (LoginISunApi) failed with status code {response.StatusCode}");
    }
}