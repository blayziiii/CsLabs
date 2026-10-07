namespace sem_1_practice_2_log_parse
{
    internal class Program
    {
        public static DateTime GetLogDateTime(string line)
        {
           
            int index1 = line.IndexOf(' ');
            int index2 = line.IndexOf(' ', index1 + 1);
            string datetimeStr = line.Substring(0, index2);
            float num = float.Parse("1,2");
            DateTime dateTime = DateTime.Parse(datetimeStr);
            return dateTime;

        }

        public static string GetLogLevel(string line)
        {
            int index1 = line.IndexOf('[');
            int index2 = line.IndexOf(']');
            string res = line.Substring(index1, index2 - index1 + 1);
            return res;
        }

        public static string GetLogType(string line)
        {
            int indexx1 = line.IndexOf('[');
            int indexx2 = line.IndexOf(']');
            int index1 = line.IndexOf('[', indexx1 + 1);
            int index2 = line.IndexOf(']', indexx2 + 1);
            string res = line.Substring(index1, index2 - index1 + 1);
            return res;
        }

        public static string GetLogText(string line)
        {
            int indexx1 = line.IndexOf(']');
            indexx1 = line.IndexOf(' ', indexx1);
            string res = line.Substring(indexx1 + 1, line.Length - indexx1 - 1);
            return res;

        }

        public static void Main()
        {
            string[] lines = File.ReadAllLines("event_server.log");
            foreach (string line in lines)
            {
                DateTime dt = GetLogDateTime(line);
                Console.WriteLine(dt);
                string level = GetLogLevel(line);
                Console.WriteLine(level);
                string type = GetLogType(line);
                Console.WriteLine(type);
                string text = GetLogText(line);
                Console.WriteLine(text);
                break;
            }






        }
    }
}