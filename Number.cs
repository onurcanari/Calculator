using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class Number : DynamicText {


        public override void Append(string s) {
            if(number.Equals("0"))
                number="";
            number+=s;
            OnPropertyChanged();
        }
        public override void Delete() {
            if(number.Equals("0")) {
                return;
            }
            try {
                number=number.Remove(_number.Length-1, 1);
            } catch(ArgumentOutOfRangeException) {
                Debug.WriteLine("daha fazla silinemez!");
            }
            if(String.IsNullOrEmpty(number))
                number="0";
            OnPropertyChanged();
        }
    }
}
