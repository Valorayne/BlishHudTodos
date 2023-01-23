using Blish_HUD;
using Blish_HUD.Input;
using Todos.Source.Utils.Reactive;

namespace Todos.Source.Utils
{
    public static class MouseService
    {
        public enum ButtonState
        {
            Pressed,
            HeldDown,
            Released,
            Untouched
        }

        public static Variable<ButtonState> LeftButton = new Variable<ButtonState>(ButtonState.Untouched);

        public static void Initialize()
        {
            GameService.Input.Mouse.LeftMouseButtonPressed += OnLeftMouseButtonPressed;
            GameService.Input.Mouse.LeftMouseButtonReleased += OnLeftMouseButtonReleased;
        }

        public static void Update()
        {
            if (LeftButton.Value == ButtonState.Pressed) LeftButton.Value = ButtonState.HeldDown;
            if (LeftButton.Value == ButtonState.Pressed) LeftButton.Value = ButtonState.Untouched;
        }

        private static void OnLeftMouseButtonPressed(object sender, MouseEventArgs e)
        {
            LeftButton.Value = ButtonState.Pressed;
        }

        private static void OnLeftMouseButtonReleased(object sender, MouseEventArgs e)
        {
            LeftButton.Value = ButtonState.Released;
        }

        public static void Dispose()
        {
            GameService.Input.Mouse.LeftMouseButtonPressed -= OnLeftMouseButtonPressed;
            GameService.Input.Mouse.LeftMouseButtonReleased -= OnLeftMouseButtonReleased;

            LeftButton.Dispose();
            LeftButton = new Variable<ButtonState>(ButtonState.Untouched);
        }
    }
}