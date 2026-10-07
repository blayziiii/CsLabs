using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace sem_1_lab_04_event_log

{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("event_server.log");
            LogEntry[] entries = ParseLog(lines);
            Console.WriteLine(GetServerStatus(entries));
            Console.WriteLine(CountByLevel(entries, "Error"));
            LogEntry[] searchResult = Search(entries, "сервер");
            Console.WriteLine(searchResult.Length);
            LogEntry[] errors = FilterByLevel(entries, "Error");
            Console.WriteLine(errors.Length);
            LogEntry[] serverEntries = FilterByCategory(entries, "Server");
            Console.WriteLine(serverEntries.Length);
            LogEntry[] dateEntries = FilterByDate(entries, new DateTime(2026, 9, 2));
            Console.WriteLine(dateEntries.Length);
        }
        public static LogEntry[] ParseLog(string[] lines)
        {
            LogEntry[] entries = new LogEntry[lines.Length];
            for (int i = 0; i < entries.Length; i++)
            {
                LogEntry entry = new LogEntry();
                entry.Timestamp = DateTime.Parse(lines[i].Substring(0, 23));
                int firstOpenBracket = lines[i].IndexOf('[');
                int firstCloseBracket = lines[i].IndexOf(']', firstOpenBracket);
                entry.Level = lines[i].Substring(firstOpenBracket + 1,firstCloseBracket - firstOpenBracket - 1);
                int secondOpenBracket = lines[i].IndexOf('[', firstCloseBracket);
                int secondCloseBracket = lines[i].IndexOf(']', secondOpenBracket);
                entry.Category = lines[i].Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
                entry.Message = lines[i].Substring(secondCloseBracket + 2);
                entries[i] = entry;
            }
            return entries;
        }
        public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
        {
            var result = new List<LogEntry>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Timestamp.Date == date.Date)
                {
                    result.Add(entries[i]);
                }
            }
            return result.ToArray()
;
        }
        public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
        {
            var result = new List<LogEntry>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    result.Add(entries[i]);
                }
            }
            return result.ToArray();
        }
        public static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
        {
            var result = new List<LogEntry>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Category == category)
                {
                    result.Add(entries[i]);
                }
            }
            return result.ToArray();
        }
        public static LogEntry[] Search(LogEntry[] entries, string text)
        {
            var result = new List<LogEntry>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Message.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(entries[i]);
                }
            }
            return result.ToArray();
        }
        public static int CountByLevel(LogEntry[] entries, string level)
        {
            int count = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    count++;
                }
            }
            return count;
        }
        public static string GetServerStatus(LogEntry[] entries)
        {
            bool haserrors = false;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == "Fatal" && entries[i].Category == "Server")
                {
                    return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
                }
                else if(entries[i].Level == "Error")
                {
                    haserrors = true;
                }
            }
            if (haserrors)
            {
                return "Есть ошибки: требуется проверка";
            }
            return "Сервер работает штатно";
        }
        
    }
}
