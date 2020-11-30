using System;
using System.IO;


namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Paths to text files
            string path_x = @"x.txt";
            string path_y = @"y.txt";

            // Creating empty arrays    
            double[] arr_x = FilesToGetArrays(path_x);
            double[] arr_y = FilesToGetArrays(path_y);
            double[] arr_z = new double[arr_x.Length];

            Console.WriteLine($"Array X: {string.Join(' ', arr_x)}");
            Console.WriteLine($"Array Y: {string.Join(' ', arr_y)}");

            SubstractionOfArrayX(ref arr_x);
            SettingOfArrayZ(arr_x, arr_y, ref arr_z);

            Console.WriteLine($"Array X (after changes): {string.Join(' ', arr_x)}");
            Console.WriteLine($"Array Z: {string.Join(' ', arr_z)}");
        }
        static double[] FilesToGetArrays(string link)
        {
            string[] fileTxt = File.ReadAllText(link).Split(", ");
            double[] tempArr = new double[fileTxt.Length];
            for (int i = 0; i < fileTxt.Length; i++)
            {
                try
                {
                    tempArr[i] = Convert.ToDouble(fileTxt[i]);

                }
                catch (FileNotFoundException FNF)
                {
                    Console.WriteLine(FNF.Message);
                    Environment.Exit(1);
                }
                catch (FormatException FE)
                {
                    Console.WriteLine(FE.Message);
                    Environment.Exit(1);
                }
                catch (InvalidCastException ICE)
                {
                    Console.WriteLine(ICE.Message);
                    Environment.Exit(1);
                }
            }
            return tempArr;
        }

        static void SubstractionOfArrayX(ref double[] arr_x)
        {
            for (int i = 0; i < arr_x.Length; i++)
            {
                if (arr_x[i] % 5 == 0)
                {
                    arr_x[i] = arr_x[i] - 8;
                }
            }
        }

        static void SettingOfArrayZ(double[] arr_x, double[] arr_y, ref double[] arr_z)
        {
            for (int i = 0; i < arr_x.Length; i++)
            {
                arr_z[i] = (arr_x[i] * arr_x[i] + arr_y[i] * arr_y[i]) / 2;
            }
        }

    }
}
