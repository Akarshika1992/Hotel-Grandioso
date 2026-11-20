using Microsoft.EntityFrameworkCore;

// Gör att å, ä, ö och ² visas rätt i konsolen
Console.OutputEncoding = System.Text.Encoding.UTF8;

using var context = new HotelContext();

// Skapar databasen och kör migrationerna om det behövs
await context.Database.MigrateAsync();

var repository = new HotelRepository(context);
var service = new BookingService(repository);
var menu = new ConsoleMenu(repository, service);

await menu.RunAsync();