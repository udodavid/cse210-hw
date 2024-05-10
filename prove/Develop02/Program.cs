using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public JournalEntry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    class Journal
    {
        private List<JournalEntry> entries;

        public Journal()
        {
            entries = new List<JournalEntry>();
        }

        public void AddEntry(string prompt, string response, string date)
        {
            JournalEntry newEntry = new JournalEntry(prompt, response, date);
            entries.Add(newEntry);
        }

        public void DisplayEntries()
        {
            int count = 1;
            foreach (JournalEntry entry in entries)
            {
                Console.WriteLine($"Entry {count++}:\n{entry}");
            }
        }

        public void SaveJournal(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    foreach (JournalEntry entry in entries)
                    {
                        writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                    }
                }
                Console.WriteLine("Journal saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the journal: {ex.Message}");
            }
        }

        public void LoadJournal(string filename)
        {
            try
            {
                entries.Clear();
                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        string date = parts[0];
                        string prompt = parts[1];
                        string response = parts[2];
                        entries.Add(new JournalEntry(prompt, response, date));
                    }
                }
                Console.WriteLine("Journal loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the journal: {ex.Message}");
            }
        }

        public void DeleteEntry(int index)
        {
            if (index >= 0 && index < entries.Count)
            {
                entries.RemoveAt(index);
                Console.WriteLine("Entry deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid entry index!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Journal App");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Delete an entry");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        WriteNewEntry(journal);
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        SaveJournal(journal);
                        break;
                    case "4":
                        LoadJournal(journal);
                        break;
                    case "5":
                        DeleteEntry(journal);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void WriteNewEntry(Journal journal)
        {
            string[] prompts = {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Length)];

            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Enter your response: ");
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();

            journal.AddEntry(prompt, response, date);
        }

        static void SaveJournal(Journal journal)
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();
            journal.SaveJournal(filename);
        }

        static void LoadJournal(Journal journal)
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            journal.LoadJournal(filename);
        }

        static void DeleteEntry(Journal journal)
        {
            Console.Write("Enter the index of the entry to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                journal.DeleteEntry(index - 1);
            }
            else
            {
                Console.WriteLine("Invalid index!");
            }
        }
    }
}