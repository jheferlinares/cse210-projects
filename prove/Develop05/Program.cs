using System;
using System.Collections.Generic;
using System.IO;
public abstract class Goal
{
    public string Name { get; private set; }
    public bool IsComplete { get; protected set; }
    public int Points { get; private set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();

    public virtual string GetCompletionStatus()
    {
        return IsComplete ? "[X]" : "[ ]";
    }

    public abstract string GetDetailsString();
}
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points)
    {
    }

    public override void RecordEvent()
    {
        IsComplete = true;
    }

    public override string GetDetailsString()
    {
        return $"{GetCompletionStatus()} {Name}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override void RecordEvent()
    {
    }

    public override string GetDetailsString()
    {
        return $"{GetCompletionStatus()} {Name} (Eternal)";
    }
}

public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CompletedCount { get; private set; }

    public ChecklistGoal(string name, int points, int targetCount) : base(name, points)
    {
        TargetCount = targetCount;
    }

    public override void RecordEvent()
    {
        CompletedCount++;
        if (CompletedCount >= TargetCount)
        {
            IsComplete = true;
        }
    }

    public override string GetDetailsString()
    {
        return $"{GetCompletionStatus()} {Name} (Completed {CompletedCount}/{TargetCount})";
    }
}

public class GoalManager
{
    private List<Goal> goals = new List<Goal>();

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            Goal goal = goals[goalIndex];
            if (!goal.IsComplete)
            {
                goal.RecordEvent();
                Console.WriteLine($"Event recorded for goal: {goal.Name}");
            }
            else
            {
                Console.WriteLine($"Goal already completed: {goal.Name}");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Goals: ");
        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];
            Console.WriteLine($"{i + 1}. {goal.GetDetailsString()}");
        }
    }

    public void SaveGoals(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.IsComplete}");
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string fileName)
    {
        goals.Clear();
        if (File.Exists(fileName))
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        string goalType = parts[0];
                        string name = parts[1];
                        bool isComplete = bool.Parse(parts[2]);

                        Goal goal;
                        switch (goalType)
                        {
                            case nameof(SimpleGoal):
                                goal = new SimpleGoal(name, 0);
                                break;
                            case nameof(EternalGoal):
                                goal = new EternalGoal(name, 0);
                                break;
                            case nameof(ChecklistGoal):
                                goal = new ChecklistGoal(name, 0, 0);
                                break;
                            default:
                                continue;
                        }

                        goal.IsComplete = isComplete;
                        goals.Add(goal);
                    }
                }
            }
        }
        Console.WriteLine("Goals loaded successfully.");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool exitProgram = false;

        while (!exitProgram)
        {
            Console.WriteLine("Menu options");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Save goals");
            Console.WriteLine("4. Load goals");
            Console.WriteLine("5. Record event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal(goalManager);
                    break;
                case "2":
                    goalManager.DisplayGoals();
                    break;
                case "3":
                    Console.Write("Enter file name to save goals: ");
                    string saveFileName = Console.ReadLine();
                    goalManager.SaveGoals(saveFileName);
                    break;
                case "4":
                    Console.Write("Enter file name to load goals: ");
                    string loadFileName = Console.ReadLine();
                    goalManager.LoadGoals(loadFileName);
                    break;
                case "5":
                    AddEvent(goalManager);
                    break;
                case "6":
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateNewGoal(GoalManager goalManager)
    {
        Console.WriteLine("The types of Goals are: ");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("What type of goal would you like to create? ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateSimpleGoal(goalManager);
                break;
            case "2":
                CreateEternalGoal(goalManager);
                break;
            case "3":
                CreateChecklistGoal(goalManager);
                break;
            default:
                Console.WriteLine("Invalid choice. Goal not created.");
                break;
        }
    }

    private static void CreateSimpleGoal(GoalManager goalManager)
    {
        Console.Write("What is the name of the goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = new SimpleGoal(name, points);
        goalManager.AddGoal(goal);
        Console.WriteLine("Simple goal created successfully.");
    }

    private static void CreateEternalGoal(GoalManager goalManager)
    {
        Console.Write("What is the name of the goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = new EternalGoal(name, points);
        goalManager.AddGoal(goal);
        Console.WriteLine("Eternal goal created successfully.");
    }

    private static void CreateChecklistGoal(GoalManager goalManager)
    {
        Console.Write("What is the name of the goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("How many times must you complete this goal? ");
        int targetCount = int.Parse(Console.ReadLine());

        Goal goal = new ChecklistGoal(name, points, targetCount);
        goalManager.AddGoal(goal);
        Console.WriteLine("Checklist goal created successfully.");
    }

    private static void AddEvent(GoalManager goalManager)
    {
        goalManager.DisplayGoals();
        Console.Write("Enter the goal index: ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;
        goalManager.RecordEvent(goalIndex);
    }
}
