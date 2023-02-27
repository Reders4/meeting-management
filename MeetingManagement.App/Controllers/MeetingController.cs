using MeetingManagement.App.Common;
using MeetingManagement.Domain.Interfaces.Clients;
using MeetingManagement.Domain.Interfaces.Services;

namespace MeetingManagement.App.Controllers
{
    public class MeetingController
    {
        private readonly IMeetingService _meetingService;
        private readonly IMeetingAdditionClient _meetingAdditionClient;
        private readonly IMeetingUpdateClient _meetingUpdateClient;

        public MeetingController(IMeetingService meetingService, IMeetingAdditionClient meetingAdditionClient, IMeetingUpdateClient meetingUpdateClient)
        {
            _meetingService = meetingService;
            _meetingAdditionClient = meetingAdditionClient;
            _meetingUpdateClient = meetingUpdateClient;
            Task.Run(() => _meetingService.MeetingNotify());
            _meetingService.Notify += ConsoleOutput.MeetingNotify;
        }
        public void StartApp()
        {
            Console.Clear();

            while (true)
            {
                ConsoleOutput.DisplayHelpInfo();
                try
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.A:
                            _meetingAdditionClient.AddMeeting();
                            break;
                        case ConsoleKey.H:
                            ConsoleOutput.DisplayKeysInfo();
                            break;
                        case ConsoleKey.G:
                            ConsoleOutput.ConsoleWriteLineWithColor(_meetingService.GetMeetingsByDate(ConsoleInput.InputDate()), ConsoleColor.DarkYellow);
                            break;
                        case ConsoleKey.D:
                            ConsoleOutput.ConsoleWriteLineWithColor("Для удаления встречи введите её идентификатор", ConsoleColor.DarkYellow);
                            _meetingService.RemoveMeeting(ConsoleInput.TryGetMeetingId());
                            ConsoleOutput.ConsoleWriteLineWithColor("Встреча успешно удалена!", ConsoleColor.Green);
                            break;
                        case ConsoleKey.U:
                            _meetingUpdateClient.UpdateMeeting();
                            break;
                        case ConsoleKey.E:
                            _meetingService.ExportMeetings(ConsoleInput.InputDate());
                            ConsoleOutput.ConsoleWriteLineWithColor("Файл успешно сохранён на рабочий стол", ConsoleColor.Green);
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            return;
                        default:
                            ConsoleOutput.ConsoleWriteLineWithColor("Введена неверная кнопка", ConsoleColor.Red);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleOutput.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }
                
            }
        }

    }
}
