using DevExpress.Xpf.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace DXGridThreads
{
    public class ViewModel : ViewModelBase {

        static object SyncRoot = new object();
        
        protected Timer timer;
        protected Random random = new Random();

        public ICustomService CustomService { get { return this.GetService<ICustomService>(); } }

        protected void TimerCallback(object state) {
            lock(SyncRoot) {
                if(this.CustomService != null) {
                    this.CustomService.BeginUpdate();

                    foreach(DataItem item in Source) {
                        item.Value = random.Next(100);
                    }

                    this.CustomService.EndUpdate();
                }
            }
        }

        public ViewModel() {
            timer = new Timer(TimerCallback, null, 1000, 1000);
        }

        ObservableCollection<DataItem> _Source;
        public ObservableCollection<DataItem> Source {
            get {
                if(this._Source == null) {
                    this._Source = new ObservableCollection<DataItem>();
                    for(int i = 0; i < 100; i++) {
                        this._Source.Add(new DataItem() { Name = "Name" + i.ToString(), Value = i });
                    }
                }
                return this._Source;
            }
            
        }

    }
}
