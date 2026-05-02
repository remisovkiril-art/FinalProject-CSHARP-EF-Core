using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BookstoreApp.Entities;

namespace BookstoreApp.Data;

public class BookStoreContext : DbContext
{
    private readonly IConfiguration configuration;

    public BookStoreContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var dbType = configuration["DatabaseType"];
        if (dbType == "MSSQL")
        {
            options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
        }
        else
        {
            options.UseMySql(configuration.GetConnectionString("MYSQLConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MYSQLConnection")));
        }
    }
}