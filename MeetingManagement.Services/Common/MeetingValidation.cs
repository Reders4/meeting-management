using MeetingManagement.Domain.Common;
using MeetingManagement.Domain.Models;

namespace MeetingManagement.Services.Common
{
    internal class MeetingValidation
    {
        public void AdditionValidation(Meeting meeting, List<Meeting> meetings)
        {
            var today = DateTimeSupport.GetTodayDate();
            DateValidation(meeting.Date, today);
            TimeValidation(meeting.StartTime, meeting.EndTime, ExceptionMessage.StartTimeMoreThenEndTime);
            TimeValidation(meeting.NoticeTime, meeting.StartTime, ExceptionMessage.NoticeTimeMoreThenStartTime);
            TimeValidation(DateTimeSupport.GetTimeNow(), meeting.NoticeTime, ExceptionMessage.NoticeTimeLessThenNowTime);
            TimeValidation(meeting.StartTime, meeting.Date, today, ExceptionMessage.TimeIsPassed);
            IntersectValidation(meeting.Date, meeting.StartTime, meeting.EndTime, meetings);
        }

        public Meeting UpdatingValidation(Meeting meetingToUpdate, Meeting newMeetingData, List<Meeting> meetings)
        {
            var today = DateTimeSupport.GetTodayDate();

            IntersectValidationForUpdate(meetingToUpdate, newMeetingData, meetings);

            if (!IsDefault(newMeetingData.Date))
            {
                DateValidation(newMeetingData.Date, today);
                if (IsDefault(newMeetingData.StartTime))
                {
                    TimeValidation(meetingToUpdate.StartTime, newMeetingData.Date, today, ExceptionMessage.TimeIsPassed);
                }
                meetingToUpdate.Date = newMeetingData.Date;
            }
            if (!IsDefault(newMeetingData.StartTime))
            {
                if (IsDefault(newMeetingData.EndTime))
                {
                    TimeValidation(newMeetingData.StartTime, meetingToUpdate.EndTime, ExceptionMessage.StartTimeMoreThenEndTime);
                }
                TimeValidation(newMeetingData.StartTime, meetingToUpdate.Date, today, ExceptionMessage.TimeIsPassed);
                meetingToUpdate.StartTime = newMeetingData.StartTime;
            }
            if (!IsDefault(newMeetingData.EndTime))
            {
                TimeValidation(meetingToUpdate.StartTime, newMeetingData.EndTime, ExceptionMessage.StartTimeMoreThenEndTime);
                meetingToUpdate.EndTime = newMeetingData.EndTime;
            }
            if (!IsDefault(newMeetingData.NoticeTime))
            {
                TimeValidation(newMeetingData.NoticeTime, meetingToUpdate.StartTime, ExceptionMessage.NoticeTimeMoreThenStartTime);
                TimeValidation(DateTimeSupport.GetTimeNow(), newMeetingData.NoticeTime, ExceptionMessage.NoticeTimeLessThenNowTime);
                meetingToUpdate.NoticeTime = newMeetingData.NoticeTime;
            }
            if (!IsDefault(newMeetingData.Participants))
            {
                meetingToUpdate.Participants = newMeetingData.Participants;
            }
            
            return meetingToUpdate;
        }

        private bool IsDefault<T>(T obj) => (obj == null) ? (default(T) == null) : obj.Equals(default(T));

        private void IntersectValidationForUpdate(Meeting meetingToUpdate, Meeting newMeetingData, List<Meeting> meetings)
        {
            if (!IsDefault(newMeetingData.Date) &&
                !IsDefault(newMeetingData.StartTime) &&
                !IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(newMeetingData.Date, newMeetingData.StartTime, newMeetingData.EndTime, meetings);
            }
            if (!IsDefault(newMeetingData.Date) &&
                IsDefault(newMeetingData.StartTime) &&
                !IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(newMeetingData.Date, meetingToUpdate.StartTime, newMeetingData.EndTime, meetings);
            }
            if (!IsDefault(newMeetingData.Date) &&
                !IsDefault(newMeetingData.StartTime) &&
                IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(newMeetingData.Date, newMeetingData.StartTime, meetingToUpdate.EndTime, meetings);
            }
            if (!IsDefault(newMeetingData.Date) &&
                IsDefault(newMeetingData.StartTime) &&
                IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(newMeetingData.Date, meetingToUpdate.StartTime, meetingToUpdate.EndTime, meetings);
            }
            if (IsDefault(newMeetingData.Date) &&
                !IsDefault(newMeetingData.StartTime) &&
                !IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(meetingToUpdate.Date, newMeetingData.StartTime, newMeetingData.EndTime, meetings);
            }
            if (IsDefault(newMeetingData.Date) &&
                !IsDefault(newMeetingData.StartTime) &&
                IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(meetingToUpdate.Date, newMeetingData.StartTime, meetingToUpdate.EndTime, meetings);
            }
            if (IsDefault(newMeetingData.Date) &&
                IsDefault(newMeetingData.StartTime) &&
                !IsDefault(newMeetingData.EndTime))
            {
                IntersectValidation(meetingToUpdate.Date, meetingToUpdate.StartTime, newMeetingData.EndTime, meetings);
            }


        }

        private void IntersectValidation(DateOnly date, TimeOnly startTime, TimeOnly endTime, List<Meeting> meetings)
        {
            var isMeetingIntersected = meetings.Where(x => x.Date == date)
                .Any(x =>
                (x.StartTime <= startTime && startTime <= x.EndTime) ||
                (startTime <= x.StartTime && x.StartTime <= endTime)
            );

            if (isMeetingIntersected)
            {
                throw new Exception("Встреча пересекается с существующей");
            }
        }

        private void TimeValidation(TimeOnly timeAt, TimeOnly timeBy, string message)
        {
            if (timeAt >= timeBy)
            {
                throw new ArgumentException(message);
            }
        }

        private void TimeValidation(TimeOnly startTime, DateOnly meetingDate, DateOnly today, string message)
        {
            if (startTime < DateTimeSupport.GetTimeNow() && meetingDate == today)
            {
                throw new ArgumentException(message);
            }
        }

        private void DateValidation(DateOnly date, DateOnly today)
        {
            if (date < today)
            {
                throw new ArgumentException(ExceptionMessage.DateIsPassed);
            }
        }
    }
}
