using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }

    public class LocalFileLogger<T> : ILogger
    {
        private string filePath;
        public LocalFileLogger(string filePath)
        {
            this.filePath = filePath;
        }

        public void LogInfo(string message)
        {
            using StreamWriter file = new StreamWriter(filePath, append: true);
            file.WriteLine($"[Info]: [{typeof(T).Name}] : {message}");
        }

        public void LogWarning(string message)
        {
            using StreamWriter file = new StreamWriter(filePath, append: true);
            file.WriteLine($"[Warning]: [{typeof(T).Name}] : {message}");
        }

        public void LogError(string message, Exception ex)
        {
            using StreamWriter file = new StreamWriter(filePath, append: true);
            file.WriteLine($"[Error] : [{typeof(T).Name}] : {message}. {ex.Message}");
        }
    }
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message) { }
    }

    public class Entity
    {
        public int Id;
        public int ParentId;
        public string Name;

        public Entity(int Id, int ParentId, string Name)
        {
            this.Id = Id;
            this.ParentId = ParentId;
            this.Name = Name;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            CustomException customException = new CustomException("Test Exception");

            LocalFileLogger<string> test0 = new LocalFileLogger<string>("G:\\Projects\\BARScourse\\Task2\\out.txt");
            test0.LogInfo("LogInfoString");
            test0.LogWarning("LogWarningString");
            test0.LogError("LogErrorString", customException);

            LocalFileLogger<int> test1 = new LocalFileLogger<int>("G:\\Projects\\BARScourse\\Task2\\out.txt");
            test1.LogInfo("LogInfoInt");
            test1.LogWarning("LogWarningInt");
            test1.LogError("LogErrorInt", customException);

            LocalFileLogger<double> test2 = new LocalFileLogger<double>("G:\\Projects\\BARScourse\\Task2\\out.txt");
            test2.LogInfo("LogInfoDouble");
            test2.LogWarning("LogWarningDouble");
            test2.LogError("LogErrorDouble", customException);

            /*Второе задание*/

            Dictionary<int, List<Entity>> work(List<Entity> ts)
            {
                if (ts.Count == 0)
                    return null;
                Dictionary<int, List<Entity>> dicktionary=new Dictionary<int, List<Entity>>();
                dicktionary = ts.GroupBy(x => x.ParentId).ToDictionary(x => x.Key, x=>x.ToList());
                return dicktionary;
            }

            List<Entity> ts = new List<Entity>();
            ts.Add(new Entity(1, 0, "Root Entity"));
            ts.Add(new Entity(2, 1, "Child of 1 Entity"));
            ts.Add(new Entity(3, 1, "Child of 1 Entity"));
            ts.Add(new Entity(4, 2, "Child of 2 Entity"));
            ts.Add(new Entity(5, 4, "Child of 4 Entity"));

            var dickionary = work(ts);
            if (dickionary != null)
                foreach (var dk in dickionary)
                    Console.WriteLine("Key = " + dk.Key + ", Value = List { " + string.Join(", ", dk.Value.Select(x => "Entity { Id = " + x.Id + " }")) + " }");
            else
                Console.WriteLine("Dictionary is empty");

            Console.ReadLine();
        }
    }
}
