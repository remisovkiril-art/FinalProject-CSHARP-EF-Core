namespace BookstoreApp.Entities;

public class Promotion
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DiscountPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int? BookId { get; set; }
    public Book Book { get; set; }
}