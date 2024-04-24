using System;

class Program
{
    static void Main(string[] args)
    {
        //Ask User for their Name        
        Console.Write("What is your first name? ");
        string first_name = Console.ReadLine();
        
        Console.Write("What is your last name? ");
        string last_name = Console.ReadLine();
        
        Console.Write($"Your name is {last_name}, {first_name} {last_name}.");
    }
}