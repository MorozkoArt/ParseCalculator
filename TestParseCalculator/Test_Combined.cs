using ParseCalculator;


namespace TestParseCalculator
{
    public class Test_Combined
    {
        [Theory]
        [InlineData("10 - (2 * (3 + 1))", 2)]
        [InlineData("2 + 3 * (4 - 1) / 2", 6.5)]
        [InlineData("(10 / 2 - 1) * (3 + 2)", 20)]
        [InlineData("10.7 - 2 * ((7/2 - 1) - (7.7 +2.4))", 25.9)]
        [InlineData("2.5 + 3.5 * (2 - 1) / 2", 4.25)]
        [InlineData("-2 * (3.5 + 1.5) / 2", -5)]
        [InlineData("-(5.5 + (2.5 * 2)) * 2", -21)]


        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
