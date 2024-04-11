using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personas
{
    public class Personas
    {
        public string nombre = "";
        public int edad = 0;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Personas persona1, persona2, persona3;
            persona1 = new Personas();
            persona2 = new Personas();
            persona3 = new Personas();

            Console.Write("Ingrese el nombre de la persona 1:");
            persona1.nombre = Console.ReadLine();
            Console.Write("Ingrese el nombre de la persona 2:");
            persona2.nombre = Console.ReadLine();
            Console.Write("Ingrese el nombre de la persona 3:");
            persona3.nombre = Console.ReadLine();
            Console.Write("Ingrese la edad de la persona 1:");
            persona1.edad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese la edad de la persona 2:");
            persona2.edad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese la edad de la persona 3:");
            persona3.edad = int.Parse(Console.ReadLine());
            if (persona1.edad == persona2.edad && persona1.edad == persona3.edad)
            {
                Console.WriteLine("Las tres personas tienen la misma edad");
            }
            else { 
                if(persona1.edad == persona2.edad || persona1.edad == persona3.edad || persona2.edad == persona3.edad)
                {
                    Console.WriteLine("Dos personas tienen la misma edad");
                }
                else { 
            if(persona1.edad > persona2.edad && persona1.edad > persona3.edad) {
                Console.WriteLine("{0} es el mayor de los tres", persona1.nombre);
                if (persona2.edad > persona3.edad)
                {
                    Console.WriteLine("{0} es el menor de los tres", persona3.nombre);
                }
                else
                {
                    Console.WriteLine("{0} es el menor de los tres", persona2.nombre);
                }
            }
            else
            {
                if(persona2.edad > persona3.edad)
                {
                    Console.WriteLine("{0} es el mayor de los tres", persona2.nombre);
                    if (persona1.edad > persona3.edad)
                    {
                        Console.WriteLine("{0} es el menor de los tres", persona3.nombre);
                    }
                    else
                    {
                        Console.WriteLine("{0} es el menor de los tres", persona1.nombre);
                    }
                }
                else
                {
                    Console.WriteLine("{0} es el mayor de los tres", persona3.nombre);
                    if (persona1.edad > persona2.edad)
                    {
                        Console.WriteLine("{0} es el menor de los tres", persona2.nombre);
                    }
                    else
                    {
                        Console.WriteLine("{0} es el menor de los tres", persona1.nombre);
                    }

                }
            }
                }
            }
            Console.ReadKey();
        }
    }
}
