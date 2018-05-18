﻿using System.Windows;
using ColorMixer.ViewModels;
using ReactiveUI;

namespace ColorMixer.Views
{
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
            typeof(MainViewModel), typeof(MainWindow), new PropertyMetadata(null));

        public MainViewModel ViewModel
        {
            get => (MainViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new MainViewModel();

            this.OneWayBind(ViewModel, vm => vm.ListViewModel, v => v.nodeList.ViewModel);
            this.OneWayBind(ViewModel, vm => vm.NetworkViewModel, v => v.viewHost.ViewModel);
            this.OneWayBind(ViewModel, vm => vm.ValueLabel, v => v.valueLabel.Content);
        }
    }
}