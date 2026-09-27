namespace program1
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello!");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hi,{name}");


            Console.Write("How old are you? ");
            string buffer = Console.ReadLine();
            int age = Convert.ToInt32(buffer); // ту инт это для всех типов данных
            age = int.Parse(buffer); // парс(парсить) это всегда из текста в объект
            Console.WriteLine($"You're {age} years");

        }
    }
}
