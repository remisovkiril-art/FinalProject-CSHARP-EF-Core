namespace BookstoreApp.Entities;

public class Reservation
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.Now;
}