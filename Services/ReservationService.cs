using BookstoreApp.Data;
using BookstoreApp.Entities;

namespace BookstoreApp.Services;

public class ReservationService
{
    private readonly BookStoreContext context;

    public ReservationService(BookStoreContext context)
    {
        this.context = context;
    }

    public void ReserveBook(int bookId, string customerName)
    {
        var reservation = new Reservation
        {
            BookId = bookId,
            CustomerName = customerName,
            ReservationDate = DateTime.Now
        };
        context.Reservations.Add(reservation);
        context.SaveChanges();
        Console.WriteLine("Бронирование успешно зарезервировано");
    }
}