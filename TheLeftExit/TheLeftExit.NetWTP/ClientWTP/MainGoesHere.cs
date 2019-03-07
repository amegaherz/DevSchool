using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClientWTP
{
    class MainGoesHere
    {
        public static void Main(string[] args)
        {
            // Если запускаем лениво и/или из VS, задаём параметры в интерактивном режиме.
            if(args.Length == 0)
            {
                args = new string[3];
                Console.Write("Что используем для конвертации (-mm или -word): ");
                args[0] = Console.ReadLine();
                Console.Write("Входной файл: ");
                args[1] = Console.ReadLine();
                Console.Write("Выходной файл: ");
                args[2] = Console.ReadLine();
            }

            // Чтоб не было исключений.
            if (args.Length != 3)
            {
                Console.WriteLine("Неизвестный формат ввода.");
                return;
            }
            if (args[0] != "-mm" && args[0] != "-word")
            {
                Console.WriteLine("Неизвестный режим работы.");
                return;
            }
            if (!File.Exists(args[1]))
            {
                Console.WriteLine("Входной файл не найден.");
                return;
            }

            byte[] inbin = File.ReadAllBytes(args[1]);
            WTP.WTPResponse response = null;

            Console.WriteLine("Операция выполняется...");

            using (var client = new WTP.Service1Client())
                switch (args[0].ToLower())
                {
                    case "-mm":
                        response = client.ConvertWithMM(inbin);
                        break;
                    case "-word":
                        response = client.ConvertWithWord(inbin);
                        break;
                }

            if (response.success)
            {
                try
                {
                    File.WriteAllBytes(args[2], response.binary);
                }
                catch
                {
                    Console.WriteLine("Нет доступа к рабочей папке. Нажмите любую клавишу...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Операция завершена. Нажмите любую клавишу...");
            }
            else
            {
                Console.WriteLine($"Операция прервана. Нажмите любую клавишу...\n{response.header}");
            }

            Console.ReadKey();
            return;
        }
    }
}
