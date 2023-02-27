using MeetingManagement.Domain.Common;
using MeetingManagement.Domain.Interfaces.Common;
using MeetingManagement.Domain.Models;

namespace MeetingManagement.App.Common
{
    internal class FillData : IFillData
    {
        public DateOnly FillDate()
        {
            ConsoleOutput.ConsoleWriteLineWithColor("Хотите использвать текущую дату? Для подтверждения нажмите 'Y' для отказа 'N'", ConsoleColor.White, true);

            while (true)
            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        Console.Clear();
                        return DateTimeSupport.GetTodayDate();
                    case ConsoleKey.N:
                        var date = ConsoleInput.InputDate();
                        if (date >= DateTimeSupport.GetTodayDate())
                        {
                            return date;
                        }
                        else
                        {
                            ConsoleOutput.ConsoleWriteLineWithColor(ExceptionMessage.DateIsPassed, ConsoleColor.Red);
                            break;
                        }
                    default:
                        ConsoleOutput.ConsoleWriteLineWithColor("Введена неверная кнопка", ConsoleColor.Red);
                        ConsoleOutput.ConsoleWriteLineWithColor("Для использования текущей даты нажмите 'Y' для самостоятельного ввода даты нажмите 'N'", ConsoleColor.White, false);
                        break;
                }
            }

        }

        public TimeOnly FillTime(string message, DateOnly date)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine(message);
                var isTime = TimeOnly.TryParse(Console.ReadLine(), out var time);
                if (isTime)
                {
                    if ((time > DateTimeSupport.GetTimeNow() && date >= DateTimeSupport.GetTodayDate()) || (time < DateTimeSupport.GetTimeNow() && date > DateTimeSupport.GetTodayDate()))
                    {
                        return time;
                    }
                    if (time <= DateTimeSupport.GetTimeNow() && date == DateTimeSupport.GetTodayDate())
                    {
                        ConsoleOutput.ConsoleWriteLineWithColor(ExceptionMessage.TimeIsPassed, ConsoleColor.Red);
                    }
                    if (date < DateTimeSupport.GetTodayDate())
                    {
                        throw new ArgumentException(ExceptionMessage.DateIsPassed);
                    }
                }
                else
                {
                    ConsoleOutput.DisplayWrongInputData();
                }
            }
        }

        public List<Participant> FillParticipants(List<Participant> participants)
        {
            ConsoleOutput.ConsoleWriteLineWithColor("Встреча не может проходить без участников :)");

            while (true)
            {
                Console.WriteLine("Желаете добавить участников? 'Y' - да, 'N' - нет");
                Console.WriteLine("Для удаления участника нажмите 'D'");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        ConsoleOutput.ConsoleWriteLineWithColor("Для добавления необходимо ввести имя и фамилию участника", ConsoleColor.DarkYellow);
                        participants.Add(InputParticipant());
                        break;
                    case ConsoleKey.N:
                        if (participants.Count == 0)
                        {
                            ConsoleOutput.ConsoleWriteLineWithColor("Необходимо добавить хотя бы одного участника, для добавления нажмите 'Y'", ConsoleColor.DarkYellow);
                            continue;
                        }
                        else
                        {
                            return participants;
                        }
                    case ConsoleKey.D:
                        DeleteParticipant(participants);
                        break;
                    default:
                        ConsoleOutput.ConsoleWriteLineWithColor("Если вы добавили/удалили участников и хотите продолжить нажмите 'N'", ConsoleColor.DarkYellow);
                        break;
                }
            }
        }

        private void DeleteParticipant(List<Participant> participants)
        {
            ConsoleOutput.ConsoleWriteLineWithColor("Для удаления необходимо ввести имя и фамилию участника", ConsoleColor.DarkYellow);
            var participant = InputParticipant();
            var participantToDelete = participants.FirstOrDefault(x => x.FirstName == participant.FirstName && x.LastName == participant.LastName);
            if (participantToDelete != null) 
            {
                participants.Remove(participantToDelete);
                ConsoleOutput.ConsoleWriteLineWithColor($"Участник '{participant}' успешно удалён", ConsoleColor.Green, false);
            }
            else
            {
                ConsoleOutput.ConsoleWriteLineWithColor($"Участник '{participant}' не найден", ConsoleColor.Red, false);
            }
        }
        private Participant InputParticipant()
        {
            var participant = new Participant
            {
                FirstName = ConsoleInput.CheckInputNames("Введите имя: ", "Имя не может быть пустым"),
                LastName = ConsoleInput.CheckInputNames("Введите фамилию: ", "Фамилия не может быть пустой")
            };

            return participant;
        }
    }
}
