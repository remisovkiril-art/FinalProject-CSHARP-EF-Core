using BookstoreApp.Data;
using BookstoreApp.Entities;

namespace BookstoreApp.Services;

public class PromotionService
{
    private readonly BookStoreContext context;

    public PromotionService(BookStoreContext context)
    {
        this.context = context;
    }

    public void AddPromotion(string name, int discount, DateTime start, DateTime end, int? bookId = null)
    {
        var promotion = new Promotion
        {
            Name = name,
            DiscountPercent = discount,
            StartDate = start,
            EndDate = end,
            BookId = bookId
        };
        context.Promotions.Add(promotion);
        context.SaveChanges();
        Console.WriteLine("Промоакция успешно добавлена");
    }
}