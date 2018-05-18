using System.Reactive.Linq;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class AdditiveBlendingViewModel : ColorBlendingViewModel
    {
        static AdditiveBlendingViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<AdditiveBlendingViewModel>));
        }
        
        public AdditiveBlendingViewModel()
        {
            Name = "Additive Blending";

            var sum = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null ? Input1.Value + Input2.Value : null);

            Output.Name = "A + B";
            Output.Value = sum;
        }
    }
}
