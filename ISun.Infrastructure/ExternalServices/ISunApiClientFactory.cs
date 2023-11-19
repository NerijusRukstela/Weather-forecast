namespace ISun.Infrastructure.ExternalServices;

public class ISunApiClientFactory
{
    protected ISunApiClientFactory(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri("https://weather-api.isun.ch/");
    }
}