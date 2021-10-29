using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using DevExpress.Mvvm.UI;

namespace DXGridThreads {
    public interface IGridUpdateService {
        void BeginUpdate();
        void EndUpdate();
    }

    public class GridUpdateService : ServiceBase, IGridUpdateService {
        public static readonly DependencyProperty GridControlProperty =
            DependencyProperty.Register("GridControl", typeof(GridControl), typeof(GridUpdateService), new PropertyMetadata(null));

        public GridControl GridControl {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }

        public void BeginUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if (GridControl != null) {
                    GridControl.BeginDataUpdate();
                }
            }));
        }

        public void EndUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if (GridControl != null) {
                    GridControl.EndDataUpdate();
                }
            }));
        }
    }
}