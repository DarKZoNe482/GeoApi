using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ZoneInondable> Zones { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ZoneInondable>()
            .Property(z => z.Contour)
            .HasColumnType("Geometry");  // Utilisation de la colonne Geometry

        base.OnModelCreating(modelBuilder);
    }
}