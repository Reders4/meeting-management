using MeetingManagement.Domain.Common;
using MeetingManagement.Domain.Interfaces.Services;
using MeetingManagement.Domain.Models;
using MeetingManagement.Services.Common;

namespace MeetingManagement.Services.Services
{
    public sealed class MeetingService : IMeetingService
    {
        private readonly List<Meeting> _meetings;
        private readonly MeetingValidation _meetingValidation = new();
        public event Action<string> Notify;

        public MeetingService()
        {
            _meetings = SetupMockMeetingsData();
        }

        public void AddMeeting(Meeting meeting)
        {
            lock (_meetings)
            {
                _meetingValidation.AdditionValidation(meeting, _meetings);
                _meetings.Add(meeting);
            }
            

        }
        public void RemoveMeeting(uint meetingId)
        {
            _meetings.Remove(GetMeetingById(meetingId));
        }
        public Meeting UpdateMeeting(uint meetingId, Meeting newMeetingData)
        {
            var meetingToUpdate = GetMeetingById(meetingId);
            return _meetingValidation.UpdatingValidation(meetingToUpdate, newMeetingData, _meetings);
        }
        public string GetMeetingsByDate(DateOnly date)
        {
            var meetings = _meetings.Where(x => x.Date == date);
            if (!meetings.Any())
            {
                return $"Запланированных встреч на {date} не найдено!";
            }
            var result = $"Встречи на {date}:\n";
            foreach (var item in meetings)
            {
                result += "\n" + item +"\n";
            }

            return result;
        }

        public Meeting GetMeetingById(uint meetingId)
        {
            var meeting = _meetings.FirstOrDefault(x => x.Id == meetingId);
            if (meeting != null)
            {
                return meeting;
            }
            else
            {
                throw new AggregateException("Встреча не найдена!");
            }
        }

        public void ExportMeetings(DateOnly date)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Meetings.txt";
            using var sw = new StreamWriter(path);
            sw.WriteLine(GetMeetingsByDate(date));
        }

        public void MeetingNotify()
        {
            while (true)
            {
                lock (_meetings)
                {
                    var meeting = _meetings.Where(x => x.Date == DateTimeSupport.GetTodayDate())
                        .OrderBy(x => x.StartTime)
                        .FirstOrDefault(x => x.StartTime > DateTimeSupport.GetTimeNow());

                    var timeNow = DateTimeSupport.GetTimeNow();
                    if (meeting != null && meeting.NoticeTime.Hour == timeNow.Hour && meeting.NoticeTime.Minute == timeNow.Minute)
                    {
                        Notify?.Invoke($"Уведомление о предстоящей встрече: \n\n{meeting}");
                        
                    }
                }
                Thread.Sleep(35000);

            }
        }

       
        private List<Meeting> SetupMockMeetingsData()
        {
            return new List<Meeting>
            {
                new Meeting
                (
                    new DateOnly(2023, 02, 20), 
                    new TimeOnly(15, 00), 
                    new TimeOnly(17, 00), 
                    new TimeOnly(14, 40),
                    new List<Participant>()
                    {
                        new Participant() { FirstName = "Дмитрий", LastName = "Лежень"},
                        new Participant() { FirstName = "Егор", LastName = "Суханов"}
                    }
                ),
                new Meeting
                (
                    new DateOnly(2023, 02, 20),
                    new TimeOnly(10, 00),
                    new TimeOnly(11, 00),
                    new TimeOnly(9, 45),
                    new List<Participant>()
                    {
                        new Participant() { FirstName = "Кирилл", LastName = "Мичурин"},
                        new Participant() { FirstName = "Олег", LastName = "Сергеев"}
                    }
                ),
                new Meeting
                (
                    new DateOnly(2023, 03, 20),
                    new TimeOnly(13, 00),
                    new TimeOnly(13, 30),
                    new TimeOnly(12,50),
                    new List<Participant>()
                    {
                        new Participant() { FirstName = "Юлия", LastName = "Малинина"}
                    }
                ),
                new Meeting
                (
                    new DateOnly(2023, 03, 20),
                    new TimeOnly(15, 00),
                    new TimeOnly(15, 30),
                    new TimeOnly(14,50),
                    new List<Participant>()
                    {
                        new Participant() { FirstName = "Игорь", LastName = "Нечаев"}
                    }
                )
            };
        }
    }
}
