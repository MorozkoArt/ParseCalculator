using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    internal class EvaluateRPN
    {
        public static double EvaluateRPN_generation(string rpn)
        {
            Stack<double> stack = [];
            string[] tokens = rpn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                FullValueCalculation(stack, token);
            }
            if (stack.Count != 1)
            {
                throw new ArgumentException("Ошибка вычисления: Несоответствие количества операндов и операторов.");
            }
            return Math.Round(stack.Pop(), 5);
        }

        private static void FullValueCalculation(Stack<double> stack, string token)
        {
            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
            {
                stack.Push(Math.Round(num, 4));
            }
            else if (IsOperator(token[0]))
            {
                СalculatingValue(stack, token);
            }
            else
            {
                throw new ArgumentException($"Недопустимый токен: '{token}'");
            }
        }

        private static void СalculatingValue(Stack<double> stack, string token)
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

        private static  bool IsOperator(char c) => "+-*/~".IndexOf(c) != -1;

        private static double ApplyOperator(char op, double operand1, double operand2)
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
    }
}
