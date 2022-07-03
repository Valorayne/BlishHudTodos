namespace TodoList.Models
{
    public class Todo
    {
        public string Text { get; private set; }

        public Todo()
        {
            Text = "This is some test content";
        }
    }
}