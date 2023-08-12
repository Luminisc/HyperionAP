using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;

namespace HSAT.Controls.Behaviors
{
    /// <summary>Allow to handle mouse scroll events</summary>
    public class MouseZoomBehavior : Behavior<VisualElement>
    {
        /// <summary>
        /// Backing BindableProperty for the <see cref="ZoomCommand"/> property.
        /// </summary>
        public static readonly BindableProperty ZoomCommandProperty =
            BindableProperty.Create(nameof(ZoomCommand), typeof(ICommand), typeof(MouseZoomBehavior));

        /// <summary>
        /// The Command that should be executed when the event configured with <see cref="EventName"/> is triggered. This is a bindable property.
        /// </summary>
        public ICommand ZoomCommand
        {
            get => (ICommand)GetValue(ZoomCommandProperty);
            set => SetValue(ZoomCommandProperty, value);
        }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
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
            ZoomCommand?.Execute(e);
        }
    }
}
