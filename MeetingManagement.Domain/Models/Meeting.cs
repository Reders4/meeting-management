namespace MeetingManagement.Domain.Models
{
    public class Meeting
    {
        public uint Id { get; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeOnly NoticeTime { get; set; }
        public List<Participant> Participants { get; set; }

        private static uint _counter;

        public Meeting(DateOnly date, TimeOnly startTime, TimeOnly endTime, TimeOnly noticeTime, List<Participant> participants)
        {
            _counter++;
            Id = _counter;

            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            NoticeTime = noticeTime;
            Participants = participants;
        }

        public override string ToString()
        {
            return $"Встреча: {Date} начнется в {StartTime}, закончится в {EndTime}, уведомить в {NoticeTime}. \nИдентификатор встречи: {Id} \nУчастники: {ViewParticipant()}";
        }

        private string ViewParticipant()
        {
            var result = string.Empty;

            if (Participants.Count == 1)
            {
                result = Participants[0].ToString();
            }
            else
            {
                foreach (var item in Participants)
                {
                    if (Participants.Last() == item)
                    {
                        result += item.ToString();
                    }
                    else
                    {
                        result += item.ToString() + ", ";
                    }
                    
                }
            }
            

            return result;
        }
    }
}
