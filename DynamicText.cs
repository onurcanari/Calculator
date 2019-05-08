using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    abstract class DynamicText : INotifyPropertyChanged {
        protected string _text;
        public string text{
            get{ return _text; }
            set {
                _text=value;
                OnPropertyChanged("");
            }
        }

        public DynamicText() {
            text="0";
        }
        
        public abstract void Append(string s);
        public abstract void Delete();
        public abstract void Clear();
        
        public override string ToString() {
            return text;
        }
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
