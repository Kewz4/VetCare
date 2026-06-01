<Query Kind="Statements">
  <Connection>
    <ID>54bf9502-9daf-4093-88e8-7177c12aaaaa</ID>
    <NamingService>2</NamingService>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\ChinookDemoDb.sqlite</AttachFileName>
    <DisplayName>Demo database (SQLite)</DisplayName>
    <DriverData>
      <PreserveNumeric1>true</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
      <MapSQLiteDateTimes>true</MapSQLiteDateTimes>
      <MapSQLiteBooleans>true</MapSQLiteBooleans>
    </DriverData>
  </Connection>
</Query>

var db = this;

// 1. Estadísticas de la base de datos
int totalCanciones = db.Tracks.Count();
int totalAlbumes = db.Albums.Count();
int totalArtistas = db.Artists.Count();
int totalClientes = db.Customers.Count();

Console.WriteLine($"Canciones: {totalCanciones}");
Console.WriteLine($"Álbumes: {totalAlbumes}");
Console.WriteLine($"Artistas: {totalArtistas}");
Console.WriteLine($"Clientes: {totalClientes}");

// 2. ¿Hay canciones de Jazz? (GenreId = 2)
bool hayJazz = db.Tracks.Any(t => t.GenreId == 2);
Console.WriteLine($"¿Hay canciones de Jazz? {hayJazz}");

// 3. Clientes que han realizado compras
int clientesConCompras = db.Customers.Count(c => c.Invoices.Any());
Console.WriteLine($"Clientes que han comprado: {clientesConCompras}");