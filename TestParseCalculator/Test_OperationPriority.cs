using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_OperationPriority
    {
        [Theory]
        [InlineData("2 + 3 * 4", 14)]
        [InlineData("10 - 2 / 2", 9)]
        [InlineData("2 * 3 + 4 * 5", 26)]
        [InlineData("10 / 2 + 3 * 2", 11)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
