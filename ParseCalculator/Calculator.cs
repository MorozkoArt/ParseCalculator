using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    public class Calculator
    {
        
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
        }
    }
}

