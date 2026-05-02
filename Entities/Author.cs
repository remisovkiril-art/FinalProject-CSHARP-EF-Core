namespace BookstoreApp.Entities;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public List<Book> Books { get; set; } = new();
}