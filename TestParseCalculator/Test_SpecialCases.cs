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
        [InlineData("2(2+2)", 8)]
        [InlineData("2(6 -(5 - 2)/3) / 4", 2.5)]
        [InlineData("(2+2)2", 8)]
        [InlineData("(2+2)2+ 3(2(4(1-2)2))", -40)]
        [InlineData("-2(2+2)", -8)]
        [InlineData("1000 + 20 - 100", 920)]


        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
