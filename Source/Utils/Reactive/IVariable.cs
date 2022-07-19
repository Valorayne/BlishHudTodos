namespace Todos.Source.Utils
{
    public interface IVariable<T> : IProperty<T>
    {
        new T Value { get; set; }
    }
}