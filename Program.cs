using BookstoreApp.Data;
using BookstoreApp.Menu;
using BookstoreApp.Services;
using BookstoreApp.HttpServer;
using Microsoft.Extensions.Configuration;
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
var context = new BookStoreContext(configuration);
var bookService = new BookService(context);
var salesService = new SalesService(context);
var reservationService = new ReservationService(context);
var promotionService = new PromotionService(context);
var statisticsService = new StatisticsService(context);
var server = new HttpServer(context);
Task.Run(() => server.Start());
var menu = new ConsoleMenu(
    bookService,
    salesService,
    reservationService,
    promotionService,
    statisticsService);
menu.Show();