using System;
using System.Threading;

public abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
        this.duration = 0;
    }

    public void Start()
    {
        Console.WriteLine($"Welcome to the {name} activity.");
        Console.WriteLine(description);
        SetDuration();
        PrepareToBegin();
        DoActivity();
        Finish();
    }

    protected virtual void SetDuration()
    {
        Console.Write("How long, in seconds, would you like for your session? ");
        duration = Convert.ToInt32(Console.ReadLine());
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("Get Ready...");
        Thread.Sleep(1500);
    }

    protected abstract void DoActivity();

    protected void Finish()
    {
        Console.WriteLine("Well done!");
        Thread.Sleep(1000);
        Console.WriteLine($"You have completed another {duration} seconds of the {name} activity.");
        Thread.Sleep(1000);
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    protected override void DoActivity()
    {
        Console.WriteLine("Clear your mind and focus on your breathing.");
        Thread.Sleep(3000);

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner();
            Console.WriteLine("Breathe out...");
            ShowSpinner();
        }
    }

    private void ShowSpinner()
    {
        for (int j = -1; j < 3; j++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine();
    }
}

public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(string name, string description) : base(name, description)
    {
    }

    protected override void DoActivity()
    {
        Console.WriteLine("Think deeply about the following prompt:");
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);
        Thread.Sleep(4000);

        foreach (string question in questions)
        {
            Console.WriteLine(question);
            ShowSpinner();
        }
    }

    private void ShowSpinner()
    {
        for (int j = 0; j < 3; j++)
        {
            Thread.Sleep(1000);
            Console.Write(".");
        }
        Console.WriteLine();
    }
}


public class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(string name, string description) : base(name, description)
    {
    }

    protected override void DoActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        do
        {
            if (DateTime.Now >= endTime)
            {
                break;
            }

            string entrada = Console.ReadLine();

        } while (true);

        Console.WriteLine("");
    }

    private void ShowSpinner()
    {
        for (int j = 0; j < 3; j++)
        {
            Thread.Sleep(1000);
            Console.Write(".");
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Menu Options: ");
            Console.WriteLine("1.Start Breathing Activity");
            Console.WriteLine("2.Start Reflection Activity");
            Console.WriteLine("3.Start Listing Activity");
            Console.WriteLine("4.Quit");

            Console.Write("Select a choice from the menu: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    BreathingActivity breathingActivity = new BreathingActivity("Breathing", "This activity will help you relax by walking you through breathing in and out slowly.");
                    breathingActivity.Start();
                    break;
                case 2:
                    ReflectionActivity reflectionActivity = new ReflectionActivity("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.");
                    reflectionActivity.Start();
                    break;
                case 3:
                    ListingActivity listingActivity = new ListingActivity("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                    listingActivity.Start();
                    break;
                case 4:
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
