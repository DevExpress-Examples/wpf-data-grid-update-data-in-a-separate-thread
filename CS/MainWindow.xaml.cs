using System;
using System.Windows;
using System.Windows.Threading;
using Model;

namespace DXGridThreads {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ViewModel viewModel;

        public MainWindow() {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.AsyncProcessingStarted += new EventHandler(viewModel_AsyncProcessingStarted);
            viewModel.AsyncProcessingCompleted += new EventHandler(viewModel_AsyncProcessingCompleted);
            DataContext = viewModel;
        }

        void viewModel_AsyncProcessingStarted(object sender, EventArgs e) {
            grid.Dispatcher.Invoke(new Action(delegate { grid.BeginDataUpdate(); }), DispatcherPriority.DataBind);
        }
        void viewModel_AsyncProcessingCompleted(object sender, EventArgs e) {
            grid.Dispatcher.Invoke(new Action(delegate { grid.EndDataUpdate(); }), DispatcherPriority.DataBind);
        }
    }
}
