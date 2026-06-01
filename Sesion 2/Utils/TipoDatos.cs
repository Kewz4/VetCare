using System;

// 1. TIPOS DE DATOS NUMÉRICOS ENTEROS
Console.WriteLine("=== 1. TIPOS ENTEROS ===");
byte edad = 25;                    // 0 a 255
short temperatura = -50;           // -32,768 a 32,767
int población = 1500000;           // -2,147,483,648 a 2,147,483,647
long numeroGrande = 9223372036854775807;  // Números muy grandes

Console.WriteLine($"Edad (byte): {edad}");
Console.WriteLine($"Temperatura (short): {temperatura}°C");
Console.WriteLine($"Población (int): {población}");
Console.WriteLine($"Número grande (long): {numeroGrande}");

// 2. TIPOS DE DATOS NUMÉRICOS DECIMALES
Console.WriteLine("\n=== 2. TIPOS DECIMALES ===");
float precio = 19.99f;             // Precisión de 7 dígitos
double salario = 2500.50;          // Precisión de 15-17 dígitos
decimal costo = 99.99m;            // Precisión financiera exacta

Console.WriteLine($"Precio (float): {precio}");
Console.WriteLine($"Salario (double): {salario}");
Console.WriteLine($"Costo (decimal): {costo}");

// 3. TIPO BOOLEAN (Verdadero/Falso)
Console.WriteLine("\n=== 3. TIPO BOOLEAN ===");
bool esEstudiante = true;
bool esGraduado = false;

Console.WriteLine($"¿Es estudiante?: {esEstudiante}");
Console.WriteLine($"¿Es graduado?: {esGraduado}");

// 4. TIPO CHARACTER (Un carácter)
Console.WriteLine("\n=== 4. TIPO CHARACTER ===");
char inicial = 'C';
char calificacion = 'A';

Console.WriteLine($"Inicial: {inicial}");
Console.WriteLine($"Calificación: {calificacion}");

// 5. TIPO STRING (Cadena de texto)
Console.WriteLine("\n=== 5. TIPO STRING ===");
string nombre = "César López";
string dirección = "Calle Principal 123";
string correo = "cesar@email.com";

Console.WriteLine($"Nombre: {nombre}");
Console.WriteLine($"Dirección: {dirección}");
Console.WriteLine($"Correo: {correo}");

// 6. VARIABLE CON VAR (Inferencia de tipo)
Console.WriteLine("\n=== 6. INFERENCIA DE TIPO CON VAR ===");
var ciudad = "San José";           // Se infiere como string
var cantidad = 100;                // Se infiere como int
var promedio = 85.5;               // Se infiere como double

Console.WriteLine($"Ciudad (var): {ciudad}");
Console.WriteLine($"Cantidad (var): {cantidad}");
Console.WriteLine($"Promedio (var): {promedio}");

// 7. CONVERSIÓN ENTRE TIPOS
Console.WriteLine("\n=== 7. CONVERSIÓN ENTRE TIPOS ===");
string numeroTexto = "42";
int numeroConvertido = int.Parse(numeroTexto);
Console.WriteLine($"Conversión: \"{numeroTexto}\" (string) → {numeroConvertido} (int)");

double resultado = 10.5 + 5;
Console.WriteLine($"Suma: 10.5 + 5 = {resultado}");

// 8. CONSTANTES (Valores que no cambian)
Console.WriteLine("\n=== 8. CONSTANTES ===");
const double PI = 3.14159;
const string EMPRESA = "Mi Empresa";

Console.WriteLine($"PI (constante): {PI}");
Console.WriteLine($"Empresa (constante): {EMPRESA}");

// 9. OPERACIONES CON VARIABLES
Console.WriteLine("\n=== 9. OPERACIONES CON VARIABLES ===");
int num1 = 20;
int num2 = 8;

Console.WriteLine($"Suma: {num1} + {num2} = {num1 + num2}");
Console.WriteLine($"Resta: {num1} - {num2} = {num1 - num2}");
Console.WriteLine($"Multiplicación: {num1} * {num2} = {num1 * num2}");
Console.WriteLine($"División: {num1} / {num2} = {num1 / num2}");
Console.WriteLine($"Módulo: {num1} % {num2} = {num1 % num2}");

// 10. VARIABLES NULAS (null)
Console.WriteLine("\n=== 10. VARIABLES NULAS ===");
string? textoOpcional = null;      // String anulable
int? numeroOpcional = null;        // Int anulable

Console.WriteLine($"Texto opcional: {textoOpcional ?? "No tiene valor"}");
Console.WriteLine($"Número opcional: {numeroOpcional ?? 0}");

numeroOpcional = 50;
Console.WriteLine($"Número opcional (con valor): {numeroOpcional}");
