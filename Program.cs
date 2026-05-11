using System;
using System.Collections.Generic;

namespace Planner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int yourChoice = 0;
            string path = "";

            Console.WriteLine("ПЛАНИРОВЩИК\n");
            Console.Write("Введите путь к бинарному файлу: ");
            path = CheckRead.ReadPath();

            Database.FillBinaryFile(path);
            List<Database> tasks = Database.ReadFilePath(path);

            while (true)
            {
                Console.WriteLine("\nПриветствую, выберите =>");
                Console.WriteLine("1. Просмотр всех задач");
                Console.WriteLine("2. Удаление задачи (по номеру)");
                Console.WriteLine("3. Добавление новой задачи");
                Console.WriteLine("\nLINQ-запросы");
                Console.WriteLine("4. Получить список задач с высоким приоритетом (приоритет 3)");
                Console.WriteLine("5. Получить список НЕвыполненных задач");
                Console.WriteLine("6. Получить ОБЩУЮ длительность всех задач");
                Console.WriteLine("7. Получить КОЛИЧЕСТВО выполненных задач");
                Console.WriteLine("\n8. Выход");
                Console.Write("\nВаш выбор: ");

                yourChoice = CheckRead.ReadInt();

                switch (yourChoice)
                {
                    case 1:
                        Database.PrintAll(tasks);
                        break;
                    case 2:
                        Database.DeleteTaskByIndexInteractive(tasks);
                        break;
                    case 3:
                        Database.AddTask(tasks);
                        break;
                    case 4:
                        Database.GetHighPriorityTasks(tasks);
                        break;
                    case 5:
                        Database.GetUncompletedTasks(tasks);
                        break;
                    case 6:
                        Database.GetTotalDuration(tasks);
                        break;
                    case 7:
                        Database.CountCompletedTasks(tasks);
                        break;
                    case 8:
                        Console.WriteLine("\nДо свидания!");
                        break;
                    default:
                        Console.WriteLine("\nОшибка: нет такого пункта меню.");
                        break;
                }

                if (yourChoice == 8)
                    return;
            }
        }
    }
}