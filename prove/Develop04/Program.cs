using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;

    public void StartActivity(string name, string description)
    {
        Console.WriteLine($"Starting {name} Activity");
        Console.WriteLine(description);
        Console.Write("Enter the duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        PauseWithSpinner("Get ready to begin...");
    }

    public void EndActivity(string name)
    {
        PauseWithSpinner("Great job!");
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        PauseWithSpinner("Finishing up...");
    }

    protected void PauseWithSpinner(string message)
    {
        Console.Write(message);
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void RunActivity();
}

class BreathingActivity : MindfulnessActivity
{
    public override void RunActivity()
    {
        StartActivity("Breathing", "This activity will help you relax by guiding you through deep breathing. Clear your mind and focus on your breathing.");

        int halfDuration = duration / 2;
        for (int i = 0; i < halfDuration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(5000);  // 5 seconds
            Console.WriteLine("Breathe out...");
            Thread.Sleep(5000);  // 5 seconds
        }

        EndActivity("Breathing");
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
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

    public override void RunActivity()
    {
        StartActivity("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);

        int elapsed = 0;
        while (elapsed < duration)
        {
            string question = questions[random.Next(questions.Count)];
            Console.WriteLine(question);
            PauseWithSpinner("");
            elapsed += 5;  // Assume each question and reflection takes about 5 seconds
        }

        EndActivity("Reflection");
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public override void RunActivity()
    {
        StartActivity("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);

        PauseWithSpinner("Start thinking...");

        List<string> items = new List<string>();
        Console.WriteLine("Start listing items (press Enter after each item, and type 'done' to finish):");
        DateTime startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
                break;
            items.Add(item);
        }

        Console.WriteLine($"You listed {items.Count} items.");
        EndActivity("Listing");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 4)
                break;

            MindfulnessActivity activity = null;
            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.RunActivity();
        }
    }
}
