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
            string rpn = ShuntingYard.ShuntingYard_generation(expression);
            return EvaluateRPN.EvaluateRPN_generation(rpn);
        }
    }
}

