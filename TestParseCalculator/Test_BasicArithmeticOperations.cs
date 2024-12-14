using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_BasicArithmeticOperations
    {
        [Theory]
        [InlineData("2 + 2", 4)]
        [InlineData("5 - 3", 2)]
        [InlineData("4 * 6", 24)]
        [InlineData("10 / 2", 5)]
        [InlineData("10 / 3", 3.33333)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
