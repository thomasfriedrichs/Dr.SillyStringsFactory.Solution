using Microsoft.EntityFrameworkCore;

namespace StringFactory.Models
{
  public class StringFactoryContext : DbContext
  {
    public virtual DbSet<Mechanic> Categories { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MechanicMachine> MechanicMachine { get; set; }

    public StringFactoryContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}