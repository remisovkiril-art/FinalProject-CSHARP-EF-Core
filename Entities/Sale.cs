namespace BookstoreApp.Entities;

public class Sale
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.Now;
}