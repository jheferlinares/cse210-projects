using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");

        string answer = Console.ReadLine();
        int gradeNumber = int.Parse(answer);
        string letter = "";

        if (gradeNumber >= 90)
        {
            letter = "A";
        }
        if (gradeNumber >= 80)
        {
            letter = "B";
        }
        if (gradeNumber >= 70)
        {
            letter = "C";
        }
        if (gradeNumber >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is {letter}.");

        if (gradeNumber > 70)
        {
            Console.WriteLine("YOU PASSED!");
        }
        else
        {
           Console.WriteLine("Better luck next time! :("); 
        }
        
    }
}