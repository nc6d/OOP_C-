using System;

namespace Task8
{
    class Task8
    {
        static void Main(string[] args)
        {
            MyComplex A = new MyComplex(1, 1);
            MyComplex B = new MyComplex();
            MyComplex C = new MyComplex(1);
            MyComplex D = new MyComplex();

            D.InputFromTerminal();

            C = A + B;
            C = A + 10.5;
            C = 10.5 + A;
            D = -C;
            C = A + B + C + D;
            C = A = B = C;

            Console.WriteLine($"\nA = {A}, B = {B}, C = {C}, D = {D}" + $"");
            Console.WriteLine(
                $"\nRe(A) = {A["Re"]}, Im(A) = {A["Im"]}\n" +
                $"Re(B) = {B["Re"]}, Im(B) = {B["Im"]}\n" +
                $"Re(C) = {C["Re"]}, Im(C) = {C["Im"]} \n" +
                $"Re(D) = {D["Re"]}, Im(D) = {D["Im"]}\n");

        }
    }
    class MyComplex
    {
        private double Re, Im;

        public MyComplex(double initRe = 0, double initIm = 0)
        {
            Re = initRe;
            Im = initIm;
        }

        public static MyComplex operator +(MyComplex a, MyComplex b) => new MyComplex(a.Re + b.Re, a.Im + b.Im);
        public static MyComplex operator +(MyComplex a, double b) => new MyComplex(a.Re + b, a.Im);
        public static MyComplex operator +(double a, MyComplex b) => new MyComplex(a + b.Re, b.Im);
        public static MyComplex operator -(MyComplex a) => new MyComplex(-a.Re, -a.Im);
        public static MyComplex operator /(MyComplex a, MyComplex b)
        {
            MyComplex x = new MyComplex();
            x.Re = a.Re * b.Re;
            x.Im = a.Im * b.Im;
            //x = a.Re * b.Re, a.Im * b.Im;
            return x;
        }

        public void InputFromTerminal()
        {
            Console.Write("Введите действительое число: ");
            var temp1 = Console.ReadLine();
            while (!double.TryParse(temp1, out Re))
            {
                Console.WriteLine("Действительое число задано неверно, попробуйте ещё раз");
                temp1 = Console.ReadLine();
            }
            Console.Write("Введите мнимое число: ");
            var temp2 = Console.ReadLine();
            while (!double.TryParse(temp2, out Im))
            {
                Console.WriteLine("Мнимое число задано неверно, попробуйте ещё раз");
                temp2 = Console.ReadLine();
            }
        }
        public string this[string index] => index switch
        {
            "Re" => Re.ToString(),
            "Im" => Im + "i",
            _ => " ",
        };

        public override string ToString()
        {
            if (Im > 0)
                return $"{Re}+{Im}i";
            else if (Im == 0)
                return $"{Re}";
            else
                return $"{Re}{Im}i";
        }
    }


}
