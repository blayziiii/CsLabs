namespace sem_1_lab_06_realtime_monitor
{
    public class Program
    {
        static double[] CPU = new double[20];
        static double[] RAM = new double[20];
        static double[] TEMP = new double[20];
        private static int count = 0;
        static void Main(string[] args)
        {
            var monitor = new ServerMonitor();
            monitor.DataReceived += OnDataReceived;
            monitor.Start();
            Console.WriteLine("Мониторинг запущен.");
            Console.WriteLine("Нажмите Enter для завершения.");
            Console.ReadLine();
            monitor.Stop();
        }
        public static string CreateBar(double value)
        {
            int filled = (int)Math.Round(value / 5);
            int dots = 20 - filled;
            return new string('#', filled) + new string('.',dots);
        }
        public static string TemperatureBar(double temperature)
        {
            int filled = (int)Math.Round(((temperature-30)/65)*20);
            int dots = 20 - filled;
            return new string('#', filled) + new string('.', dots);
        }
        public static void OnDataReceived(double cpu, double ram, double temperature)
        {
            Console.Clear();
            Console.WriteLine("Мониторинг запущен. Нажмите Enter для завершения.\n");
            if (count < 20)
            {
                CPU[count] = cpu;
                RAM[count] = ram;
                TEMP[count] = temperature;
                count++;
            }
            else
            {
                for(int i =0; i < CPU.Length - 1; i++ )
                {
                    CPU[i] = CPU[i + 1];
                }
                for (int i = 0; i < RAM.Length - 1; i++)
                {
                    RAM[i] = RAM[i + 1];
                }
                for (int i = 0; i < TEMP.Length - 1; i++)
                {
                    TEMP[i] = TEMP[i + 1];
                }
                CPU[CPU.Length - 1] = cpu;
                RAM[RAM.Length - 1] = ram;
                TEMP[TEMP.Length - 1] = temperature;
            }
            Console.Clear();

            Console.WriteLine("МОНИТОРИНГ СЕРВЕРА");
            Console.WriteLine("Нажмите Enter для завершения.\n");
            Console.WriteLine($"CPU: {cpu}%");
            Console.WriteLine($"RAM: {ram}%");
            Console.WriteLine($"Temperature: {temperature} °C");
            Console.WriteLine($"Процессор: {CreateBar(cpu)}");
            Console.WriteLine($"Память: {CreateBar(ram)}");
            Console.WriteLine($"Температура: {TemperatureBar(temperature)}");
            DrawGraph(CPU, "CPU (%)", 100, 5, 5);
            DrawGraph(RAM, "RAM (%)", 100, 5, 5);
            DrawGraph(TEMP, "Temperature (°C)", 95, 30, 5);
        }
        public static void DrawGraph(double[] data, string title, int maxLevel, int minLevel, int step)
        {
            Console.WriteLine($"\n История {title}:");
            for (int level = maxLevel; level >= minLevel; level -= step)
            {
                Console.Write($"{level,3} | ");
                for (int i = 0; i < count; i++)
                {
                    if (Math.Round(data[i]) >= level)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.Write("    +");
            for (int i = 0; i < count; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
            Console.Write("      ");
            for (int i = 0; i < count; i++)
            {
                Console.Write((i + 1) / 10 == 0 ? ' ' : (char)('0' + (i + 1) / 10));
            }
            Console.WriteLine();
            Console.Write("      ");
            for (int i = 0; i < count; i++)
            {
                Console.Write((i + 1) % 10);
            }
            Console.WriteLine();
        }
    }
}
