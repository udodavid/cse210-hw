using System;
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
