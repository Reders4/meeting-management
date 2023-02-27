using MeetingManagement.Domain.Models;

namespace MeetingManagement.Domain.Interfaces.Services
{
    public interface IMeetingService
    {
        public event Action<string> Notify;
        public void AddMeeting(Meeting meeting);
        public void RemoveMeeting(uint meetingId);
        public Meeting UpdateMeeting(uint meetingId, Meeting meeting);
        public string GetMeetingsByDate(DateOnly date);
        public Meeting GetMeetingById(uint meetingId);
        public void ExportMeetings(DateOnly date);
        public void MeetingNotify();
    }
}
