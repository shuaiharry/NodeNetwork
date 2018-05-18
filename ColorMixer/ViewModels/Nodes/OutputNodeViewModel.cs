using System.Windows.Media;
using ColorMixer.ViewModels.Editors;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class OutputNodeViewModel : NodeViewModel
    {
        static OutputNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<OutputNodeViewModel>));
        }

        public ValueNodeInputViewModel<Color?> ResultInput { get; }

        public ColorEditorViewModel ValueEditor { get; } = new ColorEditorViewModel();

        private Color _colorValue;
        public Color ColorValue
        {
            get => _colorValue;
            set
            {
                this.RaiseAndSetIfChanged(ref _colorValue, value);
                ValueEditor.ColorValue = value;
            }
        }

        public OutputNodeViewModel()
        {
            Name = "Output";

            CanBeRemovedByUser = false;

            ResultInput = new ValueNodeInputViewModel<Color?>
            {
                Name = "Value",
                Editor = ValueEditor,
                HideEditorIfConnected = false
            };

            this.WhenAnyValue(vm => vm.ResultInput.Value)
                .BindTo(this, vm => vm.ColorValue);

            Inputs.Add(ResultInput);
        }
    }
}
