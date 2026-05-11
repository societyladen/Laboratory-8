using System;
using System.IO;

namespace Planner
{
    internal class CheckRead
    {
        public static int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    int result = Convert.ToInt32(input);
                    if (result >= 0)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Число должно быть неотрицательным. Повторите ввод:");
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Введите целое число. Повторите ввод:");
                }
            }
        }

        public static int ReadPriority()
        {
            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    int result = Convert.ToInt32(input);
                    if (result >= 1 && result <= 3)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Приоритет должен быть 1, 2 или 3. Повторите ввод:");
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Введите целое число. Повторите ввод:");
                }
            }
        }

        public static double ReadDoubleScore()
        {
            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    double result = Convert.ToDouble(input);
                    if (result >= 0.1 && result <= 24)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Длительность должна быть от 0.1 до 24 часов. Повторите ввод:");
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Введите число. Повторите ввод:");
                }
            }
        }

        public static string ReadPath()
        {
            while (true)
            {
                string path = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(path))
                {
                    try
                    {
                        string dir = Path.GetDirectoryName(path);
                        if (string.IsNullOrEmpty(dir))
                            dir = Environment.CurrentDirectory;
                        if (Directory.Exists(dir))
                            return path;
                    }
                    catch { }
                }
                Console.WriteLine("Ошибка! Введите корректный путь. Повторите ввод:");
            }
        }

        public static string ReadString()
        {
            string str = "";
            while (string.IsNullOrWhiteSpace(str))
            {
                str = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(str))
                {
                    Console.WriteLine("Ошибка! Название не может быть пустым. Повторите ввод:");
                }
            }
            return str;
        }

        public static bool ReadBool()
        {
            while (true)
            {
                string input = Console.ReadLine()?.ToLower();
                if (input == "да" || input == "yes" || input == "true" || input == "1")
                    return true;
                if (input == "нет" || input == "no" || input == "false" || input == "0")
                    return false;
                Console.WriteLine("Ошибка! Введите 'да' или 'нет'. Повторите ввод:");
            }
        }
    }
}