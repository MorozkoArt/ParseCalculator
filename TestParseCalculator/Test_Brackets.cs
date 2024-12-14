using ParseCalculator;

namespace TestParseCalculator
{
    public class Test_Brackets
    {
        [Theory]
        [InlineData("(2 + 2) * 2", 8)]
        [InlineData("2 * (3 + 4)", 14)]
        [InlineData("(10 - 2) / 2", 4)]
        [InlineData("10 / (2 + 3)", 2)]
        [InlineData("(2 + 3) * (4 - 1)", 15)]
        [InlineData("(2 + 3 * (5 - 2))", 11)]
        [InlineData("((2 + 10) / 2) + 5", 11)]
        [InlineData("10 / (2 * (1 + 1))", 2.5)]
        [InlineData("2 + (((2+2)))", 6)]

        public void Calculate_OneOperationInTwoNumbers(string expression, double result)
        {
            var calculator = new Calculator(expression);
            Assert.Equal(result, calculator.Evaluate());
        }
    }
}
