using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class Number : INotifyPropertyChanged {
        private StringBuilder _number;
        public StringBuilder number{
            get{ return _number; }
            set {
                _number=value;
                OnPropertyChanged("");
            }
        }
        public void Append(char c) {
            _number.Append(c);
            OnPropertyChanged("append char");
        }
        public void Append(string s) {
            _number.Append(s);
            OnPropertyChanged("append string");
        }
        public void Delete() {
            _number.Remove(_number.Length-1, 1);
            OnPropertyChanged("delete");
        }
        
        public override string ToString() {
            return _number.ToString();
        }
        protected virtual void OnPropertyChanged(string number) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(number));
        }
        
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
