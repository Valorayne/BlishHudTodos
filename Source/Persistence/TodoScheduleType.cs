namespace Todos.Source.Models
{
    public enum TodoScheduleType
    {
        NoReset = -1,
        DailyServer = 0,
        WeeklyServer = 1,
        LocalTime = 2,
        Duration = 3
    }
}