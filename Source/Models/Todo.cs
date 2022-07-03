namespace TodoList.Models
{
    public class Todo
    {
        public string Text { get; private set; }

        public Todo(string text)
        {
            Text = text;
        }
    }
}