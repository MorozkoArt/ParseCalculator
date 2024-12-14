using ParseCalculator;


namespace TestParseCalculator
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("10 - (2 * (3 + 1))", 2)]
        [InlineData("(2+2)*2", 8)]
        [InlineData("2 + 3.5 * 2", 9)]
        [InlineData("10.7 - 2 * ((7/2 - 1) - (7.7 +2.4))", 25.9)]
        [InlineData("2 +      2/1945", 2.00103)]
        [InlineData("-2 + 4", 2)]






        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }

    }
}