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

// 1. Canciones agrupadas por género
var cancionesPorGenero = db.Tracks
    .GroupBy(t => t.Genre.Name)
    .Select(g => new {
        Genero = g.Key,
        Cantidad = g.Count(),
        DuracionPromedio = TimeSpan.FromMilliseconds(g.Average(t => t.Milliseconds)),
        PrecioPromedio = g.Average(t => t.UnitPrice)
    })
    .OrderByDescending(g => g.Cantidad)
    .ToList();

cancionesPorGenero.Dump();

// 2. Facturas agrupadas por año
var facturasPorAnio = db.Invoices
    .GroupBy(i => i.InvoiceDate.Year)
    .Select(g => new {
        Anio = g.Key,
        TotalFacturas = g.Count(),
        TotalVentas = g.Sum(i => i.InvoiceLines.Sum(il => il.Quantity * il.UnitPrice))
    })
    .OrderBy(g => g.Anio)
    .ToList();

facturasPorAnio.Dump();