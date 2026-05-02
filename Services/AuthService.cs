using BookstoreApp.Data;
using BookstoreApp.Entities;

namespace BookstoreApp.Services;

public class AuthService
{
    private readonly BookStoreContext context;

    public AuthService(BookStoreContext context)
    {
        this.context = context;
    }

    public User Login(string login, string password)
    {
        return context.Users
            .FirstOrDefault(u => u.Login == login && u.Password == password);
    }
}