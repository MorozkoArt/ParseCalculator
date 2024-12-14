using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_DecimalFractions
    {
        [Theory]
        [InlineData("2.5 + 3.5", 6)]
        [InlineData("10.7 - 2.3", 8.4)]
        [InlineData("2.5 * 3.5", 8.75)]
        [InlineData("10.5 / 2.0", 5.25)]
        [InlineData("1.2 + 2.3 * 3.4", 9.02)]
        [InlineData("(1.2 + 2.3) * 3.4", 11.9)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
