using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    public class Calculator
    {
        private string expression;
        public Calculator(string expression)
        {
            this.expression = expression.Replace(" ", "");
        }
        public double Evaluate()
        {
            string rpn = ShuntingYard(expression);
            return EvaluateRPN(rpn);
        }
        private string ShuntingYard(string infix)
        {
            Stack<char> operatorStack = new Stack<char>();
            List<string> outputQueue = new List<string>();
            string currentNumber = "";
            bool isUnary = true;

            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];

                if (char.IsDigit(c) || c == '.')
                {
                    currentNumber += c;
                    isUnary = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        outputQueue.Add(currentNumber);
                        currentNumber = "";
                    }

                    if (IsOperator(c))
                    {
                        if (c == '-' && isUnary)
                        {
                            operatorStack.Push('~');
                        }
                        else
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && Precedence(c) <= Precedence(operatorStack.Peek()))
                            {
                                outputQueue.Add(operatorStack.Pop().ToString());
                            }
                            operatorStack.Push(c);
                            isUnary = true;
                        }
                    }
                    else if (c == '(')
                    {
                        operatorStack.Push(c);
                        isUnary = true;
                    }
                    else if (c == ')')
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                        {
                            outputQueue.Add(operatorStack.Pop().ToString());
                        }

                        if (operatorStack.Count == 0)
                        {
                            throw new ArgumentException("Несоответствие скобок");
                        }
                        operatorStack.Pop();
                        isUnary = false;
                    }
                    else
                    {
                        throw new ArgumentException($"Недопустимый символ: {c}");
                    }
                }
            }
            if (!string.IsNullOrEmpty(currentNumber))
            {
                outputQueue.Add(currentNumber);
            }
            while (operatorStack.Count > 0)
            {
                char op = operatorStack.Pop();
                if (op != '(')
                    outputQueue.Add(op.ToString());
            }
            return string.Join(" ", outputQueue);
        }

        private double EvaluateRPN(string rpn)
        {
            Stack<double> stack = new Stack<double>();
            string[] tokens = rpn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                {
                    stack.Push(Math.Round(num, 4));
                }
                else if (IsOperator(token[0]))
                {
                    if (stack.Count < 2 && token[0] != '~')
                    {
                        throw new ArgumentException($"Недостаточно операндов для операции '{token}'");
                    }
                    if (token[0] == '~')
                    {
                        double operand = stack.Pop();
                        stack.Push(-operand);
                    }
                    else
                    {
                        double operand2 = stack.Pop();
                        double operand1 = stack.Pop();
                        stack.Push(ApplyOperator(token[0], operand1, operand2));
                    }
                }
                else
                {
                    throw new ArgumentException($"Недопустимый токен: '{token}'");
                }
            }

            if (stack.Count != 1)
            {
                throw new ArgumentException("Ошибка вычисления: Несоответствие количества операндов и операторов.");
            }
            return Math.Round(stack.Pop(), 5);
        }
        private bool IsOperator(char c) => "+-*/~".IndexOf(c) != -1;

        private int Precedence(char op)
        {
            if (op == '~') return 3;
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0;
        }
        private double ApplyOperator(char op, double operand1, double operand2)
        {
            switch (op)
            {
                case '+': return operand1 + operand2;
                case '-': return operand1 - operand2;
                case '*': return operand1 * operand2;
                case '/':
                    if (operand2 == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return operand1 / operand2;
                default: throw new ArgumentException("Недопустимый оператор");
            }
        }
    }
}

