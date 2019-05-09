using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Calculator {
    public partial class Form1 : Form {

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
            bool changeOperator = false;
            for(int i= 0; i<calc.OPERATORS.Length; i++) {
                if(expression.text.EndsWith(calc.OPERATORS[i]+" ")) {
                    changeOperator=true;
                }
            }
            for(int i=0;i<operatorButtons.Length;i++) {
                if((Button)sender==operatorButtons[i]) {
                    if(expression.text.EndsWith(") ")) {
                        expression.Append(calc.OPERATORS[i]+" ");
                    }
                    else if(changeOperator && number.text == "0") {
                        expression.text=String.Format("{0} {1} ", expression.text.Substring(0, expression.text.Length-3), calc.OPERATORS[i]);
                    }else if(i == 4) {
                        expression.Append(String.Format("{0} {1} 2 ", number.ToString(), calc.OPERATORS[i]));

                    } else {
                        expression.Append(String.Format("{0} {1} ", number.ToString(), calc.OPERATORS[i]));
                    }
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

        private void Backspace_Click(object sender, EventArgs e) {
            number.Delete();
        }

        private void Factorial_Click(object sender, EventArgs e) {
            decimal n;
            if(!decimal.TryParse(number.ToString(), out n)) {
                return;
            }
            n=Calculator.Factorial(n);
            number.text=n.ToString();
        }

        private void Evaluate_Click(object sender, EventArgs e) {
            
            string oldExpression = expression.text;
            string newExpression = "";

            if(number.text !="0") {
                newExpression = oldExpression + number.text;
                expression.text=newExpression;
            }else {
                try {
                    newExpression=oldExpression.Substring(0, oldExpression.Length-3);
                } catch(ArgumentOutOfRangeException) {
                    Debug.WriteLine("Expression boş olamaz.");
                }
            }
            
            var result = calc.EvaluateTheExpression(newExpression);
            number.text=result;
            expression.Clear();
        }

        private void btnClearNumber_Click(object sender, EventArgs e) {
            number.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            expression.Clear();
            number.Clear();
        }

        private void PiNumber_Click(object sender, EventArgs e) {
            number.text=Math.PI.ToString();
        }

        private void Dot_Click(object sender, EventArgs e) {
            if(number.text.Contains('.'))
                return;
            number.Append(".");
        }
        
        private void Sign_Click(object sender, EventArgs e) {
            decimal.TryParse(number.text, out decimal realNumber);
            realNumber*=-1;
            number.text=realNumber.ToString();
        }

        private void BtnLeftParenthesis_Click(object sender, EventArgs e) {
            expression.Append("( ");
            calc.paranthesisInExpressionCount++;
        }

        private void BtnRightParenthesis_Click(object sender, EventArgs e) {
            if(calc.paranthesisInExpressionCount != 0 && number.text != "0") {
                expression.Append(string.Format("{0} {1}", number.text, ") "));
                number.Clear();
            }
            
        }
    }
}
