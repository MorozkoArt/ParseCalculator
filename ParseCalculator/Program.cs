using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParseCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ex = Console.ReadLine();
            var calculator = new Calculator(ex);
            Console.WriteLine(calculator.Evaluate());
        }
    }
}