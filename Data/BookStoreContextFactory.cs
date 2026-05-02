using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookstoreApp.Data;

public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
{
    public BookStoreContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return new BookStoreContext(configuration);
    }
}