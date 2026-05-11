using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Planner
{
    internal class Database
    {
        private int _id;
        private string _title;
        private int _priority;
        private double _duration;
        private bool _isCompleted;

        public Database(int id, string title, int priority, double duration, bool isCompleted)
        {
            this._id = id;
            this._title = title;
            this._priority = priority;
            this._duration = duration;
            this._isCompleted = isCompleted;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Title
        {
            get { return _title; }
        }

        public int Priority
        {
            get { return _priority; }
        }

        public double Duration
        {
            get { return _duration; }
        }

        public bool IsCompleted
        {
            get { return _isCompleted; }
        }

        public static void FillBinaryFile(string path)
        {
            string[] titles = { "Сделать отчёт", "Позвонить клиенту",
                "Написать код", "Сходить в спортзал", "Купить продукты" };
            int[] priorities = { 3, 2, 3, 1, 2 };
            double[] durations = { 2.5, 0.5, 4.0, 1.0, 1.5 };
            bool[] completions = { false, true, false, false, true };

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    writer.Write(i + 1);
                    writer.Write(titles[i]);
                    writer.Write(priorities[i]);
                    writer.Write(durations[i]);
                    writer.Write(completions[i]);
                }
            }
            Console.WriteLine("Бинарный файл создан: {0}", path);
        }

        public static List<Database> ReadFilePath(string path)
        {
            int id = 0;
            string title = "";
            int priority = 0;
            double duration = 0;
            bool isCompleted = false;
            List<Database> tasks = new List<Database>();

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
                return tasks;
            }
            try
            {
                using (BinaryReader readerFile = new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
                {
                    while (readerFile.BaseStream.Position < readerFile.BaseStream.Length)
                    {
                        id = readerFile.ReadInt32();
                        title = readerFile.ReadString();
                        priority = readerFile.ReadInt32();
                        duration = readerFile.ReadDouble();
                        isCompleted = readerFile.ReadBoolean();

                        tasks.Add(new Database(id, title, priority, duration, isCompleted));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
            }
            return tasks;
        }

        public static void PrintAll(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            Console.WriteLine("| \tID     \tНазвание         \tПриоритет \tЧасы  \tСтатус        |");
            string priorityText = "";
            string statusText = "";

            foreach (Database t in tasks)
            {
                if (t.Priority == 3)
                {
                    priorityText = "Высокий";
                }
                else if (t.Priority == 2)
                {
                    priorityText = "Средний";
                }
                else
                {
                    priorityText = "Низкий";
                }

                if (t.IsCompleted == true)
                {
                    statusText = "Выполнено";
                }
                else
                {
                    statusText = "Не выполнено";
                }

                Console.WriteLine("| \t{0}  \t{1}  \t{2}  \t{3}  \t{4}",
                    t.Id, t.Title, priorityText, t.Duration, statusText);
            }
            Console.WriteLine("\nВсего записей: {0}", tasks.Count);
        }

        public static bool DeleteTaskByIndex(List<Database> tasks, int index)
        {
            if (index < 0 || index >= tasks.Count)
            {
                Console.WriteLine("\nОшибка: Индекс {0} вне диапазона (0-{1})!", index, tasks.Count - 1);
                return false;
            }

            Database removed = tasks[index];
            tasks.RemoveAt(index);
            Console.WriteLine("\nЗадача '{0}' успешно удалена!", removed.Title);
            return true;
        }

        public static void DeleteTaskByIndexInteractive(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }

            Console.WriteLine("\nУдаление: ");
            Console.WriteLine("Текущий список задач:");

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine("  {0}. {1} (ID: {2})", i + 1, tasks[i].Title, tasks[i].Id);
            }

            Console.Write("\nВведите номер задачи для удаления (1-{0}): ", tasks.Count);
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                DeleteTaskByIndex(tasks, index - 1);
            }
            else
            {
                Console.WriteLine("Ошибка! Введите число.");
            }
        }

        public static void AddTask(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            int newId = 0;
            string title = "";
            int priority = 0;
            double duration = 0;
            bool isCompleted = false;

            int maxId = 0;
            foreach (Database t in tasks)
            {
                if (t.Id > maxId)
                    maxId = t.Id;
            }
            newId = maxId + 1;

            Console.WriteLine("Введите данные о задаче, чтобы добавить её (Название, Приоритет, Длительность, Выполнена ли):");

            title = CheckRead.ReadString();
            priority = CheckRead.ReadPriority();
            duration = CheckRead.ReadDoubleScore();
            isCompleted = CheckRead.ReadBool();

            tasks.Add(new Database(newId, title, priority, duration, isCompleted));
            Console.WriteLine("Добавлена задача '{0}' с ID = {1}\n", title, newId);
        }

        public static void GetHighPriorityTasks(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            Console.WriteLine("\nЗадачи с высоким приоритетом: ");

            var result = from t in tasks
                         where t.Priority == 3
                         select t;

            foreach (var t in result)
            {
                Console.WriteLine("{0} (ID: {1}, {2} ч.)", t.Title, t.Id, t.Duration);
            }
        }

        public static void GetUncompletedTasks(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            Console.WriteLine("\nНевыполненные задачи: ");

            var result = from t in tasks
                         where t.IsCompleted == false
                         select t;

            foreach (var t in result)
            {
                Console.WriteLine("{0} (Приор: {1}, {2} ч.)", t.Title, t.Priority, t.Duration);
            }
        }

        public static void GetTotalDuration(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            Console.WriteLine("\nОбщая длительность всех задач: ");

            double totalDuration = tasks.Sum(t => t.Duration);
            Console.Write(totalDuration);
        }

        public static void CountCompletedTasks(List<Database> tasks)
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nБаза данных пуста!");
                return;
            }
            int count = tasks.Count(t => t.IsCompleted == true);
            Console.WriteLine("\nКоличество выполненных задач: {0} из {1}", count, tasks.Count);
        }

        public override string ToString()
        {
            string priorityText = _priority == 3 ? "Высокий" : (_priority == 2 ? "Средний" : "Низкий");
            string statusText = _isCompleted ? "Выполнено" : "Не выполнено";
            return _id + " " + _title + " " + priorityText + " " + _duration + " " + statusText;
        }
    }
}