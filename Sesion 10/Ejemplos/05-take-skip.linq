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

// 1. Top 10 canciones más populares (por cantidad de veces en playlists)
var topCanciones = db.PlaylistTracks
    .GroupBy(pt => pt.Track.Name)
    .Select(g => new {
        Cancion = g.Key,
        VecesEnPlaylists = g.Count()
    })
    .OrderByDescending(g => g.VecesEnPlaylists)
    .Take(10)
    .ToList();

topCanciones.Dump();

// 2. Paginación de clientes (página 2, 10 por página)
var pagina2 = db.Customers
    .OrderBy(c => c.LastName)
    .Skip(10)
    .Take(10)
    .Select(c => new { c.LastName, c.FirstName, c.Email })
    .ToList();

pagina2.Dump();