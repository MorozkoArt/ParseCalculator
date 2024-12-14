
using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_Exception
    {
        [Theory]
        [InlineData("10 / 0", typeof(DivideByZeroException))]
        [InlineData("2 +", typeof(ArgumentException))]
        [InlineData(")2 + 2(", typeof(ArgumentException))]
        [InlineData("2 + a", typeof(ArgumentException))]
        public void Calculate_InvalidExpressions_ThrowsException(string expression, Type exceptionType)
        {
            var calculator = new Calculator(expression);
            Assert.Throws(exceptionType, () => calculator.Evaluate());
        }
    }
}

