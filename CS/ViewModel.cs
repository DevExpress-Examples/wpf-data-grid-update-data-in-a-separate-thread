using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace Model {
    public class ViewModel : INotifyPropertyChanged {
        static object SyncRoot = new object();
        Timer timer;
        Random random = new Random();

        void TimerCallback(object state) {
            lock(SyncRoot) {
                OnAsyncProcessingStarted();
                foreach(DataItem item in Source) {
                    item.Value = random.Next(100);
                }
                OnAsyncProcessingCompleted();
            }
        }

        public ViewModel() {
            source = DataItem.Data;
            timer = new Timer(TimerCallback, null, 1000, 1000);
        }

        ObservableCollection<DataItem> source;
        public ObservableCollection<DataItem> Source {
            get { return source; }
            set {
                if(source == value) return;
                source = value;
                OnPropertyChanged("Source");
            }
        }

        public event EventHandler AsyncProcessingStarted;
        public event EventHandler AsyncProcessingCompleted;

        private void OnAsyncProcessingStarted() {
            if(AsyncProcessingStarted != null) {
                AsyncProcessingStarted(this, EventArgs.Empty);
            }
        }
        private void OnAsyncProcessingCompleted() {
            if(AsyncProcessingCompleted != null) {
                AsyncProcessingCompleted(this, EventArgs.Empty);
            }
        }

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class DataItem : INotifyPropertyChanged {
        static ObservableCollection<DataItem> data;
        public static ObservableCollection<DataItem> Data {
            get {
                if(data == null) {
                    data = new ObservableCollection<DataItem>();
                    for(int i = 0; i < 100; i++) {
                        data.Add(new DataItem() { Name = "Name" + i.ToString(), Value = i });
                    }
                }
                return data;
            }
        }

        public string Name { get; set; }
        volatile int valueField;
        public int Value {
            get { return valueField; }
            set {
                valueField = value;
                OnPropertyChanged("Value");
            }
        }
        public override string ToString() {
            return Name;
        }

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
