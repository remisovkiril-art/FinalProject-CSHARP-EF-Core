using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Entities;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public string Publisher { get; set; }
    public int Pages { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }

    public int Year { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }

    public int? SequelToBookId { get; set; }
    public int Stock { get; set; }
    public int? SequelId { get; set; }
    public Book? Sequel { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.Now;
}