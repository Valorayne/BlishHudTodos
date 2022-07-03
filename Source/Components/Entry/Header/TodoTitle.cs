using Blish_HUD;
using Blish_HUD.Controls;

namespace TodoList.Components
{
    public class TodoTitle : Panel
    {
        private readonly Label _label;
        
        public TodoTitle(Settings settings)
        {
            Height = HEADER_HEIGHT;
            _label = new Label
            {
                Parent = this,
                StrokeText = true,
                Text = "Bla Blub",
            };
        }

        protected override void DisposeControl()
        {
            _label.Dispose();
            base.DisposeControl();
        }
    }
}