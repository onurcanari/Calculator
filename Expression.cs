using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class Expression : DynamicText{

        public Expression() {
            text="";
        }
        public override void Append(string s) {
            text+=s;
            OnPropertyChanged();
        }
        public override void Delete() {
            if(text.Equals("")) {
                return;
            }
            try {
                text=text.Remove(_text.Length-1, 1);
            } catch(ArgumentOutOfRangeException) {
                Debug.WriteLine("daha fazla silinemez!");
            }
            if(String.IsNullOrEmpty(text))
                text="0";
            OnPropertyChanged();
        }
        public override void Clear() {
            text="";
        }
    }
}
