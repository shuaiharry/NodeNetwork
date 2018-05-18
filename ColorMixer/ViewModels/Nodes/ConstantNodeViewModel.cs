using System.Windows.Media;
using ColorMixer.ViewModels.Editors;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class ConstantNodeViewModel : NodeViewModel
    {
        static ConstantNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<ConstantNodeViewModel>));
        }

        public ColorEditorViewModel ValueEditor { get; } = new ColorEditorViewModel();

        public ValueNodeOutputViewModel<Color?> Output { get; }

        public ConstantNodeViewModel()
        {
            Name = "ConstantColor";
            
            Output = new ValueNodeOutputViewModel<Color?>
            {
                Name = "Color",
                Editor = ValueEditor,
                Value = this.WhenAnyValue(vm => vm.ValueEditor.Value)
            };
            Outputs.Add(Output);
        }
    }
}
