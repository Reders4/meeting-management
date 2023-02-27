namespace MeetingManagement.Domain.Common
{
    //Почему-то Extension не работает
    public static class DateTimeSupport
    {
        public static DateOnly GetTodayDate() => DateOnly.FromDateTime(DateTime.Now);

        public static TimeOnly GetTimeNow() => TimeOnly.FromDateTime(DateTime.Now);
    }
}
