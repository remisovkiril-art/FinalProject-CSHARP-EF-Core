using System.Net;
using System.Text;
using BookstoreApp.Data;
namespace BookstoreApp.HttpServer;
public class HttpServer
{
    private readonly HttpListener listener;
    private readonly BookStoreContext db;
    public HttpServer(BookStoreContext context)
    {
        db = context;
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
    }
    public void Start()
    {
        listener.Start();
        Console.WriteLine("Сервер запущен: http://localhost:5000/");
        while (true)
        {
            var ctx = listener.GetContext();
            HandleRequest(ctx);
        }
    }
    private void HandleRequest(HttpListenerContext ctx)
    {
        var path = ctx.Request.Url.AbsolutePath;
        if (path == "/")
            SendFile(ctx, "wwwroot/index.html");
        else if (path == "/books")
            BookEndpoints.GetAll(ctx, db);
        else if (path.StartsWith("/filter"))
            BookEndpoints.Filter(ctx, db);
        else if (path == "/stats")
            BookEndpoints.Stats(ctx, db);
        else
            SendFile(ctx, "wwwroot/notfound.html");
    }
    public static void SendHtml(HttpListenerContext ctx, string content)
    {
        var layout = File.ReadAllText("wwwroot/layout.html");
        var full = layout.Replace("{{content}}", content);

        var buffer = Encoding.UTF8.GetBytes(full);
        ctx.Response.ContentType = "text/html; charset=utf-8";
        ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);
        ctx.Response.Close();
    }
    private void SendFile(HttpListenerContext ctx, string path)
    {
        if (!File.Exists(path))
        {
            SendHtml(ctx, "<h2>Файл не найден</h2>");
            return;
        }

        var content = File.ReadAllText(path);
        SendHtml(ctx, content);
    }
}