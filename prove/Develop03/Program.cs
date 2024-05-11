using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Scripture object
            Scripture scripture = new Scripture("John 3:16", "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have eternal life.");

            // Display the complete scripture
            scripture.Display();

            // Prompt the user to press enter to hide words
            while (true)
            {
                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    break;

                // Hide random words
                scripture.HideRandomWords();
            }
        }
    }

    class Scripture
    {
        private string reference;
        private List<Word> words;

        public Scripture(string reference, string text)
        {
            this.reference = reference;
            words = new List<Word>();

            // Split the text into words
            string[] wordArray = text.Split(' ');

            // Add words to the list
            foreach (string word in wordArray)
            {
                words.Add(new Word(word));
            }
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(reference);
            foreach (Word word in words)
            {
                Console.Write(word.IsHidden ? "____ " : word.Text + " ");
            }
        }

        public void HideRandomWords()
        {
            // Select a random word
            Random random = new Random();
            int index = random.Next(words.Count);

            // Hide the selected word
            words[index].Hide();

            // Display the scripture again
            Display();

            // Check if all words are hidden
            if (words.All(word => word.IsHidden))
            {
                Console.WriteLine("\nAll words are hidden. Program ended.");
                Environment.Exit(0);
            }
        }
    }

    class Word
    {
        public string Text { get; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }
    }
}