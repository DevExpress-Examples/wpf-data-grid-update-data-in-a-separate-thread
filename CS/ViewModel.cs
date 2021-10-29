using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace DXGridThreads {
    public class DataItem : BindableBase {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public int Value { get { return GetValue<int>(); } set { SetValue(value); } }
    }

    public class ViewModel : ViewModelBase {
        public ObservableCollection<DataItem> Source { get; set; }
        public IGridUpdateService GridUpdateService { get { return GetService<IGridUpdateService>(); } }

        public ViewModel() {
            timer = new Timer(TimerCallback, null, 1000, 1000);
            Source = new ObservableCollection<DataItem>(GetData());
        }

        static object SyncRoot = new object();
        Timer timer;
        Random random = new Random();

        void TimerCallback(object state) {
            lock(SyncRoot) {
                if(GridUpdateService != null) {
                    GridUpdateService.BeginUpdate();
                    foreach(DataItem item in Source) {
                        item.Value = random.Next(100);
                    }
                    GridUpdateService.EndUpdate();
                }
            }
        }

        static IEnumerable<DataItem> GetData() {
            for(var i = 0; i < 100; ++i) {
                yield return new DataItem() { Name = $"Name {i}", Value = i };
            }
        }
    }
}
