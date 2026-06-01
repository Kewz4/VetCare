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

// 1. Álbumes ordenados por título (A-Z)
var albumesOrdenados = db.Albums
    .OrderBy(a => a.Title)
    .Select(a => new { a.AlbumId, a.Title, Artista = a.Artist.Name })
    .Take(20)
    .ToList();

albumesOrdenados.Dump();

// 2. Clientes ordenados por país y dentro por nombre
var clientesOrdenados = db.Customers
    .OrderBy(c => c.Country)
    .ThenBy(c => c.LastName)
    .Select(c => new { c.LastName, c.FirstName, c.Country })
    .Take(20)
    .ToList();

clientesOrdenados.Dump();