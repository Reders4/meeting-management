using MeetingManagement.App.Common;
using MeetingManagement.Domain.Common;
using MeetingManagement.Domain.Interfaces.Clients;
using MeetingManagement.Domain.Interfaces.Common;
using MeetingManagement.Domain.Interfaces.Services;
using MeetingManagement.Domain.Models;

namespace MeetingManagement.App.Clients
{
    internal class MeetingAdditionClient : IMeetingAdditionClient
    {
        private readonly IFillData _fillData;
        private readonly IMeetingService _meetingService;
        public MeetingAdditionClient(IMeetingService meetingService, IFillData fillData)
        {
            _meetingService = meetingService;
            _fillData = fillData;
        }
        public void AddMeeting()
        {
            ConsoleOutput.ConsoleWriteLineWithColor("Для добавления встречи в расписание необходимо указать дату и время");
            var date = _fillData.FillDate();
            var startTime = _fillData.FillTime(TimesMessage.StartTime, date);
            var endTime = _fillData.FillTime(TimesMessage.EndTime, date);
            var noticeTime = _fillData.FillTime(TimesMessage.NoticeTime, date);
            var participants = _fillData.FillParticipants(new List<Participant>());
            ConfirmAddMeeting(new Meeting(date, startTime, endTime, noticeTime, participants));

        }

        private void ConfirmAddMeeting(Meeting meeting)
        {
            Console.Clear();

            while (true)
            {
                ConsoleOutput.ConsoleWriteLineWithColor($"Информация о встрече: \n\n{meeting}\n\nЖелаете добавить встречу в расписание? 'Y' - да, 'N' - нет", ConsoleColor.DarkYellow);
                switch (Console.ReadKey().Key)
                {

                    case ConsoleKey.Y:
                        _meetingService.AddMeeting(meeting);
                        ConsoleOutput.ConsoleWriteLineWithColor($"Встреча успешно добавлена, информация о встрече: \n\n{meeting}", ConsoleColor.Green);
                        return;
                    case ConsoleKey.N:
                        ConsoleOutput.ConsoleWriteLineWithColor("Встреча не будет добавлена в расписание!", ConsoleColor.DarkYellow);
                        return;
                    default:
                        ConsoleOutput.ConsoleWriteLineWithColor("Введена неверная кнопка", ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}
