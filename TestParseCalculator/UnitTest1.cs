using ParseCalculator;


namespace TestParseCalculator
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("2 + 2", 4)]
        [InlineData("2 - 2", 0)]
        [InlineData("2 + 2 * 2", 6)]
        [InlineData("(2+2)* 2", 8)]


        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }

    }
}