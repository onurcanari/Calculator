using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculator {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        Calculator calc;
        DynamicText number = new Number();
        DynamicText expressions = new Number();
        public Form1() {
            InitializeComponent();
            lblNumber.DataBindings.Add("Text", number, "number");
            lblExpression.DataBindings.Add("Text", expressions, "number");
        }

        private void Form1_Load(object sender, EventArgs e) {
            calc=new Calculator(number, expressions);
        }

        private void One_Click(object sender, EventArgs e) {
            number.Append("1");
        }
        private void Two_Click(object sender, EventArgs e) {
            number.Append("2");
        }
        private void Three_Click(object sender, EventArgs e) {
            number.Append("3");
        }
        private void Four_Click(object sender, EventArgs e) {
            number.Append("4");
        }
        private void Five_Click(object sender, EventArgs e) {
            number.Append("5");
        }
        private void Six_Click(object sender, EventArgs e) {
            number.Append("6");
        }
        private void Seven_Click(object sender, EventArgs e) {
            number.Append("7");
        }
        private void Eight_Click(object sender, EventArgs e) {
            number.Append("8");
        }
        private void Nine_Click(object sender, EventArgs e) {
            number.Append("9");
        }
        private void Add_Click(object sender, EventArgs e) {
            expressions.Append(number.ToString() + " +");
            lblExpression.Text=expressions.ToString();
        }
        private void Subtract_Click(object sender, EventArgs e) {
            number.Append("-");
        }
        private void Multiply_Click(object sender, EventArgs e) {
            number.Append("*");
        }
        private void Divide_Click(object sender, EventArgs e) {
            number.Append("/");
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

        private void Zero_Click(object sender, EventArgs e) {
            number.Append("0");
        }

        private void FindFactorial(object sender, EventArgs e) {

        }

        private void MetroButton35_Click(object sender, EventArgs e) {

        }
    }
}
