namespace TodoList.Models
{
    public enum TodoScheduleType
    {
        DailyServer = 0,
        WeeklyServer = 1,
        CustomLocal = 2
    }
    
    public static class TodoScheduleTypeExtensions 
    {
        public const string NO_RESET = "Never Resets";
        public const string DAILY_SERVER_RESET = "Daily Server Reset";
        public const string WEEKLY_SERVER_RESET = "Weekly Server Reset";
        public const string CUSTOM_LOCAL_TIME = "Custom Local Time";

        public static TodoScheduleType? FromDropdownEntry(this string entry)
        {
            switch (entry)
            {
                case DAILY_SERVER_RESET: return TodoScheduleType.DailyServer;
                case WEEKLY_SERVER_RESET: return TodoScheduleType.WeeklyServer;
                case CUSTOM_LOCAL_TIME: return TodoScheduleType.CustomLocal;
                default: return null;
            }
        }
        
        public static string ToDropdownEntry(this TodoScheduleType type)
        {
            switch (type)
            {
                case TodoScheduleType.DailyServer: return DAILY_SERVER_RESET;
                case TodoScheduleType.WeeklyServer: return WEEKLY_SERVER_RESET;
                case TodoScheduleType.CustomLocal: return CUSTOM_LOCAL_TIME;
                default: return NO_RESET;
            }
        }
    }
}