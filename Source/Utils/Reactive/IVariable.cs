namespace Todos.Source.Utils.Reactive
{
    public interface IVariable<T> : IProperty<T>
    {
        new T Value { get; set; }
    }
}