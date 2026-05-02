using BookstoreApp.Data;
using BookstoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Services;

public class BookService
{
    private readonly BookStoreContext context;

    public BookService(BookStoreContext context)
    {
        this.context = context;
    }

    public void AddBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
        Console.WriteLine("Книга добавлена");
    }

    public void EditBook(Book book)
    {
        var existingBook = context.Books.FirstOrDefault(b => b.Id == book.Id);

        if (existingBook == null)
        {
            Console.WriteLine("Книга не найдена");
            return;
        }

        existingBook.Title = book.Title;
        existingBook.AuthorId = book.AuthorId;
        existingBook.Publisher = book.Publisher;
        existingBook.Pages = book.Pages;
        existingBook.GenreId = book.GenreId;
        existingBook.Year = book.Year;
        existingBook.CostPrice = book.CostPrice;
        existingBook.SalePrice = book.SalePrice;
        existingBook.SequelId = book.SequelId;
        existingBook.Stock = book.Stock;

        context.SaveChanges();

        Console.WriteLine("Книга обновлена");
    }

    public void DeleteBook(int id)
    {
        var book = context.Books.Find(id);
        if (book != null)
        {
            context.Books.Remove(book);
            context.SaveChanges();
            Console.WriteLine("Книга удалена");
        }
    }

    public void WriteOffBook(int id, int quantity)
    {
        var book = context.Books.Find(id);
        if (book != null)
        {
            book.Stock -= quantity;
            if (book.Stock < 0) book.Stock = 0;
            context.SaveChanges();
            Console.WriteLine("Книга списана");
        }
    }

    public List<Book> Search(string title, string author, string genre)
    {
        return context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b =>
                (title == null || b.Title.Contains(title)) &&
                (author == null || b.Author.FullName.Contains(author)) &&
                (genre == null || b.Genre.Name.Contains(genre)))
            .ToList();
    }

    public Author? GetAuthorById(int id) => context.Authors.Find(id);

    public Genre? GetGenreById(int id) => context.Genres.Find(id);

    public Author AddAuthor(string fullName)
    {
        var author = new Author { FullName = fullName };
        context.Authors.Add(author);
        context.SaveChanges();
        return author;
    }

    public Genre AddGenre(string name)
    {
        var genre = new Genre { Name = name };
        context.Genres.Add(genre);
        context.SaveChanges();
        return genre;
    }

    public List<Book> GetNewBooks()
    {
        var date = DateTime.Now.AddDays(-30);
        return context.Books.Include(b => b.Author).Include(b => b.Genre)
            .Where(b => b.Year >= date.Year)
            .ToList();
    }
}