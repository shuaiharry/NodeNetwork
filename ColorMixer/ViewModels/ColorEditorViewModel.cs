using System.Windows.Media;
using ColorMixer.Views;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace ColorMixer.ViewModels.Editors
{
    public class ColorEditorViewModel : ValueEditorViewModel<Color?>
    {
        static ColorEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new ColorEditorView(), typeof(IViewFor<ColorEditorViewModel>));
        }
        
        private Color _colorValue;
        public Color ColorValue
        {
            get => _colorValue;
            set =>this.RaiseAndSetIfChanged(ref _colorValue, value);
        }

        public ColorEditorViewModel()
        {
            this.WhenAnyValue(vm => vm.ColorValue)
                .BindTo(this, vm => vm.Value);
        }
    }
}
