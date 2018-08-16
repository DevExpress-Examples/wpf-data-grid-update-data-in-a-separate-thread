// Developer Express Code Central Example:
// How to display data which is being updated on another thread
// 
// Let's suppose that your data is being updated on another thread, by the timer in
// this example. You should take a special action to correctly reflect those
// changes in the grid - wrap them inside BeginDataUpdate/EndDataUpdate
using System.Windows;

namespace DXGridThreads {
    public partial class MainWindow : Window {
        
        public MainWindow() {
            InitializeComponent();
        }
    }
}
