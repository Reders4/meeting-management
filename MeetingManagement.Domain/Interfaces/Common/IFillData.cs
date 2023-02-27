using MeetingManagement.Domain.Models;

namespace MeetingManagement.Domain.Interfaces.Common
{
    public interface IFillData
    {
        public DateOnly FillDate();
        public TimeOnly FillTime(string message, DateOnly date);
        public List<Participant> FillParticipants(List<Participant> participants);
    }
}
