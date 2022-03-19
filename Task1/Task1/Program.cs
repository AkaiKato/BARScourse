using System;
namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task work = new Task();
            work.OnKeyPressed += printMessage;
            work.Run();

            void printMessage(Object sender, char letter)
            {
                Console.WriteLine($"\nYour symbol is {letter}");
            }
        }
    }
    public class Task
    {
        public event EventHandler<char> OnKeyPressed;

        public void Run()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Press the button:");
                var letter = Console.ReadKey();
                
                switch (letter.KeyChar)
                {
                    case 'C':
                        flag = false;
                        break;
                    case 'c':
                        flag = false;
                        break;
                    default:
                        OnKeyPressed?.Invoke(this, letter.KeyChar);
                        break;
                }

            }
        }
    }
}