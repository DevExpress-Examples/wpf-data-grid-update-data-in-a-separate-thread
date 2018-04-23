using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using DevExpress.Xpf.Mvvm.UI;

namespace DXGridThreads
{

    public interface ICustomService {
        void BeginUpdate();
        void EndUpdate();
    }

    public class CustomService : ServiceBase , ICustomService
    {

        public GridControl GridControl {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }

        public static readonly DependencyProperty GridControlProperty =
            DependencyProperty.Register("GridControl", typeof(GridControl), typeof(CustomService), new PropertyMetadata(null));

        public void BeginUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if(this.GridControl != null) {
                    this.GridControl.BeginDataUpdate();
                }
            }));
        }

        public void EndUpdate() {
            Dispatcher.Invoke(new Action(() => {
                if(this.GridControl != null) {
                    this.GridControl.EndDataUpdate();
                }
            }));
        }
    }
}
