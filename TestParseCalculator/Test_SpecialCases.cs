using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_SpecialCases
    {
        [Theory]
        [InlineData("     0 +       0                 ", 0)]
        [InlineData("0 - 0", 0)]
        [InlineData("0 * 0", 0)]
        [InlineData("0 / 1", 0)]
        [InlineData("1 / 1", 1)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
