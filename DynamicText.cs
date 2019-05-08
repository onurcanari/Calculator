using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    abstract class DynamicText : INotifyPropertyChanged {
        protected string _number;
        public string number{
            get{ return _number; }
            set {
                _number=value;
                OnPropertyChanged("");
            }
        }

        public DynamicText() {
            number="0";
        }

        public abstract void Append(string s);
        public abstract void Delete();
        
        public override string ToString() {
            return number.ToString();
        }
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
