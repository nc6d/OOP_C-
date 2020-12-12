using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_9
{
    class Program
    {
        static void Main()
        {
            int arrayRowsNum = GetData("количество строк массива");
            int arrayColsNum = GetData("количество столбцов массива");
            Array array = new Array(arrayRowsNum, arrayColsNum);
            array.FillArray();
           
            Console.WriteLine($"\n{array}");
            int key = GetData("значение ключа для поиска");
            SearchKey_Print(array.KeySearch(key));
            MinMax_Print(array.MinMaxElements());

            BoolArray boolarray = new BoolArray(arrayRowsNum, arrayColsNum);
            boolarray.FillBoolArray();

            Console.WriteLine($"Стандартный массив: \n{array}");
            boolarray.MyCalculation(array.mainArr, x => x % 13 == 0);           // result1
            Console.WriteLine($"При x % 13 == 0: \n{boolarray}");
            boolarray.MyCalculation(array.mainArr, boolarray.ConditionCheck);   // result2
            Console.WriteLine($"При x > 17:\n{boolarray}");
        }
        static int GetData(string queue)
        {
            int num;
            while (true)
            {

                do
                {
                    Console.Write($"Введите {queue}: ");
                    if (queue == "значение ключа для поиска")
                    {
                        if (int.TryParse(Console.ReadLine(), out num))
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Введено неверное значение! Повторите попытку.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        if (int.TryParse(Console.ReadLine(), out num) && num >= 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Число строк и столбцов массива должны быть положительным целым числом! Повторите попытку.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }


                } while (num <= 0);

                break;

            }

            return num;
        }

        static void SearchKey_Print(Dictionary<int, List<int[]>> KeysList)
        {
            if (KeysList.Count > 0)
            {
                foreach (var k in KeysList.Keys)
                {
                    Console.WriteLine($"\nЧисло {k} найдено в массиве по таким индексам:\r\n ");
                    for (var i = 0; i < KeysList[k].Count; i++)
                    {
                        Console.WriteLine("(" + string.Join(";", KeysList[k][i]) + ")");
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nИскомый ключ не найден");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void MinMax_Print(Dictionary<string, int>[] minMaxDict)
        {
            foreach (Dictionary<string, int> dict in minMaxDict)
            {
                foreach (string k in dict.Keys)
                {
                    Console.WriteLine($"{k}: {dict[k]}");
                }
            }
        }
    }
    class Array
    {
        protected readonly int row;
        protected readonly int col;
        public readonly int[][] mainArr;


        public Array(int row, int col)
        {
            if (row <= 0 || col <= 0) return;
            this.row = row;
            this.col = col;
            mainArr = new int[row][];
        }

        public void FillArray()
        {
            var rnd = new Random();
            for (var i = 0; i < row; i++)
            {
                mainArr[i] = new int[col];
                for (var j = 0; j < col; j++)
                    mainArr[i][j] = rnd.Next(1, 41);
            }
        }


        public Dictionary<int, List<int[]>> KeySearch(int key)
        {
            var indexList = new List<int[]>();
            var dict = new Dictionary<int, List<int[]>>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (mainArr[i][j] == key)
                    {
                        indexList.Add(new[] { i, j });
                    }
                }
            }
            if (indexList.Count > 0)
            {
                dict.Add(key, indexList);
            }

            return dict;
        }

        public Dictionary<string, int>[] MinMaxElements()
        {
            var dict = new Dictionary<string, int>[mainArr.Length];

            for (var i = 0; i < row; i++)
            {
                var minElem = mainArr[i].Min();
                var maxElem = mainArr[i].Max();
                dict[i] = new Dictionary<string, int>()
                {
                    ["\nСтрока"] = i,
                    ["Минимальный элемент"] = minElem,
                    ["Максимальный элемент"] = maxElem,
                    ["Произведение минимального и максимального элементов"] = minElem * maxElem
                };
            }
            return dict;
        }
        public override string ToString()
        {
            var strArr = new string[mainArr.Length];
            var res = new string[mainArr.Length][];
            for (var i = 0; i < mainArr.Length; i++)
            {
                res[i] = new string[mainArr[i].Length];

                for (int j = 0; j < mainArr[i].Length; j++)
                    res[i][j] = Convert.ToString(mainArr[i][j]);

                strArr[i] = "\t" + string.Join(" ", res[i]);
            }
            

            return string.Join("\n", strArr);
        }

    }
    class BoolArray : Array
    {
        private readonly bool[][] boolArr;
        public BoolArray(int row, int col) : base(row, col)
        {
            if (row > 0 && col > 0)
                boolArr = new bool[row][];
        }
        public void FillBoolArray()
        {
            for (int i = 0; i < row; i++)
            {
                boolArr[i] = new bool[col];
                for (int j = 0; j < col; j++)
                    boolArr[i][j] = false;
            }
        }

        public delegate bool IsEqual(int x);

        public bool ConditionCheck(int x)
        {
            if (x > 17)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MyCalculation(int[][] arr, IsEqual func)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    boolArr[i][j] = func(arr[i][j]) ? boolArr[i][j] == false : boolArr[i][j] == true;

            }
        }
        public override string ToString()
        {
            string[] strBoolArr = new string[boolArr.Length];
            string[][] res2 = new string[boolArr.Length][];
            for (int i = 0; i < boolArr.Length; i++)
            {
                res2[i] = new string[boolArr[i].Length];

                for (int j = 0; j < boolArr[i].Length; j++)
                    res2[i][j] = Convert.ToString(boolArr[i][j]);

                strBoolArr[i] = "\t" + string.Join(" ", res2[i]);
            }

            return string.Join("\n", strBoolArr);
        }
    }
}

