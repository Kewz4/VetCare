using System;
using System.Runtime.Intrinsics.X86;

namespace Clase3
{
    class Program
    {
        static void Main()
        {
            miMetodo();
            //Crear varible para almacenar el salario base
            float salario;
            //llamando al método para calcular el salario base y almacenando el resultado en la variable salario
            salario = calcularSalarioBase(40, 15.00f);
            Console.WriteLine($"Salario base calculado: {salario}");


        }   
       private static float calcularSalarioBase(int horasTrabajadas, float tarifaPorHora)
        {
            //variable local para almacenar el resultado del cálculo
            float resultado;
            //operacion para calcular el salario base
            resultado = horasTrabajadas * tarifaPorHora;
            //retorna el resultado del cálculo
            return resultado; 
        }



        public static void miMetodo(){ Console.WriteLine("¡Hola desde mi método!");}
        

 
    }
 }
