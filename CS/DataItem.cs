using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXGridThreads {
    public class DataItem : INotifyPropertyChanged {


        protected string _Name;

        public string Name {
            get {
                return this._Name;
            }

            set {
                if(this._Name != value) {
                    this._Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }


        protected volatile int _Value;

        public int Value {
            get {
                return this._Value;
            }

            set {
                if(this._Value != value) {
                    this._Value = value;
                    this.OnPropertyChanged("Value");
                }
            }
        }


        public override string ToString() {
            return Name;
        }


        public void OnPropertyChanged(string info) {
            if(this.PropertyChanged != null) {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
