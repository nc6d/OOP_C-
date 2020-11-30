using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_6
{
    class Task6
    {
        static void Main()
        {
            Array array = new Array(GetData("количество строк массива"), GetData("количество столбцов массива"));
            

            array.FillArray();
            array.FillBoolArray();
            //Console.WriteLine($"\n{array}");
            int key = GetData("значение ключа для поиска");
            SearchKey_Print(array.KeySearch(key));
            MinMax_Print(array.MinMaxElements());
            array.ConditionCheck();
            Console.WriteLine($"\n{array}");
            Console.WriteLine($"\n{array}");
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
                foreach (int k in KeysList.Keys)
                {
                    Console.WriteLine($"\nЧисло {k} найдено в массиве по таким индексам:\r\n ");
                    for (int i = 0; i < KeysList[k].Count; i++)
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
}
class Array
{
    private readonly int row;
    private readonly int col;
    private readonly int[][] mainArr;
    private readonly bool[][] boolArr;


    public Array(int row, int col)
    {
        if (row > 0 && col > 0)
        {
            this.row = row;
            this.col = col;
            mainArr = new int[row][];
            boolArr = new bool[row][];
        }
    }

    public void FillArray()
    {
        Random rnd = new Random();
        for (int i = 0; i < row; i++)
        {
            mainArr[i] = new int[col];
            for (int j = 0; j < col; j++)
                mainArr[i][j] = rnd.Next(1, 41);
        }
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

    public Dictionary<int, List<int[]>> KeySearch(int key)
    {
        List<int[]> indexList = new List<int[]>();
        Dictionary<int, List<int[]>> dict = new Dictionary<int, List<int[]>>();

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
        Dictionary<string, int>[] dict = new Dictionary<string, int>[mainArr.Length];

        for (int i = 0; i < row; i++)
        {
            int minElem = mainArr[i].Min();
            int maxElem = mainArr[i].Max();
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

    public delegate bool IsEqual(int x);

    public void ConditionCheck()
    {

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                boolArr[i][j] = mainArr[i][j] > 17 ? boolArr[i][j] == true : boolArr[i][j] == false;
            }
        }
    }


    public override string ToString()
    {
        string[] strArr = new string[mainArr.Length];
        string[] strBoolArr = new string[boolArr.Length];
        string[][] res = new string[mainArr.Length][];
        string[][] res2 = new string[boolArr.Length][];
        for (int i = 0; i < mainArr.Length; i++)
        {
            res[i] = new string[mainArr[i].Length];
            for (int j = 0; j < mainArr[i].Length; j++)
            {
                res[i][j] = Convert.ToString(mainArr[i][j]);
            }
            strArr[i] = "\t" + string.Join(" ", res[i]);
        }
        for (int i = 0; i < boolArr.Length; i++)
        {
            res2[i] = new string[boolArr[i].Length];
            for (int j = 0; j < boolArr[i].Length; j++)
            {
                res2[i][j] = Convert.ToString(boolArr[i][j]);
            }
            strBoolArr[i] = "\t" + string.Join(" ", res2[i]);
        }

        return string.Join("\n", strBoolArr);
    }
    
}