// See https://aka.ms/new-console-template for more information
public class FizzBuzz
{
    public void CountTo(int lastNumber)
    {
        for (int actualNumber = 1, actualNumber < lastNumber, actualNumber++)
        {
            if ( lastNumber % 3 == 0 && lastNumber % 5 == 0) 
            {
            Console.WriteLine("FizzBuzz");
            }
            else if ( actualNumber % 3 == 0) 
            {
            Console.WriteLine("Fizz");
            }
            else if (actualNumber % 5 == 0)
            {
            Console.WriteLine("Buzz");
            }
            Console.WriteLine(actualNumber);
       }
    }
}
