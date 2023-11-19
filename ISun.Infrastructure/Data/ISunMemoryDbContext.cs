using ISun.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISun.Infrastructure.Data;

public class ISunMemoryDbContext: DbContext
{
    public virtual DbSet<WeatherForecastOfCity> Cities { get; set; }

    public ISunMemoryDbContext(DbContextOptions<ISunMemoryDbContext> options)
        : base(options)
    {
    }
}