using ISun.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISun.Infrastructure.Data.Configurations;

public class WeatherForecastOfCityConfiguration: IEntityTypeConfiguration<WeatherForecastOfCity>
{
    public void Configure(EntityTypeBuilder<WeatherForecastOfCity> builder)
    {
        builder.ToTable("WeatherForecastOfCities");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.City);
        builder.Property(x => x.Temperature);
        builder.Property(x => x.Precipitation);
        builder.Property(x => x.WindSpeed);
        builder.Property(x => x.Summary);
    }
}