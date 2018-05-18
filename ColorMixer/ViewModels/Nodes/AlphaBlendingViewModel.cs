using System.Reactive.Linq;
using System.Windows.Media;
using NodeNetwork.Views;
using ReactiveUI;

namespace ColorMixer.ViewModels.Nodes
{
    public class AlphaBlendingViewModel : ColorBlendingViewModel
    {
        static AlphaBlendingViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<AlphaBlendingViewModel>));
        }

        private static Color CalcAlphaBlend(Color src, Color dest)
        {
            return src * src.ScA + dest * (1 - src.ScA);
        }

        public AlphaBlendingViewModel()
        {
            Name = "Alpha Blending";

            var sum = this.WhenAnyValue(vm => vm.Input1.Value, vm => vm.Input2.Value)
                .Select(_ => Input1.Value != null && Input2.Value != null ? CalcAlphaBlend(Input1.Value.GetValueOrDefault(), Input2.Value.GetValueOrDefault()) : (Color?) null);

            Output.Name = "Input1*A + Input2*(1-A)";
            Output.Value = sum;
        }
    }
}
