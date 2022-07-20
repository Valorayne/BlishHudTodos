using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Todos.Source.Models;
using Todos.Source.Utils;
using Todos.Source.Utils.Subscriptions;

namespace Todos.Source.Components.Entry.Content
{
    public class TodoCheckbox : Panel
    {
        private readonly Point SIZE = new Point(32, 32);
        private readonly Point OFFSET = new Point(2, 2);

        private readonly SettingsModel _settings;
        private readonly TodoScheduleModel _schedule;
        private readonly HoverSubscription _hoverSubscription;

        public TodoCheckbox(SettingsModel settings, TodoScheduleModel schedule)
        {
            _settings = settings;
            _schedule = schedule;
            
            Width = HEADER_HEIGHT;
            Height = HEADER_HEIGHT;

            var background = new Image(Resources.GetTexture(Textures.Empty)) { Parent = this, Location = OFFSET, Size = SIZE };
            var hovered = new Image(Resources.GetTexture(Textures.Empty)) { Parent = this, Location = OFFSET, Size = SIZE, Visible = false };
            var @checked = new Image(Resources.GetTexture(Textures.Empty)) { Parent = this, Location = OFFSET, Size = SIZE };

            schedule.IsDone.Subscribe(this, isDone => @checked.Visible = isDone);
            schedule.CheckboxTooltip.Subscribe(this, tooltip => @checked.BasicTooltipText = tooltip);
            settings.CheckboxType
                .Subscribe(this, checkboxType => background.Texture = checkboxType.GetBackgroundImage())
                .Subscribe(this, checkboxType => hovered.Texture = checkboxType.GetHoveredImage())
                .Subscribe(this, checkboxType => @checked.Texture = checkboxType.GetCheckedImage());

            _hoverSubscription = new HoverSubscription(this, () => hovered.Visible = true, () => hovered.Visible = false);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            _schedule.ToggleDone();
            base.OnClick(e);
        }

        protected override void DisposeControl()
        {
            _schedule.IsDone.Unsubscribe(this);
            _schedule.CheckboxTooltip.Unsubscribe(this);
            _settings.CheckboxType.Unsubscribe(this);
            _hoverSubscription.Dispose();
            base.DisposeControl();
        }
    }
}