using System.Reactive.Linq;
using System.Windows.Media;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class X2MultiplicativeBlendingViewModel : ColorBlendingViewModel
    {
        static X2MultiplicativeBlendingViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<X2MultiplicativeBlendingViewModel>));
        }

        public X2MultiplicativeBlendingViewModel()
        {
            Name = "2X Multi Blending";

            var sum = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null ? Input2.Value.GetValueOrDefault() + MuliplyColor(Input1.Value.GetValueOrDefault(),Input2.Value.GetValueOrDefault()) : (Color?) null);

            Output.Name = "2X multi";
            Output.Value = sum;
        }

    }
}
