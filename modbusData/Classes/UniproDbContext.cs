using System.Data.Entity;

public class UniproDbContext : DbContext
{
    // Pass the connection string name from App.config or an actual connection string
    public UniproDbContext(string connectionString)
        : base(connectionString)
    {
    }

    public DbSet<UniproModbus> Unipro_Modbus { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: If table or column names differ from class names
        modelBuilder.Entity<UniproModbus>().ToTable("Unipro_Setup");
    }
}
