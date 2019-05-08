using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        private readonly Dictionary<string, (string symbol, int predence, bool rightAssociative)> operators =
            new (string symbol, int predence, bool rightAssociative)[] {
                ("^", 4, true),
                ("*", 3, false),
                ("/", 3, false),
                ("+", 2, false),
                ("-", 2, false)
            }.ToDictionary(op => op.symbol);

        public DynamicText number { get; set; }
        public DynamicText expressions { get; set; }


        public Calculator(DynamicText number,DynamicText expression) {
            this.number=number;
            this.expressions=expression;
        }
        public void EvaluateTheExpression() {

        }
        // shunting-yard algoritması tablomuz aracılığıyla infix ifadeyi
        //  postfix hale çeviriyor.
        private List<string> InfixToPostfix(string infix) {
            string[] tokens = infix.Split(' ');
            Stack<string> stack = new Stack<string>();
            List<string> postfix = new List<string>();
            foreach(string token in tokens) {
                if(Decimal.TryParse(token, out _)) {
                    postfix.Add(token);
                } else if(operators.TryGetValue(token, out var op1)) {
                    while(stack.Count > 0 && operators.TryGetValue(stack.Peek(), out var op2)){
                        int c = op1.predence.CompareTo(op2.predence);
                        if(c < 0 || !op1.rightAssociative && c<=0) {
                            postfix.Add(stack.Pop());
                        } else {
                            break;
                        }
                    }
                    stack.Push(token);
                } else if(token=="(") {
                    stack.Push("(");
                } else if(token ==")") {
                    while(stack.Count > 0 && stack.Peek() != "(") {
                        postfix.Add(stack.Pop());
                    }
                    postfix.Add(stack.Pop());
                }
            }
            while(stack.Count >0) {
                postfix.Add(stack.Pop());
            }
            foreach(string s in postfix) {
                Debug.Write(s+" ");
            }
            Debug.WriteLine("");
            return postfix;   
        }
        public string PostfixEvaluation(List<string> postfix) {
            decimal temp,topStack, retVal=0;
            var stack = new Stack<string>();
            foreach(string s in postfix) {
                if(Decimal.TryParse(s,out _)) {
                    stack.Push(s);
                } else if(operators.TryGetValue(s,out var op)) {

                    Decimal.TryParse(stack.Pop(), out topStack);
                    Decimal.TryParse(stack.Pop(), out temp);
                    switch(s) {
                        case "+":
                            retVal=Decimal.Add(temp, topStack);
                            break;
                        case "-":
                            retVal=Decimal.Subtract(temp, topStack);
                            break;
                        case "/":
                            retVal=Decimal.Divide(temp, topStack);
                            break;
                        case "*":
                            retVal=Decimal.Multiply(temp, topStack);
                            break;
                        case "^":
                            //retVal=Decimal.Pow(temp, Convert.ToInt32(topStack));
                            break;
                        default:
                            retVal=-987656789;
                            break;
                    }
                    stack.Push(retVal.ToString());
                }
            }
            return stack.Pop();
        }

    }
}
