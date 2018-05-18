using System.Linq;
using System.Reactive.Linq;
using ColorMixer.ViewModels.Nodes;
using ColorMixer.Views;
using NodeNetwork;
using NodeNetwork.Toolkit;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace ColorMixer.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        static MainViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
        }

        public NodeListViewModel ListViewModel { get; } = new NodeListViewModel();
        public NetworkViewModel NetworkViewModel { get; } = new NetworkViewModel();

        private string _valueLabel;
        public string ValueLabel
        {
            get => _valueLabel;
            set => this.RaiseAndSetIfChanged(ref _valueLabel, value);
        } 

        public MainViewModel()
        {
            ListViewModel.AddNodeType(() => new ConstantNodeViewModel());
            ListViewModel.AddNodeType(() => new AdditiveBlendingViewModel());
            ListViewModel.AddNodeType(() => new MultiplicativeBlendingViewModel());
            ListViewModel.AddNodeType(() => new X2MultiplicativeBlendingViewModel());
            ListViewModel.AddNodeType(() => new AlphaBlendingViewModel());

            OutputNodeViewModel output = new OutputNodeViewModel();
            NetworkViewModel.Nodes.Add(output);

            NetworkViewModel.Validator = network =>
            {
                var containsLoops = GraphAlgorithms.FindLoops(network).Any();
                return containsLoops ? new NetworkValidationResult(false, false, new ErrorMessageViewModel("Network contains loops!")) : new NetworkValidationResult(true, true, null);
            };
            
            output.ResultInput.ValueChanged
                .Select(v => (NetworkViewModel.LatestValidation?.IsValid ?? true) ? v.ToString() : "Error")
                .BindTo(this, vm => vm.ValueLabel);
        }
    }
}
