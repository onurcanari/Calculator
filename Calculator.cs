using Extreme.Mathematics;
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
        
        public readonly string[] DIGITS = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public readonly string[] OPERATORS = { "+", "-", "×", "÷", "^" };
        public readonly string[] PARANTHESIS = { "(", ")" };  
        private readonly Dictionary<string, (string symbol, int predence, bool rightAssociative)> operators =
            new (string symbol, int predence, bool rightAssociative)[] {
                ("^", 4, true),
                ("×", 3, false),
                ("÷", 3, false),
                ("+", 2, false),
                ("-", 2, false)
            }.ToDictionary(op => op.symbol);
        public static readonly decimal PI = 3.1415926535897932384626M;

        public DynamicText number { get; set; }
        public DynamicText expressions { get; set; }
        public int paranthesisInExpressionCount{ get; set; }
        
        public Calculator(DynamicText number,DynamicText expression) {
            this.number=number;
            this.expressions=expression;
        }

        public static decimal Factorial(decimal n) {
            if(n<=0)
                return 1;
            return n*Factorial(n-1);
        }
        

        public string EvaluateTheExpression(string infixExpression) {
            var postfix = InfixToPostfix(infixExpression);
            var result = EvaluatePostix(postfix);
            return result;
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

        // postfix ifadeyi hesaplıyor. (Reverse-Polish Notation)
        private string EvaluatePostix(List<string> postfix) {
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
                        case "÷":
                            retVal=Decimal.Divide(temp, topStack);
                            break;
                        case "×":
                            retVal=Decimal.Multiply(temp, topStack);
                            break;
                        case "^":
                            retVal=Calculator.Pow(temp, topStack);
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

        public static decimal Pow(decimal x, decimal y) {
            /*
            decimal result=1;
            for(int i = 0; i<y; i++)
                result*=x;
            return result;
            */
            return DecimalMath.Pow(x,y);
        } 

        public static decimal Log10(decimal x) {
            return DecimalMath.Log10(x);
        }
        public static decimal Log(decimal x) {
            return DecimalMath.Log(x);
        }
        public static decimal Sin(decimal x) {
            return DecimalMath.Sin(x);
        }
        public static decimal Cos(decimal x) {
            return DecimalMath.Cos(x);
        }
        public static decimal Tan(decimal x) {
            return DecimalMath.Tan(x);
        }
        public static decimal Sinh(decimal x) {
            return DecimalMath.Sinh(x);
        }
        public static decimal Cosh(decimal x) {
            return DecimalMath.Cosh(x);
        }
        public static decimal Tanh(decimal x) {
            return DecimalMath.Tanh(x);
        }

        
        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M) {
            if(x<0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do {
                previous=current;
                if(previous==0.0M) return 0;
                current=(previous+x/previous)/2;
            }
            while(Math.Abs(previous-current)>epsilon);
            return current;
        }
    }
}
