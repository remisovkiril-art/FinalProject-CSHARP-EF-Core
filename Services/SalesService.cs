using BookstoreApp.Data;
using BookstoreApp.Entities;

namespace BookstoreApp.Services;

public class SalesService
{
    private readonly BookStoreContext context;

    public SalesService(BookStoreContext context)
    {
        this.context = context;
    }

    public void SellBook(int bookId, int quantity)
    {
        var book = context.Books.Find(bookId);
        if (book == null)
        {
            Console.WriteLine("Книга не найдена");
            return;
        }
        if (book.Stock < quantity)
        {
            Console.WriteLine("В наличии недостаточно книг");
            return;
        }

        book.Stock -= quantity;

        var sale = new Sale
        {
            BookId = bookId,
            Quantity = quantity,
            SaleDate = DateTime.Now
        };

        context.Sales.Add(sale);
        context.SaveChanges();
        Console.WriteLine("Книга успешно продана");
    }
}