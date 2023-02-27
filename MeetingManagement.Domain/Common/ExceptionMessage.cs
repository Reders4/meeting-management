namespace MeetingManagement.Domain.Common
{
    public class ExceptionMessage
    {

        private const string _wrongNoticeTime = "Неверно заданно время уведомления о предстоящей встрече.\n";

        public const string StartTimeMoreThenEndTime = "Запланировать встречу невозможно. Время окончания не может быть меньше или равным времени начала.";
        public const string NoticeTimeMoreThenStartTime = _wrongNoticeTime + "Невозможно задать время уведомления после начала встречи.";
        public const string NoticeTimeLessThenNowTime = _wrongNoticeTime + "Невозможно задать время уведомления так как оно уже прошло.";
        public const string TimeIsPassed = "Встреча планируется заранее, указанное время уже прошло.";
        public const string DateIsPassed = "Встреча планируется заранее, указанная дата уже прошла.";
    }
}
