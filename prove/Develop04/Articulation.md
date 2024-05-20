Inheritance is a fundamental concept in object-oriented programming (OOP) that allows a class to inherit properties and methods from another class. The class that inherits is called the derived class (or subclass), and the class it inherits from is called the base class (or superclass). Inheritance promotes code reuse, improves maintainability, and establishes a natural hierarchy between classes.

Explanation of Inheritance
Inheritance enables a new class to receive—or "inherit"—attributes and methods from an existing class. This means the derived class automatically has the functionalities defined in the base class, and it can also have additional properties or methods or override existing ones to provide specific behaviors.

Benefit of Inheritance
A major benefit of inheritance is code reuse. Instead of rewriting common functionality across multiple classes, you can define it once in a base class and have it inherited by derived classes. This makes the code easier to maintain and extend because changes in the base class are automatically reflected in the derived classes.

Application of Inheritance
In the context of the mindfulness program, inheritance is used to define common behaviors for different types of activities (breathing, reflection, and listing) in a base class, and then specific behaviors in derived classes. This structure avoids code duplication and makes the program more organized and scalable.

Example from the mindfulness program:

// Base class containing shared attributes and methods
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

// Derived class with specific behavior for Breathing activity
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


In this example, MindfulnessActivity is the base class that contains common methods like StartActivity, EndActivity, and PauseWithSpinner. The BreathingActivity class inherits from MindfulnessActivity and implements its own version of the RunActivity method. By doing this, we avoid duplicating the start and end logic in every activity class, making our code more efficient and easier to maintain. If we need to change how activities start or end, we can simply update the MindfulnessActivity class, and all derived classes will automatically use the updated logic. This demonstrates how inheritance promotes code reuse and maintainability in a real-world application.






