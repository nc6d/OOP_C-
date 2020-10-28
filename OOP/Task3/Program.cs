using System;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            double b;
            long c;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a: ");
                    a = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter correct value!");
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter b: ");
                    b = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter correct value!");
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter c: ");
                    c = Convert.ToInt64(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Enter correct value!");
                }
            }
            Console.WriteLine($" Number A: {a}\n Number B: {b}\n Number C: {c}");
            Console.ReadKey();
        }
    }
}