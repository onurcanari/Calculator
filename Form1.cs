using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Calculator {
    public partial class Form1 : MetroFramework.Forms.MetroForm {

        Button[] digitButtons;
        Button[] operatorButtons;
        Calculator calc;
        DynamicText number = new Number();
        DynamicText expression = new Expression();

        public Form1() {
            InitializeComponent();
            lblNumber.DataBindings.Add("Text", number, "text");
            lblExpression.DataBindings.Add("Text", expression, "text");
        }

        private void Form1_Load(object sender, EventArgs e) {
            calc=new Calculator(number, expression);
            digitButtons = new Button[]{ btnZero, btnOne, btnTwo, btnThree, btnFour, btnFive, btnSix, btnSeven, btnEight, btnNine};
            operatorButtons=new Button[] {btnAdd, btnSubtract, btnMultiply, btnDivide };
        }

        private void Digits_Click(object sender, EventArgs e) {
            for(int i = 0; i<digitButtons.Length; i++) {
                if((Button)sender ==digitButtons[i]) {
                    number.Append(calc.DIGITS[i]);
                    break;
                }
            }
        }

        private void Operators_Click(object sender, EventArgs e) {
            for(int i=0;i<operatorButtons.Length;i++) {
                if((Button)sender==operatorButtons[i]) {
                    expression.Append(String.Format("{0} {1} ",number.ToString(),calc.OPERATORS[i]));
                    break;
                }
            }
            number.Clear();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            /*
            if(e.KeyCode==Keys.D1 && e.KeyCode==Keys.NumPad1) One_Click(null, null);
            else if(e.KeyCode==Keys.D2 && e.KeyCode==Keys.NumPad2) Two_Click(null, null);
            else if(e.KeyCode==Keys.D3 && e.KeyCode==Keys.NumPad3) Three_Click(null, null);
            else if(e.KeyCode==Keys.D4 && e.KeyCode==Keys.NumPad4) Four_Click(null, null);
            else if(e.KeyCode==Keys.D5 && e.KeyCode==Keys.NumPad5) Five_Click(null, null);
            else if(e.KeyCode==Keys.D6 && e.KeyCode==Keys.NumPad6) Six_Click(null, null);
            else if(e.KeyCode==Keys.D7 && e.KeyCode==Keys.NumPad7) Seven_Click(null, null);
            else if(e.KeyCode==Keys.D8 && e.KeyCode==Keys.NumPad8) Eight_Click(null, null);
            else if(e.KeyCode==Keys.D9 && e.KeyCode==Keys.NumPad9) Nine_Click(null, null);
            */
        }

        private void Backspace(object sender, EventArgs e) {
            number.Delete();
        }

        private void FindFactorial(object sender, EventArgs e) {

        }


        private void Evaluate_Click(object sender, EventArgs e) {

            string oldExpression = expression.text;
            string newExpression = "";

            if(number.text !="0") {
                newExpression = oldExpression + number.text;
            }else {
                try {
                    newExpression=oldExpression.Substring(0, oldExpression.Length-3);
                } catch(ArgumentOutOfRangeException) {
                    Debug.WriteLine("Expression boş olamaz.");
                }
            }
            
            var result = calc.EvaluateTheExpression(newExpression);
            number.text=result;
        }
    }
}
