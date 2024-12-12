using System;
using System.Collections.Generic;
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
            this.expression = expression.Replace(" ", ""); // Удаляем пробелы
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

            foreach (char c in infix)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    currentNumber += c;
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
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && Precedence(c) <= Precedence(operatorStack.Peek()))
                        {
                            outputQueue.Add(operatorStack.Pop().ToString());
                        }
                        operatorStack.Push(c);
                    }
                    else if (c == '(')
                    {
                        operatorStack.Push(c);
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
                        operatorStack.Pop(); // Удаляем '('
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
                outputQueue.Add(operatorStack.Pop().ToString());
            }

            return string.Join(" ", outputQueue);
        }


        private double EvaluateRPN(string rpn)
        {
            Stack<double> stack = new Stack<double>();
            string[] tokens = rpn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Удаляем пустые токены

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    stack.Push(num);
                }
                else if (IsOperator(token[0]))
                {
                    // Проверка на достаточное количество операндов ПЕРЕД выполнением операции
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException($"Недостаточно операндов для операции '{token}'");
                    }
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();
                    stack.Push(ApplyOperator(token[0], operand1, operand2));
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

            return stack.Pop();
        }

        private bool IsOperator(char c) => "+-*/()".IndexOf(c) != -1;

        private int Precedence(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0; // Для '(' и ')'
        }

        private double ApplyOperator(char op, double operand1, double operand2)
        {
            switch (op)
            {
                case '+': return operand1 + operand2;
                case '-': return operand1 - operand2;
                case '*': return operand1 * operand2;
                case '/': return operand2 == 0 ? throw new DivideByZeroException() : operand1 / operand2;
                default: throw new ArgumentException("Недопустимый оператор");
            }
        }


        

        /*
        private string[] _expression;

        public Calculator(string expression)
        {
            _expression = expression.Split(' ');
        }
        
        public string Calculate()
        {
            var result = 0;
            var num1 = int.Parse(_expression[0]);
            var num2 = int.Parse(_expression[2]);
            var operation = _expression[1];
            return GetResult(result, num1, num2, operation).ToString();
        }

        private static int GetResult(int result, int num1, int num2, string operation)
        {
            switch (operation)
            {
                case ("+"):
                    result = num1 + num2;
                    break;
                case ("-"):
                    result = num1 - num2;
                    break;
                case ("*"):
                    result = num1 * num2;
                    break;
                case ("/"):
                    result = num1 / num2;
                    break;
                default:
                    throw new FormatException();
            }

            return result;
        }*/
    }
    
}

