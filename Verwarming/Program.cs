using System;
using System.Data;

namespace Verwarming
{
    internal class Program
    {

        static void Main(string[] args)
        {
            decimal defKamerTemp = 21;
            double defGemetenTemp = 15;
            int defVochtigheid = 80;
            double defBar = 2.5;

            decimal setkamerTemp = defKamerTemp;
            double setGemetenTemp = defGemetenTemp;
            int setVochtigheid = defVochtigheid;
            double setBar = defBar;

            decimal kamerTemp = defKamerTemp;
            double gemetenTemp = defGemetenTemp;
            int vochtigheid = defVochtigheid;
            double bar = defBar;
            //double tempInput = 21;

            ConsoleColor errorColor = ConsoleColor.Red;

            bool isRunning = true;

            while (isRunning)
            {
                string input = Console.ReadLine() ?? "";

                switch (input.ToLower())
                {
                    case "set":
                        setkamerTemp = SetKamerTemperatuur();
                        kamerTemp = setkamerTemp;
                        setGemetenTemp = SetGemetenTemp();
                        gemetenTemp = setGemetenTemp;
                        setVochtigheid = Setvochtigheid();
                        vochtigheid = setVochtigheid;
                        setBar = SetDruk();
                        bar = setBar;
                        break;
                    case "info":
                        ShofInfo(setkamerTemp,setGemetenTemp,setVochtigheid,setBar);
                        break;
                    case "run":
                        Werking(setkamerTemp, kamerTemp, bar, gemetenTemp, vochtigheid);
                        break;
                    case "default":
                        kamerTemp = defKamerTemp;
                        gemetenTemp = defGemetenTemp;
                        vochtigheid = defVochtigheid;
                        bar = defBar;
                        break;
                    default:
                        ShowError($"The command ' {input} ' is not know by the system.", errorColor);
                        break;
                }
            }
        }


        private static void Werking(decimal setkamerTemp, decimal kamerTemp, double Bar, double gemetenTemp, int vochtigheid)
        {
            if (Bar <= 3 && Bar > 2)
            {
                int teller = 0;
                while (gemetenTemp < (decimal.ToDouble(setkamerTemp) + 0.3))
                {
                    teller = teller + 1;
                    double rest = teller % 2;
                    if (rest == 0)
                    {
                        vochtigheid = vochtigheid - 1;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"De huidige waarden zijn: ");
                    Console.WriteLine($"   Gewenste kamertemperatuur: {kamerTemp}°C / Gemeten kamertemperatuur: {gemetenTemp.ToString("f2")}°C / Vochtigheid: {vochtigheid}% / Ketel druk: {Bar}%");
                    Console.WriteLine($" ");
                    gemetenTemp = gemetenTemp + 0.2;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   Gewenste kamertemperatuur: {kamerTemp}°C is bereikt");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                ShowError($" De druk in de ketel is; {Bar}bar, dit is niet binnen de normen (2-3 bar) ");
            }
            
        }


        private static void ShofInfo(decimal kamerTemp, double gemetenTemp, int vochtigheid, double bar)
        {
            Console.WriteLine($"De ingegeven test waarden zijn: ");
            Console.WriteLine($"   Gewenste kamertemperatuur: {kamerTemp}°C");
            Console.WriteLine($"   Gemeten kamertemperatuur: {gemetenTemp}°C");
            Console.WriteLine($"   Vochtigheid: {vochtigheid}%");
            Console.WriteLine($"   Ketel druk: {bar}%");
        }

        private static double SetDruk()
        {
            while(true)
            {
                int userInput = GetUserInputAsInt("Geef druk in bar (1-4)", "foute ingave");
                if (userInput > 0 && userInput <= 4)
                {
                    return userInput;
                }
                ShowError($"De waarde moet tussen 1 en 4 zijn.");
            }
        }

        private static int Setvochtigheid()
        {
            while (true)
            {
                int userInput = GetUserInputAsInt("geef vochtigheid (1-1OO)", "foute ingave");
                if (userInput > 0 && userInput <= 100)
                {
                    return userInput;
                }
                ShowError($"De waarde moet tussen 1 en 100 zijn.");
            }
        }

        private static double SetGemetenTemp()
        {
            while (true)
            {
                double userInput = GetUserInputAsDouble("geef een gemeten temperatuur (1-30)", "foute ingave");
                if (userInput > 0 && userInput <= 30)
                {
                    return userInput;
                }
                ShowError($"De waarde moet tussen 1 en 30 zijn.");
            }
        }

        private static decimal SetKamerTemperatuur()
        {
            while (true)
            {
                decimal userInput = GetUserInputAsDecimal("Geef kamertemperatuur (1-30)", "foute ingave");
                if (userInput > 0 && userInput <= 30)
                {
                    return userInput;
                }
                ShowError($"De waarde moet tussen 1 en 30 zijn.");
            }
        }



        static decimal GetUserInputAsDecimal(string msg = "Give a decimal", string errorMsg = "Error")
        {
            decimal userinput = 0;
            Console.Write($"{msg} : ");
            string input = Console.ReadLine() ?? "";
            if (decimal.TryParse(input, out userinput))
            {
                return userinput;
            }
            ShowError(errorMsg);
            return 0;
        }

        private static double GetUserInputAsDouble(string message, string errorMessage)
        {
            double userInput = 0;
            Console.Write($"{message} : ");
            string input = Console.ReadLine() ?? "";
            if (double.TryParse(input, out userInput))
            {
                return userInput;
            }
            Console.WriteLine($"{errorMessage}");
            return 0;
        }

        static int GetUserInputAsInt(string msg = "Give a Int", string errorMsg = "Error")
        {
            int userinput = 0;
            Console.Write($"{msg} : ");
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out userinput))
            {
                return userinput;
            }
            ShowError(errorMsg);
            return 0;
        }

        // door het argument: errorcolor al een waarde tegeven, is dit argument optioneel 
        static void ShowError(string message, ConsoleColor errorColor = ConsoleColor.Red)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = errorColor;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

    }
}