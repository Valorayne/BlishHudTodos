namespace Todos.Source.Models
{
    public enum TodoScheduleType
    {
        DailyServer = 0,
        WeeklyServer = 1,
        LocalTime = 2
    }
    
    public static class TodoScheduleTypeExtensions 
    {
        public const string NO_RESET = "Never Resets";
        public const string DAILY_SERVER_RESET = "Daily Server Reset";
        public const string WEEKLY_SERVER_RESET = "Weekly Server Reset";
        public const string LOCAL_TIME = "Local Time";

        public static TodoScheduleType? FromDropdownEntry(this string entry)
        {
            switch (entry)
            {
                case DAILY_SERVER_RESET: return TodoScheduleType.DailyServer;
                case WEEKLY_SERVER_RESET: return TodoScheduleType.WeeklyServer;
                case LOCAL_TIME: return TodoScheduleType.LocalTime;
                default: return null;
            }
        }
        
        public static string ToDropdownEntry(this TodoScheduleType type)
        {
            switch (type)
            {
                case TodoScheduleType.DailyServer: return DAILY_SERVER_RESET;
                case TodoScheduleType.WeeklyServer: return WEEKLY_SERVER_RESET;
                case TodoScheduleType.LocalTime: return LOCAL_TIME;
                default: return NO_RESET;
            }
        }

        public static string GetTooltip(this string entry)
        {
            switch (entry)
            {
                case DAILY_SERVER_RESET: return "This task will reset every day at 0:00 UTC";
                case WEEKLY_SERVER_RESET: return "This task will reset every Monday, 7:30 UTC";
                case LOCAL_TIME: return "This task will reset every day at the local time specified below";
                default: return "This task will not reset automatically";
            }
        }
    }
}