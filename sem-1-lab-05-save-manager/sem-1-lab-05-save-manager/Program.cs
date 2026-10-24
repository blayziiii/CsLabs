
using Newtonsoft.Json;
using System.Globalization;
using System.IO;

namespace sem_1_lab_05_save_manager
{
    public class Program
    {
        public static void Main()
        {
            string projectDirectory = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

            string indexPath = Path.Combine(projectDirectory, "index.json");
            string savesDirectory = Path.Combine(projectDirectory, "saves");
            string backupsDirectory = Path.Combine(projectDirectory, "backups");

            Directory.CreateDirectory(savesDirectory);
            Directory.CreateDirectory(backupsDirectory);

            List<SaveEntry> entries = LoadIndex(indexPath);

            if (entries == null)
            {
                return;
            }
            while (true)
            {
                Console.WriteLine("\n=== МЕНЕДЖЕР СОХРАНЕНИЙ ===");
                Console.WriteLine("1. Показать список сохранений");
                Console.WriteLine("2. Посмотреть сохранение");
                Console.WriteLine("3. Изменить сохранение");
                Console.WriteLine("4. Создать резервную копию");
                Console.WriteLine("5. Восстановить резервную копию");
                Console.WriteLine("6. Удалить сохранение");
                Console.WriteLine("7. Выйти");
                Console.Write("Выбери действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (SaveEntry entry in entries)
                    {
                        Console.WriteLine(entry.name);
                    }
                }
                else if (choice == "2")
                {
                    SaveEntry entry = AskForEntry(entries);

                    if (entry != null)
                    {
                        SaveData save = LoadSave(savesDirectory, entry);

                        if (save != null)
                        {
                            PrintSave(entry, save);
                        }
                    }
                }
                else if (choice == "3")
                {
                    SaveEntry entry = AskForEntry(entries);

                    if (entry != null)
                    {
                        EditSave(savesDirectory, indexPath, entry, entries);
                    }
                }
                else if (choice == "4")
                {
                    SaveEntry entry = AskForEntry(entries);

                    if (entry != null)
                    {
                        CreateBackup(savesDirectory, backupsDirectory, entry);
                    }
                }
                else if (choice == "5")
                {
                    SaveEntry entry = AskForEntry(entries);

                    if (entry != null)
                    {
                        RestoreBackup(
                            savesDirectory, backupsDirectory,
                            indexPath, entries, entry);
                    }
                }
                else if (choice == "6")
                {
                    SaveEntry entry = AskForEntry(entries);

                    if (entry != null)
                    {
                        DeleteSave(savesDirectory, indexPath, entries, entry);
                    }
                }
                else if (choice == "7")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Неверный пункт меню!");
                }
            }
        }

        public static SaveEntry AskForEntry(List<SaveEntry> entries)
        {
            Console.Write("Введи имя сохранения: ");
            string name = Console.ReadLine();

            SaveEntry entry = FindByName(entries, name);

            if (entry == null)
            {
                Console.WriteLine("Сохранение не найдено!");
            }

            return entry;
        }

        public static List<SaveEntry> LoadIndex(string indexPath)
        {
            if (!File.Exists(indexPath))
            {
                Console.WriteLine("Файл index.json не найден!");
                return null;
            }

            try
            {
                string json = File.ReadAllText(indexPath);

                List<SaveEntry> entries =
                    JsonConvert.DeserializeObject<List<SaveEntry>>(json);

                if (entries == null)
                {
                    Console.WriteLine("Индекс пуст или содержит некорректные данные!");
                    return null;
                }

                return entries;
            }
            catch (JsonException)
            {
                Console.WriteLine("Ошибка: JSON индекса повреждён!");
                return null;
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка чтения файла индекса!");
                return null;
            }
        }

        public static SaveData LoadSave(string savesDirectory, SaveEntry entry)
        {
            string path = Path.Combine(savesDirectory, entry.file);

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл сохранения не найден!");
                return null;
            }

