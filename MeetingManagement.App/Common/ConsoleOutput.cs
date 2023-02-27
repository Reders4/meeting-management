namespace MeetingManagement.App.Common
{
    internal static class ConsoleOutput
    {
        public static void MeetingNotify(string message) => ConsoleWriteLineWithColor(message + "\n", ConsoleColor.Green);

        public static void ConsoleWriteLineWithColor(string message, ConsoleColor color = ConsoleColor.White, bool needClear = true)
        {
            if (needClear)
            {
                Console.Clear();
            }
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void DisplayHelpInfo()
        {
            Console.WriteLine("Для предоставления информации о возможных действиях нажмите кнопку 'H'");
        }

        public static void DisplayWrongInputData()
        {
            ConsoleWriteLineWithColor("Данные введены неверно", ConsoleColor.Red);
        }

        public static void DisplayKeysInfo()
        {
            Console.Clear();
            Console.WriteLine("Для добавления новой встречи в расписание нажмите кнопку 'A'");
            Console.WriteLine("Для обновления данных о встрече в расписании нажмите кнопку 'U'");
            Console.WriteLine("Для удаления встречи в расписании нажмите кнопку 'D'");
            Console.WriteLine("Для получения списка встреч на определенную дату нажмите кнопку 'G'");
            Console.WriteLine("Для экспорта списка встреч в текстовый файл нажмите 'E'");
            Console.WriteLine("Для выхода нажмите 'Escape'");
        }

        public static void DisplayUpdateKeysInfo()
        {
            Console.Clear();
            Console.WriteLine("Для обновления данных о встрече выберите действие:");
            Console.WriteLine("Для выбора идентификатора встречи нажмите 'I'");
            Console.WriteLine("Для редактирования даты встречи нажмите 'D'");
            Console.WriteLine("Для редактирования времени начала встречи нажмите 'S'");
            Console.WriteLine("Для редактирования времени окончания встречи нажмите 'E'");
            Console.WriteLine("Для редактирования времени уведомления нажмите 'N'");
            Console.WriteLine("Для редактирования участников встречи нажмите 'P'");
            Console.WriteLine("Для подтверждения изменений и завершения редактирования нажмите 'X'");
            Console.WriteLine("Для просмотра редактируемой встречи нажмите 'M'");
            Console.WriteLine("Для отмены редактирования и возвращения в меню нажмите 'Escape'");
        }
    }
}
