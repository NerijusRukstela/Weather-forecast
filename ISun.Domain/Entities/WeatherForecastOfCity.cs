namespace ISun.Domain.Entities;

public class WeatherForecastOfCity
{
    public int Id { get; set; }
    public string City { get; set; }
    public int Temperature { get; set; }
    public int Precipitation { get; set; }
    public int WindSpeed { get; set; }
    public string Summary { get; set; }
}