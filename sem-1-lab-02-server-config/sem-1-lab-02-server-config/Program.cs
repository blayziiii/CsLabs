namespace sem_1_lab_02_server_config
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Перед запуском сервера нужно проверить данные о нем.");

            Console.WriteLine("Введите кол-во памяти RAM в ГБ:");
            int ramgb = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите кло-во пользователей на сервере:");
            int people = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите кол-во модов используемые на сервере:");
            int mods = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("На сервере включено резервное копирование (true/false)?");
            bool backup = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Включен ли режим обслуживания (true/false)?");
            bool maintanance = Convert.ToBoolean(Console.ReadLine());

            string result = CheckConfiguration(ramgb, people, mods, backup, maintanance);

            Console.WriteLine(result);
        }

        public static string CheckConfiguration(
            int ramgb,
            int people,
            int mods,
            bool backup,
            bool maintanance)
        {
            bool canStart = true;
            bool hasWarning = false;

            if (ramgb < 4)
            {
                canStart = false;
            }
            else if (ramgb < 8)
            {
                hasWarning = true;
            }

            if (people > 50)
            {
                canStart = false;
            }
            else if (people > 30)
            {
                hasWarning = true;
            }

            if (mods > 50)
            {
                canStart = false;
            }
            else if (mods > 30)
            {
                hasWarning = true;
            }

            if (!backup)
            {
                hasWarning = true;
            }

            if (maintanance)
            {
                canStart = false;
            }

            if (!canStart)
            {
                return "Запуск сервера невозможен!";
            }
            else if (hasWarning)
            {
                return "Запуск сервера возможен, но необходимо предупредить администратора.";
            }
            else
            {
                return "Запуск сервера разрешен!";
            }
        }
    }
}