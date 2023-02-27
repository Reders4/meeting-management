using MeetingManagement.App.Common;
using MeetingManagement.Domain.Common;
using MeetingManagement.Domain.Interfaces.Clients;
using MeetingManagement.Domain.Interfaces.Common;
using MeetingManagement.Domain.Interfaces.Services;
using MeetingManagement.Domain.Models;

namespace MeetingManagement.App.Clients
{
    internal class MeetingUpdateClient : IMeetingUpdateClient
    {
        private readonly IFillData _fillData;
        private readonly IMeetingService _meetingService;
        public MeetingUpdateClient(IMeetingService meetingService, IFillData fillData)
        {
            _meetingService = meetingService;
            _fillData = fillData;
        }
        public void UpdateMeeting()
        {
            Console.Clear();
            uint meetingId = 0;
            DateOnly date;
            TimeOnly startTime;
            TimeOnly endTime;
            TimeOnly noticeTime;
            List<Participant> participants = default;


            while (true)
            {
                ConsoleOutput.DisplayHelpInfo();
                try
                {
                    switch (Console.ReadKey(true).Key)
                    {

                        case ConsoleKey.D:
                            date = _fillData.FillDate();
                            break;
                        case ConsoleKey.P:
                            participants = UpdateParticipants(meetingId);
                            break;
                        case ConsoleKey.H:
                            ConsoleOutput.DisplayUpdateKeysInfo();
                            break;
                        case ConsoleKey.S:
                            startTime = FillTimeWithCheckDateForUpdate(TimesMessage.StartTime, meetingId, date);
                            break;
                        case ConsoleKey.E:
                            endTime = FillTimeWithCheckDateForUpdate(TimesMessage.EndTime, meetingId, date);
                            break;
                        case ConsoleKey.N:
                            noticeTime = FillTimeWithCheckDateForUpdate(TimesMessage.NoticeTime, meetingId, date);
                            break;
                        case ConsoleKey.M:
                            ConsoleOutput.ConsoleWriteLineWithColor(_meetingService.GetMeetingById(meetingId).ToString());
                            break;
                        case ConsoleKey.I:
                            ConsoleOutput.ConsoleWriteLineWithColor("Введите идентификатор встречи:");
                            meetingId = ConsoleInput.TryGetMeetingId();
                            break;
                        case ConsoleKey.X:
                            var updatedMeeting = _meetingService.UpdateMeeting(meetingId, new Meeting(date, startTime, endTime, noticeTime, participants));
                            ConsoleOutput.ConsoleWriteLineWithColor($"Встреча с идентификатором: {meetingId} была успешно обновлена. \nИнформация о встрече: \n\n{updatedMeeting}", ConsoleColor.Green);
                            return;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            return;
                        default:
                            ConsoleOutput.DisplayWrongInputData();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    ConsoleOutput.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    //Дубляж кода - однако вынесение в отдельный метод работает не корректно
                    date = default;
                    startTime = default;
                    endTime = default;
                    noticeTime = default;
                    participants = default;
                    Console.WriteLine("Введённые вами данные сброшены!");
                }
                catch (AggregateException ex)
                {
                    ConsoleOutput.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    ConsoleOutput.ConsoleWriteLineWithColor("Возможно вы не выбрали идентификатор встречи \nДля ввода идентификатора встречи нажмите 'I'", ConsoleColor.DarkYellow, false);
                    //Дубляж кода - однако вынесение в отдельный метод работает не корректно
                    date = default;
                    startTime = default;
                    endTime = default;
                    noticeTime = default;
                    participants = default;
                    Console.WriteLine("Введённые вами данные сброшены!");

                }


            }

        }

        private List<Participant> UpdateParticipants(uint meetingId)
        {
            var participantsToUpdate = _meetingService.GetMeetingById(meetingId).Participants;
            var updatedParticipantList = _fillData.FillParticipants(participantsToUpdate);
            return updatedParticipantList;
        }

        private TimeOnly FillTimeWithCheckDateForUpdate(string message, uint meetingId, DateOnly date)
        {
            if (date == default)
            {
                return _fillData.FillTime(message, _meetingService.GetMeetingById(meetingId).Date);
            }
            else
            {
                return _fillData.FillTime(message, date);
            }
        }
    }
}
