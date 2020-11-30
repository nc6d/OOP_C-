using System;
using System.IO;
using System.Linq;

namespace _7
{
    class Task7 
    {
        static void Main()
        {
            double[][] data = GetData(Constants.path);
            WeatherParametersDay[] weatherParametersDays = new WeatherParametersDay[data.Length];
            for (int i = 0; i < data.Length; i++)
                weatherParametersDays[i] = new WeatherParametersDay(data[i][0], data[i][1], data[i][2], data[i][3], (int)data[i][4]);

            WeatherDays weatherDays = new WeatherDays(weatherParametersDays);
            Console.WriteLine($"\n\nАктуальная информация за СЕНТЯБРЬ (30 дней):\n" +
                $"Количество солнечных дней: {weatherDays.CountSunnyDays()}\n" +
                $"Количество дней, когда не было дождя или грозы: {weatherDays.CountNoRainDays()}\n" +
                $"Минимальное количество осадков: {weatherDays.MinMaxPrecipitation().Item1}\n" +
                $"Максимальное количество осадков: {weatherDays.MinMaxPrecipitation().Item2}\n");
            
        }
        private static double[][] GetData(string path)
        {
            string[] getStrings = new string[Constants.DAYS_IN_MONTH];
            bool exit = false;
            Console.Write($"Нажмите клавишу F для ввода данных из файла {path} или любую другую клавишу для ручного ввода с консоли: ");
            while (!exit)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F:
                        getStrings = ReadFile(Constants.path);
                        exit = true;
                        break;
                    default:
                        ReadConsole(out getStrings);
                        exit = true;
                        break;
                }
            }

            return ConvertArray(getStrings);
        }

        private static string[] ReadFile(string path) => File.ReadAllLines(path);
        private static void ReadConsole(out string[] dataStrings)
        {
            Console.WriteLine("\nВведите данные для каждого дня в отдельной строке через пробел.\n" +
                "ВАЖНО: несоблюдение правил ввода данных повлечет за собой сбой программы.");
            dataStrings = new string[Constants.DAYS_IN_MONTH];

            for (int line = 0; line < Constants.DAYS_IN_MONTH; line++)
            {
                while (true)
                {
                    Console.WriteLine($"Строка {line}:");
                    dataStrings[line] = Console.ReadLine();
                    try
                    {
                        if (dataStrings[line] == "")
                            throw new NullReferenceException();
                        break;
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        private static double[][] ConvertArray(string[] lines)
        {
            double[][] data = new double[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] linesSplit = lines[i].Split();
                data[i] = (linesSplit.Length == 4) ? new double[linesSplit.Length + 1] : new double[linesSplit.Length];
                for (int j = 0; j < linesSplit.Length; j++)
                {
                    double num = 0;
                    try
                    {
                        num = Convert.ToDouble(linesSplit[j]);  
                    }
                    catch (Exception e) when (e is FormatException || e is InvalidCastException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nСлучилась ошибка при конвертации данных. Проверьте входной файл!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                    data[i][j] = num;
                }
            }

            return data;
        }

    }

    static class Constants
    {
        public static int DAYS_IN_MONTH = 30;
        public static string path = @"data.txt";
    }

    enum WeatherType
    {
        NotDefined,
        Rain,
        ShortTermRain,
        Thunder,
        Snow,
        Fog,
        Sunny,
        Cloudy
    }
    class WeatherParametersDay
    {
        public WeatherType WeatherType { get; private set; }
        public double AvgDayTemp { get; private set; }
        public double AvgNightTemp { get; private set; }
        public double AvgAtmPressure { get; private set; }
        public double Precipitation { get; private set; }
        public WeatherParametersDay(double avgDayTemp, double avgNightTemp, double avgAtmPressure, double precipitation, int weatherType)
        {
            if (Enumerable.Range(0, 7).Contains(weatherType))
            {
                WeatherType = (WeatherType)weatherType;
                AvgDayTemp = avgDayTemp;
                AvgNightTemp = avgNightTemp;
                AvgAtmPressure = avgAtmPressure;
                Precipitation = precipitation;
            }
        }
    }

    class WeatherDays
    {
        private readonly WeatherParametersDay[] dataWeatherArray;

        public WeatherDays(WeatherParametersDay[] dataWeatherArray) => this.dataWeatherArray = dataWeatherArray;
        private int CountDays(params WeatherType[] weatherType)
        {
            int daysInMonth = 0;
            foreach (WeatherParametersDay day in dataWeatherArray)
                daysInMonth += weatherType.Contains(day.WeatherType) ? 1 : 0;

            return daysInMonth;
        }
        public int CountNoRainDays() => CountDays(WeatherType.Snow, WeatherType.Fog, WeatherType.Cloudy, WeatherType.Sunny);
        public int CountSunnyDays() => CountDays(WeatherType.Sunny);
       

        public Tuple<double, double> MinMaxPrecipitation()
        {
            double[] values = new double[Constants.DAYS_IN_MONTH];
            int counter = 0;
            foreach (WeatherParametersDay day in dataWeatherArray)
            {
                values[counter] = day.Precipitation;
                counter++;
            }
            double maxPrec = values[0];
            double minPrec = values[0];
            for (int val = 0; val < values.Length; val++)
            {
                if (values[val] < minPrec)
                    minPrec = values[val];

                else if (values[val] >= maxPrec)
                    maxPrec = values[val];
            }
            return new Tuple<double, double>(minPrec, maxPrec);

        }
    }

    

    
}