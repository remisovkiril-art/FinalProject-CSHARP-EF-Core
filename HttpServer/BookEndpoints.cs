using System.Net;
using System.Text;
using BookstoreApp.Data;
using System.Linq;
namespace BookstoreApp.HttpServer;
public static class BookEndpoints
{
    public static void GetAll(HttpListenerContext ctx, BookStoreContext db)
    {
        var books = db.Books.ToList();
        string html = "<h2>Все книги</h2><ul>";
        foreach (var b in books)
        {
            html += $"<li>{b.Title} (Остаток: {b.Stock})</li>";
        }
        html += "</ul>";
        HttpServer.SendHtml(ctx, html);
    }
    public static void Filter(HttpListenerContext ctx, BookStoreContext db)
    {
        var query = ctx.Request.QueryString["title"];

        var books = db.Books
            .Where(b => query == null || b.Title.ToLower().Contains(query.ToLower()))
            .ToList();
        string html = "<h2>Результаты поиска</h2>";

        if (books.Count == 0)
        {
            html += "<p>Ничего не найдено</p>";
        }
        else
        {
            html += "<ul>";
            foreach (var b in books)
            {
                html += $"<li>{b.Title} (Остаток: {b.Stock})</li>";
            }
            html += "</ul>";
        }
        HttpServer.SendHtml(ctx, html);
    }
    public static void Stats(HttpListenerContext ctx, BookStoreContext db)
    {
        var top = db.Sales
            .GroupBy(s => s.BookId)
            .Select(g => new { Id = g.Key, Count = g.Sum(x => x.Quantity) })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();
        string html = "<h2>Популярные книги</h2><ul>";
        foreach (var t in top)
        {
            var book = db.Books.Find(t.Id);
            html += $"<li>{book?.Title} — {t.Count} продаж</li>";
        }
        html += "</ul>";
        HttpServer.SendHtml(ctx, html);
    }
}