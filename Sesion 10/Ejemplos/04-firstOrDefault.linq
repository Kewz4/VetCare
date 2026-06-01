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

// 1. Obtener el cliente más antiguo (por ID)
var primerCliente = db.Customers.FirstOrDefault();
primerCliente.Dump();

// 2. Obtener una canción específica
var cancion = db.Tracks
    .FirstOrDefault(t => t.Name.Contains("Bohemian"));

if (cancion != null)
    Console.WriteLine($"Canción encontrada: {cancion.Name}");
else
    Console.WriteLine("No se encontró la canción");