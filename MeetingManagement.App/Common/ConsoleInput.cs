namespace MeetingManagement.App.Common
{
    internal static class ConsoleInput
    {
        public static uint TryGetMeetingId()
        {
            while (true)
            {
                var isId = uint.TryParse(Console.ReadLine(), out var id);
                if (isId)
                {
                    return id;
                }
                else
                {
                    ConsoleOutput.DisplayWrongInputData();
                }
            }
        }

        public static DateOnly InputDate()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Пожалуйста введите дату в формате: 'дд.мм.гггг'");
                var isDate = DateOnly.TryParse(Console.ReadLine(), out var date);
                if (isDate)
                {
                    return date;
                }
                else
                {
                    ConsoleOutput.DisplayWrongInputData();
                }
            }

        }

        public static string CheckInputNames(string startMessage, string errorMessage)
        {
            while (true)
            {
                Console.Write(startMessage);
                var input = Console.ReadLine();
                if (input == string.Empty || input.Trim() == string.Empty)
                {
                    ConsoleOutput.ConsoleWriteLineWithColor(errorMessage, ConsoleColor.Red);
                }
                else
                {
                    return input.Trim();
                }
            }
        }
    }
}
