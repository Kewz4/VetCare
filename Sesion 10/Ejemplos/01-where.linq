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

// 1. Canciones con duración mayor a 5 minutos (300,000 milisegundos)
var cancionesLargas = db.Tracks
    .Where(t => t.Milliseconds > 300000)
    .Select(t => new { t.Name, t.Milliseconds })
    .ToList();

cancionesLargas.Dump();


// 2. Canciones de Rock (GenreId = 1)
var rock = db.Tracks
    .Where(t => t.GenreId == 1)
    .Select(t => new { t.Name, Genero = t.Genre.Name })
    .Take(10)
    .ToList();

rock.Dump();