using Microsoft.EntityFrameworkCore;

namespace AzureAirlines;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AzureRequest> AzureRequests { get; set; } = null!;
}

public class AzureRequest
{
    public int Id { get; set; }
    public required string AppName { get; set; }
    public required DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
    public required DateTimeOffset LastUpdatedOn { get; set; } = DateTimeOffset.Now;
}