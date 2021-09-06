using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using DevExpress.Mvvm.UI;

namespace DXGridThreads {

    public interface ICustomService {
        void BeginUpdate();
        void EndUpdate();
    }

    public class CustomService : ServiceBase, ICustomService {
        public static readonly DependencyProperty GridControlProperty =
            DependencyProperty.Register("GridControl", typeof(GridControl), typeof(CustomService), new PropertyMetadata(null));

        public GridControl GridControl {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }
        protected GridControl ActualGridControl { get { return GridControl != null ? GridControl : AssociatedObject as GridControl; } }

        public void BeginUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if (ActualGridControl != null) {
                    ActualGridControl.BeginDataUpdate();
                }
            }));
        }

        public void EndUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if (ActualGridControl != null) {
                    ActualGridControl.EndDataUpdate();
                }
            }));
        }
    }
}