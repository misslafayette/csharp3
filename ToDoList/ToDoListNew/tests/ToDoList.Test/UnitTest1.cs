namespace ToDoList.Test;

public class UnitTest1
{
    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(2, 2, 1)]
    public void Divide_WithoutRemainder_Succeeds(int dividend, int divisor, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Divide(dividend, divisor);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void DivideFloat_ByZero_ReturnsInfinity()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var divideAction = () => calculator.Divide(10, 0);

        // Assert
        Assert.Equal(0, calculator.Divide(10, 0));
    }
}

public class Calculator

{
    public float Divide(float dividend, float divisor)
    {
        return dividend / divisor;
    }
}
