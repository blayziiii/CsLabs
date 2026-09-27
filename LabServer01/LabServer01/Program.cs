namespace LabServer01
{
    class Program
    {
        public static void Main()
        {
            //Информация о пользователях
            short usonl = 1689;
            short usoff = 3468;
            int userall = usoff + usonl;
            // Данные пользователей
            byte ping = 60;
            byte ticrate = 128;
            float var = 0.6f;
            short fps = 324;
            // Нагрузка на железо у игроков сервера
            short mbvid = 4096;
            byte oper = 8;
            float gghz = 3.5f;
            // Расположение сервака 
            string strana = "RUS";
            string nameserv = "MAP|AWP LEGO|BHOP WS KNIFE SKINS|";
            string game = "Counter Strike 2";
            // Вывод 
          
            Console.WriteLine($"Информация о сервере {nameserv}");
            Console.WriteLine($"Игра в которой находится сервер: {game}");
            Console.WriteLine($"Страна на которой находиться сервер: {strana}");
            Console.WriteLine("");
            Console.WriteLine("Информация о пользователях:");
            Console.WriteLine($"Пользовавтели онлайн: {usonl}");
            Console.WriteLine($"Пользователи оффлайн: {usoff} ");
            Console.WriteLine($"Всего зарегестрированных пользователей: {userall}");
            Console.WriteLine("");
            Console.WriteLine("Информация о работе сервера:");
            Console.WriteLine($"Средний пинг на сервере: {ping}");
            Console.WriteLine($"Тикрейт сервера: {ticrate}");
            Console.WriteLine($"Задержка сервера(var): {var}");
            Console.WriteLine($"Средний fps на сервере у игроков: {fps}");
            Console.WriteLine("");
            Console.WriteLine("Оптимальные хар-ки для игры на сервере:");
            Console.WriteLine($"Оптимальный объем видеопамяти: {mbvid} MB");
            Console.WriteLine($"Оптимальный объем оперативной памяти: {oper} GB");
            Console.WriteLine($"Оптимальная герцовка процессора: {gghz} GHZ");
















        }
    }
}
