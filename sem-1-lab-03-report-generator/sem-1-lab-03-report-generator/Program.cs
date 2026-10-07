using System.Net.Mail;

namespace sem_1_lab_03_report_generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("event_server (1).log");
            int warnings = GetWarnings(lines);
            int errors = GetErrors(lines);
            DateTime eventDate = GetEventDate(lines);
            string winner = GetWinner(lines);
            int winnerPoints = GetWinnerPoints(lines, winner);
            string eventItem = GetEventItem(lines);
            string consolationReward = GetConsalationReward(lines);
            Console.WriteLine("# Итоги события: Восстание Ледяного Пламени");
            Console.WriteLine(eventDate.ToString("Дата: dd.MM.yyyy"));
            Console.WriteLine("Победитель: " + winner);
            Console.WriteLine("Очки победителя: " + winnerPoints);
            Console.WriteLine("Ивентовый предмет: " + eventItem);
            Console.WriteLine("Утешительная награда " + consolationReward);
            Console.WriteLine("Предупреждений во время события: " + warnings);
            Console.WriteLine("Ошибок во время события: " + errors);

        }
        static DateTime GetEventDate(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Событие началось:"))
                {
                    string dateText = line.Substring(0, 10);
                    DateTime date = DateTime.Parse(dateText);
                    return date;
                }
            }
            return DateTime.MinValue;
        }
            static string GetWinner(string[] lines)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    {
                        string line = lines[i];
                        if (line.Contains("объявлены победителями события"))
                        {
                            int massageStart = line.IndexOf("] ") + 2;
                            int winnerEnd = line.IndexOf(" объявлены победителями");
                            string winner = line.Substring(massageStart, winnerEnd - massageStart);
                            return winner;
                        }
                    }
                }
                return null;
            }
            static int GetWinnerPoints(string[] lines, string winner)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("[Reward]") && line.Contains(winner + " получили "))
                    {
                        string startText = winner + " получили ";
                        int start = line.IndexOf(startText) + startText.Length;
                        int end = line.IndexOf(" очков", start);
                        string pointText = line.Substring(start, end - start);
                        int point = int.Parse(pointText);
                        return point;
                    }
                }
                return 0;
            }
            static string GetEventItem(string[] lines)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("получили ивентовый предмет:"))
                    {
                        int start = line.IndexOf("получили ивентовый предмет:") + "получили ивентовый предмет:".Length;
                        string item = line.Substring(start).Trim();
                        return item;
                    }

                }
                return null;
            }
            static string GetConsalationReward(string[] lines)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("утешительную награду:"))
                    {
                        int masageStart = line.IndexOf("] ") + 2;
                        int clanEnd = line.IndexOf(" получили утешительную награду:");
                        string clan = line.Substring(masageStart, clanEnd - masageStart);
                        int rewardStart = line.IndexOf("получили утешительную награду:") + "получили утешительную награду:".Length;
                        int rewardEnd = line.IndexOf(" очков", rewardStart);
                        string reward = line.Substring(rewardStart, rewardEnd - rewardStart);
                        return clan + ": " + reward + " очков";





                    }


                }
                return null;
            }
        
        static int GetWarnings(string[] lines)
        {
            int warnings = 0;
            bool isEventRunning = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Событие началось:"))
                {
                    isEventRunning = true;
                }
                if (isEventRunning && line.Contains("[Warning]"))
                {
                    warnings++;
                }
                if (line.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
                {
                    isEventRunning = false;
                }


            }
            return warnings;

        }
        static int GetErrors(string[] lines)
        {
            int errors = 0;
            bool isEventRunning = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("Событие началось:"))
                {
                    isEventRunning = true;
                }
                 if (isEventRunning && line.Contains("[Error]"))
                {
                    errors++;
                }
                if (line.Contains("Событие \"Восстание Ледяного Пламени\" закрыто"))
                {
                    isEventRunning = false;
                }
            }
            return errors;
        }







    }
}

