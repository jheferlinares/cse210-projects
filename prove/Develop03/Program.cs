using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world, that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        scripture.Display();
        Console.WriteLine("");
        Console.WriteLine("Press 'ENTER' to continue or type 'quit' to exit.");

        while (true)
        {
            string input = Console.ReadLine();

            if (input == "quit")
            {
                break;
            }

            scripture.DeleteRandomWords();
            Console.Clear();
            scripture.Display();

            if (scripture.AllWordsDeleted())
            {
                break;
            }
        }
    }
}

class Scripture
{
    private Reference reference;
    private string[] words;
    private bool[] wordDeletedStatus;

    public Scripture(string referenceText, string scriptureText)
    {
        reference = new Reference(referenceText);
        words = scriptureText.Split(' ');
        wordDeletedStatus = new bool[words.Length];
    }

    public void DeleteRandomWords()
    {
        Random random = new Random();

        for (int i = 0; i < 2; i++)
        {
            int randomIndex = GetRandomWordIndex(random);

            if (!wordDeletedStatus[randomIndex])
            {
                wordDeletedStatus[randomIndex] = true;
            }
            else
            {
                i--;
            }
        }
    }

    private int GetRandomWordIndex(Random random)
    {
        int wordsDeletedCount = wordDeletedStatus.Count(w => w);
        int wordsRemainingCount = words.Length - wordsDeletedCount;

        if (wordsRemainingCount == 0)
        {
            return -1;
        }

        int randomIndex = random.Next(wordsRemainingCount);
        int deletedCount = 0;

        for (int i = 0; i < words.Length; i++)
        {
            if (!wordDeletedStatus[i])
            {
                if (deletedCount == randomIndex)
                {
                    return i;
                }

                deletedCount++;
            }
        }

        return -1;
    }

    public bool AllWordsDeleted()
    {
        return wordDeletedStatus.All(w => w);
    }

    public void Display()
    {
        Console.WriteLine(reference);

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];

            if (wordDeletedStatus[i])
            {
                string punctuation = new string(word.Where(char.IsPunctuation).ToArray());
                string underscore = new string('_', word.Length - punctuation.Length);

                Console.Write(underscore + punctuation + " ");
            }
            else
            {
                Console.Write(word + " ");
            }
        }

        Console.WriteLine();
    }
}

class Reference
{
    private string referenceText;

    public Reference(string referenceText)
    {
        this.referenceText = referenceText;
    }

    public override string ToString()
    {
        return referenceText;
    }
}