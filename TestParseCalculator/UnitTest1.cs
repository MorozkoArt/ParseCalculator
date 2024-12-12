using ParseCalculator;


namespace TestParseCalculator
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("2 + 2", "4")]
        [InlineData("2 - 2", "0")]
        public void Calculate_OneOperationInTwoNumbers(string expression, string result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Calculate());
        }

    }
}