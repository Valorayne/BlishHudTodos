using Blish_HUD.Content;
using Blish_HUD.Controls;

namespace TodoList.Components
{
    public class TodoCollapseArrow : Panel
    {
        private readonly Image _arrow;
        private bool _expanded;
        
        private readonly AsyncTexture2D _downArrow;
        private readonly AsyncTexture2D _rightArrow;
        
        public TodoCollapseArrow()
        {
            Height = HEADER_HEIGHT;
            Width = HEADER_HEIGHT;
            
            _downArrow = Resources.GetTexture(Textures.CollapseArrowDown);
            _rightArrow = Resources.GetTexture(Textures.CollapseArrowRight);
            
            _arrow = new Image(_rightArrow)
            {
                Parent = this
            };
        }

        public bool Expanded
        {
            get => _expanded;
            set { 
                _arrow.Texture = value ? _downArrow : _rightArrow;
                _expanded = value;
            }
        }

        protected override void DisposeControl()
        {
            _arrow.Dispose();
            base.DisposeControl();
        }
    }
}