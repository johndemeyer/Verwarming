using System;

namespace Verwarming
{
    internal class Program
    {

        static void Main(string[] args)
        {
            decimal defKamerTemp = 15;
            double defGemetenTemp = 21;
            int defVochtigheid = 80;
            double defBar = 2.5;

            decimal setkamerTemp = 0;
            double setGemetenTemp = 0;
            int setVochtigheid = 0;
            double setBar = 0;

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
                    case "setkamertemp":
                        setkamerTemp = SetKamerTemperatuur();
                        kamerTemp = setkamerTemp;
                        break;
                    case "setgemetentemp":
                        setGemetenTemp = SetGemetenTemp();
                        gemetenTemp = setGemetenTemp;
                        break;
                    case "setvochtigheid":
                        setVochtigheid = SetHumidity();
                        vochtigheid = setVochtigheid;
                        break;
                    case "setdruk":
                        setBar = SetDruk();
                        bar = setBar;
                        break;
                    case "info":
                        ShofInfo(setkamerTemp,setGemetenTemp,setVochtigheid,setBar);
                        break;
                    case "run":
                        Werking(setkamerTemp, kamerTemp, setGemetenTemp,setVochtigheid, bar, gemetenTemp, vochtigheid);
                        break;
                    //case "default":
                    //    setDefault(defKamerTemp, defGemetenTemp, defVochtigheid, defBar);
                    //    break;
                    default:
                        ShowError($"The command ' {input} ' is not know by the system.", errorColor);
                        break;
                }
            }
        }

        private static void Werking(decimal setkamerTemp, decimal kamerTemp, double setGemetenTemp, int setVochtigheid, double Bar, double gemetenTemp, int vochtigheid)
        {
            int teller = 0;
            while ((gemetenTemp + 0.3) < decimal.ToDouble(setkamerTemp))
            {
                teller = teller + 1;
                double rest = teller % 2;
                gemetenTemp = gemetenTemp + 0.2;
                if (rest == 0)
                {
                    vochtigheid = vochtigheid - 1;
                }
                Console.WriteLine($"De huidige waarden zijn: ");
                Console.WriteLine($"   Gewenste kamertemperatuur: {kamerTemp}°C");
                Console.WriteLine($"   Gemeten kamertemperatuur: {gemetenTemp}°C");
                Console.WriteLine($"   Vochtigheid: {vochtigheid}%");
                Console.WriteLine($"   Ketel druk: {Bar}%");
                Console.WriteLine($" ");
            }
        }

        //private static void setDefault(decimal defKamerTemp, double defGemetenTemp, int defVochtigheid, double defBar)
        //{
        //    kamerTemp = defKamerTemp;
        //}

        private static void ShofInfo(decimal kamerTemp, double gemetenTemp, int vochtigheid, double bar)
        {
            Console.WriteLine($"De ingegeven waarden zijn: ");
            Console.WriteLine($"   Gewenste kamertemperatuur: {kamerTemp}°C");
            Console.WriteLine($"   Gemeten kamertemperatuur: {gemetenTemp}°C");
            Console.WriteLine($"   Vochtigheid: {vochtigheid}%");
            Console.WriteLine($"   Ketel druk: {bar}%");
        }

        private static double SetDruk()
        {
            while(true)
            {
                int userInput = GetUserInputAsInt("Geef druk in bar (1-4):", "foute ingave");
                if (userInput > 0 && userInput <= 4)
                {
                    return userInput;
                }
                ShowError($"De waarde moet tussen 1 en 4 zijn.");
            }
        }

        private static int SetHumidity()
        {
            while (true)
            {
                int userInput = GetUserInputAsInt("geef vochtigheid (1-1OO):", "foute ingave");
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
                double userInput = GetUserInputAsDouble("geef een gemeten temperatuur (1-30):", "foute ingave");
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
                decimal userInput = GetUserInputAsDecimal("Geef kamertemperatuur (1-30):", "foute ingave");
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
            Console.WriteLine($"{message}");
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