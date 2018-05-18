using System.Reactive.Linq;
using System.Windows.Media;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class MultiplicativeBlendingViewModel : ColorBlendingViewModel
    {
        static MultiplicativeBlendingViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<MultiplicativeBlendingViewModel>));
        }

        public MultiplicativeBlendingViewModel()
        {
            Name = "Multi Blending";

            var sum = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null ? MuliplyColor(Input1.Value.GetValueOrDefault(),Input2.Value.GetValueOrDefault()) : (Color?) null);

            Output.Name = "multi";
            Output.Value = sum;
        }

    }
}
