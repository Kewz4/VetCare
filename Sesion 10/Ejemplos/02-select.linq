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

// 1. Obtener solo nombre y duración de canciones
var cancionesResumen = db.Tracks
    .Select(t => new { 
        t.Name, 
        DuracionSegundos = t.Milliseconds / 1000 
    })
    .Take(10)
    .ToList();

cancionesResumen.Dump();

// 2. Facturas con total calculado
var facturas = db.Invoices
    .Select(i => new {
        i.InvoiceId,
        i.InvoiceDate,
        Total = i.InvoiceLines.Sum(il => il.Quantity * il.UnitPrice)
    })
    .Take(10)
    .ToList();

facturas.Dump();