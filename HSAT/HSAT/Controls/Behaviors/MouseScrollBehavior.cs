using CommunityToolkit.Maui.Behaviors;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;

namespace HSAT.Controls.Behaviors
{
    /// <summary>Allow to handle mouse scroll events</summary>
    public class MouseScrollBehavior : BaseBehavior<VisualElement>
    {
        public static readonly BindableProperty ScrollCommandProperty =
            BindableProperty.Create(nameof(ScrollCommand), typeof(ICommand), typeof(MouseScrollBehavior));

        public ICommand ScrollCommand
        {
            get => (ICommand)GetValue(ScrollCommandProperty);
            set => SetValue(ScrollCommandProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            BindingContext = bindable.BindingContext;
            bindable.HandlerChanged += Bindable_HandlerChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
#if WINDOWS
            bindable.HandlerChanged -= Bindable_HandlerChanged;
            var view = bindable.Handler.PlatformView as UIElement;
            view.PointerWheelChanged -= ExecuteCommand;
#endif
        }

        private void Bindable_HandlerChanged(object sender, EventArgs e)
        {
#if WINDOWS
            var view = ((VisualElement)sender).Handler.PlatformView as UIElement;
            view.PointerWheelChanged -= ExecuteCommand;
            view.PointerWheelChanged += ExecuteCommand;
#endif
        }

        protected void ExecuteCommand(object sender, PointerRoutedEventArgs e)
        {
            ScrollCommand?.Execute(e);
        }
    }
}
