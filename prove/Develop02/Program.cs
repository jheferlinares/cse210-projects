using System;
using System.Collections.Generic;
using System.IO;

// Class to represent a journal entry
public class JournalEntry
{
    public string DoRightToday { get; set; }
    public string BestPartOfDay { get; set; }
    public string HelpToday { get; set; }
    public string StrongestEmotion { get; set; }
    public string SmileToday { get; set; }
    public DateTime EntryTime { get; set; }
}

// Class to manage journal entries
public class JournalManager
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    // Method to add a new entry
    public void AddEntry(JournalEntry entry)
    {
        entry.EntryTime = DateTime.Now;
        entries.Add(entry);
    }

    // Method to get all entries
    public List<JournalEntry> GetAllEntries()
    {
        return entries;
    }
}

// Main program class
public class Program
{
    private static JournalManager journalManager = new JournalManager();

    static void Main()
    {
        int choice;
        do
        {
            choice = DisplayMenuAndGetChoice();
            switch (choice)
            {
                case 1:
                    WriteJournalEntry();
                    break;
                case 2:
                    DisplayJournalEntries();
                    break;
                case 3:
                    LoadJournalEntries();
                    break;
                case 4:
                    SaveJournalEntries();
                    break;
                case 5:
                    Console.WriteLine("Bye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 5);
    }

    static int DisplayMenuAndGetChoice()
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");
        return int.Parse(Console.ReadLine());
    }

    static void WriteJournalEntry()
    {
        JournalEntry entry = new JournalEntry();

        Console.WriteLine("what did i do right today?");
        entry.DoRightToday = Console.ReadLine();

        Console.WriteLine("What was the best part of your day?");
        entry.BestPartOfDay = Console.ReadLine();

        Console.WriteLine("Who did I help today?");
        entry.HelpToday = Console.ReadLine();

        Console.WriteLine("What was the strongest emotion you felt today?");
        entry.StrongestEmotion = Console.ReadLine();

        Console.WriteLine("Why did I smile today?");
        entry.SmileToday = Console.ReadLine();

        journalManager.AddEntry(entry);
        Console.WriteLine("Entry added successfully.");
    }

    static void DisplayJournalEntries()
    {
        var entries = journalManager.GetAllEntries();
        foreach (var entry in entries)
        {
            Console.WriteLine($"Time: {entry.EntryTime}");
            Console.WriteLine($"Interacted Person: {entry.DoRightToday}");
            Console.WriteLine($"Best Part of the Day: {entry.BestPartOfDay}");
            Console.WriteLine($"Who i helped today: {entry.HelpToday}");
            Console.WriteLine($"Strongest Emotion: {entry.StrongestEmotion}");
            Console.WriteLine($"Why i smiled today: {entry.SmileToday}");
            Console.WriteLine();
        }
    }

    static void LoadJournalEntries()
    {
        Console.Write("Enter the file name to load: ");
        string fileName = Console.ReadLine();
        try
        {
            var entries = File.ReadAllLines(fileName);
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SaveJournalEntries()
    {
        Console.Write("Enter the file name to save: ");
        string fileName = Console.ReadLine();
        try
        {
            var entries = journalManager.GetAllEntries();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"Time: {entry.EntryTime}");
                    writer.WriteLine($"What i did right today: {entry.DoRightToday}");
                    writer.WriteLine($"Best Part of the Day: {entry.BestPartOfDay}");
                    writer.WriteLine($"I helped today: {entry.HelpToday}");
                    writer.WriteLine($"Strongest Emotion: {entry.StrongestEmotion}");
                    writer.WriteLine($"Why i smiled today: {entry.SmileToday}");
                    writer.WriteLine();
                }
            }
            Console.WriteLine("Entries saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}