using System;
using System.IO;
using System.Text;

namespace Task4
{
    public static class Program
    {
        static void Main()
        {
            GetData(out double radius, out double x, out double y);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nCircle 1:");
            Circle circle = new Circle(radius, new Point(x, y));
            Console.WriteLine(circle);
            Console.WriteLine($"Square: {circle.Square}\nLength: {circle.Length}\n");

            Console.ForegroundColor = ConsoleColor.White;
            ZoomFactorGet(out double zoom);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nCircle 2:");
            Circle circle1 = new Circle(circle.Radius * zoom, new Point(x, y));
            Console.WriteLine(circle1);
            Console.WriteLine($"Square: {circle1.Square}\nLength: {circle1.Length}\n");

            Console.ReadKey();
        }


        static void GetData(out double radius, out double x, out double y)
        {
            const string UNDERLINE = "\x1B[4m";
            const string RESET = "\x1B[0m";
            Console.Write($"Press {UNDERLINE}F{RESET} to use file data input or press {UNDERLINE}ANY KEY{RESET} to input from keyboard: ");
            if (Console.ReadKey().Key == ConsoleKey.F)
            {

                string path = @"TextFile1.txt";
                using StreamReader sr = new StreamReader(path, Encoding.Default);

                string temp0 = sr.ReadLine();
                while (!double.TryParse(temp0, out radius) || radius <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nRadius value is not correct, check and rewrite your txt file!");
                    System.Environment.Exit(0);
                }
                string temp1 = sr.ReadLine();
                while (!double.TryParse(temp1, out x))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nAxis X value is not correct, check and rewrite your txt file!");
                    Environment.Exit(0);
                }
                string temp2 = sr.ReadLine();
                while (!double.TryParse(temp2, out y))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nAxis Y value is not correct, check and rewrite your txt file!");
                    Environment.Exit(0);
                }

            }
            else
            {
                Console.Write("\nEnter radius: ");
                var temp0 = Console.ReadLine();
                while (!double.TryParse(temp0, out radius) || radius <= 0)
                {
                    Console.WriteLine("Radius value is not correct, try again");
                    temp0 = Console.ReadLine();
                }
                Console.Write("Enter axis X of center: ");
                var temp1 = Console.ReadLine();
                while (!double.TryParse(temp1, out x))
                {
                    Console.WriteLine("Axis X value is not correct, try again");
                }
                Console.Write("Enter axis Y of center: ");
                var temp2 = Console.ReadLine();
                while (!double.TryParse(temp2, out y))
                {
                    Console.WriteLine("Axis Y value is not correct, try again");
                    temp2 = Console.ReadLine();
                }
            }
        }

        static void ZoomFactorGet(out double zoom)
        {
            Console.Write("Enter zoom-factor: ");
            var temp = Console.ReadLine();
            while (!double.TryParse(temp, out zoom) || zoom <= 0)
            {
                Console.WriteLine("Invalid value, try again");
                temp = Console.ReadLine();
            }
        }
    }
}

public class Circle
{
    public double Radius { get; set; }
    public Point Center { get; set; }
    public Circle(double radius, Point center)
    {
        Radius = radius;
        Center = center;
    }

    public double Square
    {
        get { return Math.PI * Math.Pow(Radius, 2); }
    }

    public double Length
    {
        get { return Math.PI * 2 * Radius; }
    }
    public override string ToString()
    {
        return string.Format($"Radius: {Radius}\nCenter coordinates: {Center}");
    }
}

public class Point
{
    public double X { get; private set; }
    public double Y { get; private set; }
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return string.Format($"({X}, {Y})");
    }
}
