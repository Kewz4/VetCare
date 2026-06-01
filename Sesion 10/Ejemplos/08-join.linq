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

// 1. Canciones con su género (Join implícito - más fácil)
var cancionesConGenero = db.Tracks
    .Select(t => new {
        t.Name,
        Genero = t.Genre.Name,
        t.UnitPrice
    })
    .Take(20)
    .ToList();

cancionesConGenero.Dump();

// 2. Facturas con nombre del cliente
var facturasConClientes = db.Invoices
    .Select(i => new {
        i.InvoiceId,
        i.InvoiceDate,
        Cliente = i.Customer.FirstName + " " + i.Customer.LastName,
        i.Customer.Country,
        Total = i.InvoiceLines.Sum(il => il.Quantity * il.UnitPrice)
    })
    .OrderByDescending(i => i.Total)
    .Take(20)
    .ToList();

facturasConClientes.Dump();

// 3. Playlists con sus canciones
var playlistsConCanciones = db.Playlists
    .Select(p => new {
        p.Name,
        CantidadCanciones = p.PlaylistTracks.Count(),
        DuracionTotal = TimeSpan.FromMilliseconds(
            p.PlaylistTracks.Sum(pt => pt.Track.Milliseconds))
    })
    .OrderByDescending(p => p.CantidadCanciones)
    .Take(10)
    .ToList();

playlistsConCanciones.Dump();