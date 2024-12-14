using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_UnaryMinus
    {
        [Theory]
        [InlineData("-5 + 3", -2)]
        [InlineData("5 - -3", 8)]
        [InlineData("-2 * 4", -8)]
        [InlineData("-(2 + 3)", -5)]
        [InlineData("10 - (2 * -3)", 16)]
        [InlineData("-(5 - (2 * 2))", -1)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}

