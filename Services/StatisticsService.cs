using BookstoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Services;

public class StatisticsService
{
    private readonly BookStoreContext context;

    public StatisticsService(BookStoreContext context)
    {
        this.context = context;
    }

    public void MostPopularBooks()
    {
        var books = context.Sales
            .Include(s => s.Book)
            .GroupBy(s => s.Book.Title)
            .Select(g => new { Title = g.Key, Count = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        foreach (var b in books)
        {
            Console.WriteLine($"{b.Title} - {b.Count} sold");
        }
    }

    public void MostPopularAuthors()
    {
        var authors = context.Sales
            .Include(s => s.Book)
            .ThenInclude(b => b.Author)
            .GroupBy(s => s.Book.Author.FullName)
            .Select(g => new { Author = g.Key, Count = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        foreach (var a in authors)
        {
            Console.WriteLine($"{a.Author} - {a.Count} книги проданы");
        }
    }

    public void MostPopularGenres()
    {
        var genres = context.Sales
            .Include(s => s.Book)
            .ThenInclude(b => b.Genre)
            .GroupBy(s => s.Book.Genre.Name)
            .Select(g => new { Genre = g.Key, Count = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        foreach (var g in genres)
        {
            Console.WriteLine($"{g.Genre} - {g.Count} книги проданы");
        }
    }
}