            try
            {
                string json = File.ReadAllText(path);
                SaveData save = JsonConvert.DeserializeObject<SaveData>(json);

                if (save == null)
                {
                    Console.WriteLine("Сохранение не содержит данных!");
                    return null;
                }

                return save;
            }
            catch (JsonException)
            {
                Console.WriteLine("Ошибка: JSON сохранения повреждён!");
                return null;
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка чтения файла сохранения!");
                return null;
            }
        }

        public static void PrintSave(SaveEntry entry, SaveData save)
        {
            Console.WriteLine($"\nСохранение: {entry.name}");
            Console.WriteLine($"Персонаж: {save.Character}");
            Console.WriteLine($"Карта: {save.Map}");

            if (save.Position != null)
            {
                Console.WriteLine(
                    $"Координаты: {save.Position.x}, {save.Position.y}");
            }

            Console.WriteLine($"Здоровье: {save.Health}");
            Console.WriteLine("Инвентарь:");

            if (save.Inventory != null)
            {
                foreach (string item in save.Inventory)
                {
                    Console.WriteLine($"- {item}");
                }
            }
        }

        public static SaveEntry FindByName(List<SaveEntry> entries, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            foreach (SaveEntry entry in entries)
            {
                if (string.Equals(
                    entry.name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return entry;
                }
            }

            return null;
        }

        public static void SaveGame(
            string savesDirectory, SaveEntry entry, SaveData save)
        {
            string path = Path.Combine(savesDirectory, entry.file);

            string json = JsonConvert.SerializeObject(
                save, Formatting.Indented);

            File.WriteAllText(path, json);
        }

        public static void SaveIndex(string indexPath, List<SaveEntry> entries)
        {
            string json = JsonConvert.SerializeObject(
                entries, Formatting.Indented);

            File.WriteAllText(indexPath, json);
        }

        public static void EditSave(
            string savesDirectory,
            string indexPath,
            SaveEntry entry,
            List<SaveEntry> entries)
        {
            SaveData save = LoadSave(savesDirectory, entry);

            if (save == null)
            {
                return;
            }

            Console.WriteLine("Оставь поле пустым, если не хочешь его менять.");

            Console.Write($"Имя персонажа ({save.Character}): ");
            string character = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(character))
            {
                save.Character = character;
            }

            Console.Write($"Карта ({save.Map}): ");
            string map = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(map))
            {
                save.Map = map;
            }

            if (save.Position == null)
            {
                save.Position = new Position();
            }

            Console.Write($"Координата X ({save.Position.x}): ");
            string xInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(xInput))
            {
                if (int.TryParse(xInput, out int x))
                {
                    save.Position.x = x;
                }
                else
                {
                    Console.WriteLine("Некорректная координата X. Оставлено старое значение.");
                }
            }

            Console.Write($"Координата Y ({save.Position.y}): ");
            string yInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(yInput))
            {
                if (int.TryParse(yInput, out int y))
                {
                    save.Position.x = y;
                }
                else
                {
                    Console.WriteLine("Некорректная координата Y. Оставлено старое значение.");
                }
            }

            Console.Write($"Здоровье ({save.Health}): ");
            string healthInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(healthInput))
            {
                if (int.TryParse(healthInput, out int health))
                {
                    save.Health = health;
                }
                else
                {
                    Console.WriteLine("Некорректное здоровье. Оставлено старое значение.");
                }
            }

            Console.WriteLine("Текущий инвентарь:");

            if (save.Inventory == null)
            {
                save.Inventory = new List<string>();
            }

            foreach (string item in save.Inventory)
            {
                Console.WriteLine($"- {item}");
            }

            Console.Write("Введи новый инвентарь через запятую или оставь пустым: ");
            string inventoryInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(inventoryInput))
            {
                save.Inventory = inventoryInput
                    .Split(',', StringSplitOptions.TrimEntries |
                                  StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

            try
            {
                SaveGame(savesDirectory, entry, save);

                entry.updatedAt = DateTime.Now;
                SaveIndex(indexPath, entries);

                Console.WriteLine("Сохранение успешно изменено!");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка записи файла. Проверь доступ к папке.");
            }
        }

        public static void CreateBackup(
            string savesDirectory, string backupsDirectory, SaveEntry entry)
        {
            string sourcePath = Path.Combine(savesDirectory, entry.file);
            string backupPath = Path.Combine(backupsDirectory, entry.file);

            if (!File.Exists(sourcePath))
            {
                Console.WriteLine("Исходное сохранение не найдено!");
                return;
            }

            try
            {
                Directory.CreateDirectory(backupsDirectory);
                File.Copy(sourcePath, backupPath, true);
                Console.WriteLine("Резервная копия создана.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка создания резервной копии!");
            }
        }

        public static void RestoreBackup(
            string savesDirectory,
            string backupsDirectory,
            string indexPath,
            List<SaveEntry> entries,
            SaveEntry entry)
        {
            string backupPath = Path.Combine(backupsDirectory, entry.file);
            string savePath = Path.Combine(savesDirectory, entry.file);

            if (!File.Exists(backupPath))
            {
                Console.WriteLine("Резервная копия не найдена!");
                return;
            }

            try
            {
                File.Copy(backupPath, savePath, true);

                entry.updatedAt = DateTime.Now;
                SaveIndex(indexPath, entries);

                Console.WriteLine("Резервная копия восстановлена.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка восстановления резервной копии!");
            }
        }

        public static void DeleteSave(
            string savesDirectory,
            string indexPath,
            List<SaveEntry> entries,
            SaveEntry entry)
        {
            string path = Path.Combine(savesDirectory, entry.file);

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                {
                    Console.WriteLine("Файл сохранения не найден!");
                }

                entries.Remove(entry);
                SaveIndex(indexPath, entries);

                Console.WriteLine("Запись удалена из индекса.");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка при удалении или записи индекса!");
            }
        }
    }
}
