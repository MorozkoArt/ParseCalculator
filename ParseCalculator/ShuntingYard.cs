using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    internal class ShuntingYard
    {
        public static string ShuntingYard_generation(string infix)
        {
            Stack<char> operatorStack = [];
            List<string> outputQueue = [];
            string currentNumber = "";
            bool isUnary = true;

            for (int i = 0; i < infix.Length; i++)
            {
                Generation(infix, operatorStack, outputQueue, ref currentNumber, ref isUnary, i);
            }
            if (!string.IsNullOrEmpty(currentNumber)) outputQueue.Add(currentNumber);
            AddingRemainingOperations(operatorStack, outputQueue);
            return string.Join(" ", outputQueue);
        }

        private static void Generation(string infix, Stack<char> operatorStack, List<string> outputQueue, ref string currentNumber, ref bool isUnary, int i)
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
                    currentNumber = AddingNumber(infix, operatorStack, outputQueue, currentNumber, i);
                }
                if (IsOperator(c))
                {
                    isUnary = AddingOperation(operatorStack, outputQueue, isUnary, c);
                }
                else if (c == '(')
                {
                    isUnary = ForOpeningBracket(operatorStack, c);
                }
                else if (c == ')')
                {
                    isUnary = ForClosingBracket(infix, operatorStack, outputQueue, i);
                }
                else
                {
                    throw new ArgumentException($"Недопустимый символ: {c}");
                }
            }
        }

        private static void AddingRemainingOperations(Stack<char> operatorStack, List<string> outputQueue)
        {
            while (operatorStack.Count > 0)
            {
                char op = operatorStack.Pop();
                if (op == '(')
                    throw new ArgumentException("Несоответствие скобок");
                outputQueue.Add(op.ToString());
            }
        }

        private static bool ForClosingBracket(string infix, Stack<char> operatorStack, List<string> outputQueue, int i)
        {
            bool isUnary;
            while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
            {
                outputQueue.Add(operatorStack.Pop().ToString());
            }
            if (operatorStack.Count == 0) throw new ArgumentException("Несоответствие скобок");
            operatorStack.Pop();
            if (i + 1 < infix.Length && char.IsDigit(infix[i + 1])) operatorStack.Push('*');
            isUnary = false;
            return isUnary;
        }

        private static bool ForOpeningBracket(Stack<char> operatorStack, char c)
        {
            operatorStack.Push(c);
            return true;
        }

        private static string AddingNumber(string infix, Stack<char> operatorStack, List<string> outputQueue, string currentNumber, int i)
        {
            outputQueue.Add(currentNumber);
            if (i < infix.Length && infix[i] == '(') operatorStack.Push('*');
            return "";
        }

        private static bool AddingOperation(Stack<char> operatorStack, List<string> outputQueue, bool isUnary, char c)
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
            return isUnary;
        }
        private static bool IsOperator(char c) => "+-*/~".Contains(c);
        private static int Precedence(char op)
        {
            if (op == '~') return 3;
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            return 0;
        }

    }
}
