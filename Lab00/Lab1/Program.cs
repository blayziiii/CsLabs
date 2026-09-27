using System.Net.Cache;

namespace Lab1
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine("Привет пользователь для того чтобы использовать этот сервис нужно сначала зарегестрироваться!");
            Console.Write("Напиши свой ник: ");
            string name = Console.ReadLine();
            Console.Write("Сколько тебе лет: ");
            string buffer = Console.ReadLine();
            int age = Convert.ToInt32(buffer);
            Console.Write("Укажи свой пол: ");
            string fame = Console.ReadLine();
            Console.Write("Регистарция почти закончена,напиши в какую игру ты будешь играть с этого профиля: ");
            string game = Console.ReadLine();
            Console.WriteLine($"Профиль для игры {game} создан!");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine($"Твой ник: {name} ");
            Console.WriteLine($"Возраст: {age} ");
            Console.WriteLine($"Пол: {fame} ");
            Console.WriteLine($" ");
            Console.WriteLine($"Аккаунт создан! Увидимся в {game}!");

















        }
    }
}
