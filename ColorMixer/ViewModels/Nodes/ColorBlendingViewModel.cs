using System.Windows.Media;
using ColorMixer.ViewModels.Editors;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class ColorBlendingViewModel : NodeViewModel
    {
        public ValueNodeInputViewModel<Color?> Input1 { get; }
        public ValueNodeInputViewModel<Color?> Input2 { get; }
        public ValueNodeOutputViewModel<Color?> Output { get; }

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

        protected static byte IntToByte(int v)
        {
            return System.Convert.ToByte((System.Math.Min(v, byte.MaxValue)));
        }

        protected static Color MuliplyColor(Color color1, Color color2)
        {
            return Color.FromScRgb(color1.ScA * color2.ScA,
                color1.ScR * color2.ScR,
                color1.ScG * color2.ScG,
                color1.ScB * color2.ScB);
        }


        public ColorBlendingViewModel()
        {
            Input1 = new ValueNodeInputViewModel<Color?>
            {
                Name = "A",
                Editor = new ColorEditorViewModel()
            };
            Inputs.Add(Input1);

            Input2 = new ValueNodeInputViewModel<Color?>
            {
                Name = "B",
                Editor = new ColorEditorViewModel()
            };
            Inputs.Add(Input2);

            Output = new ValueNodeOutputViewModel<Color?>
            {
                Name = "A B",
                Editor = ValueEditor,
            };

            this.WhenAnyValue(vm => vm.Output.CurrentValue)
                .BindTo(this, vm => vm.ColorValue);

            Outputs.Add(Output);
        }
    }
}
