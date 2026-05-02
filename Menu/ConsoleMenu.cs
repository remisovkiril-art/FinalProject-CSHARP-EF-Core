using BookstoreApp.Services;
using BookstoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Menu;

public class ConsoleMenu
{
    private readonly BookService bookService;
    private readonly SalesService salesService;
    private readonly ReservationService reservationService;
    private readonly PromotionService promotionService;
    private readonly StatisticsService statisticsService;

    public ConsoleMenu(
        BookService bookService,
        SalesService salesService,
        ReservationService reservationService,
        PromotionService promotionService,
        StatisticsService statisticsService)
    {
        this.bookService = bookService;
        this.salesService = salesService;
        this.reservationService = reservationService;
        this.promotionService = promotionService;
        this.statisticsService = statisticsService;
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("\nМеню Книгарни");
            Console.WriteLine("1 Добавить книгу");
            Console.WriteLine("2 Редактировать книгу");
            Console.WriteLine("3 Удалить книгу");
            Console.WriteLine("4 Продать книгу");
            Console.WriteLine("5 Списать книгу");
            Console.WriteLine("6 Поиск книг");
            Console.WriteLine("7 Добавить акцию");
            Console.WriteLine("8 Зарезервировать книгу");
            Console.WriteLine("9 Показать новинки");
            Console.WriteLine("10 Самые популярные книги");
            Console.WriteLine("11 Самые популярные авторы");
            Console.WriteLine("12 Самые популярные жанры");
            Console.WriteLine("0 Выход");
            Console.Write("Выберите пункт: ");

            var key = Console.ReadLine();
            switch (key)
            {
                case "0": return;
                case "1": AddBook(); break;
                case "2": EditBook(); break;
                case "3": DeleteBook(); break;
                case "4": SellBook(); break;
                case "5": WriteOffBook(); break;
                case "6": SearchBooks(); break;
                case "7": AddPromotion(); break;
                case "8": ReserveBook(); break;
                case "9": ShowNewBooks(); break;
                case "10": MostPopularBooks(); break;
                case "11": MostPopularAuthors(); break;
                case "12": MostPopularGenres(); break;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    private void AddBook()
    {
        Console.Write("Id автора: ");
        int authorId = int.Parse(Console.ReadLine() ?? "0");

        var author = bookService.GetAuthorById(authorId);
        if (author == null)
        {
            Console.Write("Автор не найден. Введите ФИО нового автора: ");
            string fullName = Console.ReadLine();
            author = bookService.AddAuthor(fullName);
            authorId = author.Id;
        }
        Console.Write("Id жанра: ");
        int genreId = int.Parse(Console.ReadLine() ?? "0");

        var genre = bookService.GetGenreById(genreId);
        if (genre == null)
        {
            Console.Write("Жанр не найден. Введите название нового жанра: ");
            string name = Console.ReadLine();
            genre = bookService.AddGenre(name);
            genreId = genre.Id;
        }
        Console.Write("Название книги: "); var title = Console.ReadLine();
        Console.Write("Издательство: "); var publisher = Console.ReadLine();
        Console.Write("Количество страниц: "); int pages = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Год издания: "); int year = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Себестоимость: "); decimal cost = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Цена продажи: "); decimal sale = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Id книги, которой является продолжением 0 если нет: "); int sequel = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Количество на складе: "); int stock = int.Parse(Console.ReadLine() ?? "0");
        bookService.AddBook(new Book
        {
            Title = title,
            AuthorId = authorId,
            Publisher = publisher,
            Pages = pages,
            GenreId = genreId,
            Year = year,
            CostPrice = cost,
            SalePrice = sale,
            SequelToBookId = sequel == 0 ? null : sequel,
            Stock = stock,
            AddedDate = DateTime.Now 
        });
    }

    private void EditBook()
    {
        Console.Write("Id книги для редактирования: "); int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новое название: "); var title = Console.ReadLine();
        Console.Write("Новый Id автора: "); int authorId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новое издательство: "); var publisher = Console.ReadLine();
        Console.Write("Новое количество страниц: "); int pages = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новый Id жанра: "); int genreId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новый год издания: "); int year = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новая себестоимость: "); decimal cost = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новая цена продажи: "); decimal sale = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("Id книги, которой является продолжением 0 если нету: "); int sequel = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Новый склад: "); int stock = int.Parse(Console.ReadLine() ?? "0");

        bookService.EditBook(new Book
        {
            Id = id,
            Title = title,
            AuthorId = authorId,
            Publisher = publisher,
            Pages = pages,
            GenreId = genreId,
            Year = year,
            CostPrice = cost,
            SalePrice = sale,
            SequelToBookId = sequel == 0 ? null : sequel,
            Stock = stock
        });
    }

    private void DeleteBook()
    {
        Console.Write("Id книги для удаления: "); int id = int.Parse(Console.ReadLine() ?? "0");
        bookService.DeleteBook(id);
    }

    private void SellBook()
    {
        Console.Write("Id книги для продажи: "); int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Количество: "); int qty = int.Parse(Console.ReadLine() ?? "0");
        salesService.SellBook(id, qty);
    }

    private void WriteOffBook()
    {
        Console.Write("Id книги для списания: "); int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Количество: "); int qty = int.Parse(Console.ReadLine() ?? "0");
        bookService.WriteOffBook(id, qty);
    }

    private void SearchBooks()
    {
        Console.Write("Название книги оставьте пустой рядок чтобы увидеть всё: "); var title = Console.ReadLine();
        Console.Write("Автор оставьте пустой рядок чтобы увидеть всё: "); var author = Console.ReadLine();
        Console.Write("Жанр оставьте пустой рядок чтобы увидеть всё: "); var genre = Console.ReadLine();

        var books = bookService.Search(string.IsNullOrWhiteSpace(title) ? null : title,
                                       string.IsNullOrWhiteSpace(author) ? null : author,
                                       string.IsNullOrWhiteSpace(genre) ? null : genre);
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Id}: {b.Title} — {b.Author.FullName} ({b.Genre.Name}) — На складе: {b.Stock}");
        }
    }

    private void AddPromotion()
    {
        Console.Write("Название акции: "); var name = Console.ReadLine();
        Console.Write("Скидка (%): "); int discount = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Дата начала (yyyy-mm-dd): "); var start = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());
        Console.Write("Дата окончания (yyyy-mm-dd): "); var end = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());
        Console.Write("Id книги 0 если акция для всех: "); int bookId = int.Parse(Console.ReadLine() ?? "0");

        promotionService.AddPromotion(name, discount, start, end, bookId == 0 ? null : bookId);
    }

    private void ReserveBook()
    {
        Console.Write("Id книги для резервации: "); int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Имя покупателя: "); var name = Console.ReadLine();
        reservationService.ReserveBook(id, name);
    }

    private void ShowNewBooks()
    {
        Console.WriteLine("Новинки за последние 30 дней:");
        var books = bookService.GetNewBooks();
        foreach (var b in books)
        {
            Console.WriteLine($"{b.Title} — {b.Author.FullName} ({b.Genre.Name})");
        }
    }

    private void MostPopularBooks()
    {
        Console.WriteLine("Самые популярные книги:");
        statisticsService.MostPopularBooks();
    }

    private void MostPopularAuthors()
    {
        Console.WriteLine("Самые популярные авторы:");
        statisticsService.MostPopularAuthors();
    }

    private void MostPopularGenres()
    {
        Console.WriteLine("Самые популярные жанры:");
        statisticsService.MostPopularGenres();
    }
}