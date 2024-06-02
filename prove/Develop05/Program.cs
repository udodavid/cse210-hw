using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public abstract void RecordEvent();
        public abstract bool IsComplete();
        public abstract string GetDetailsString();
        public abstract string GetStringRepresentation();
        public int GetPoints() => _points;
    }

    // SimpleGoal class
    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
            _isComplete = false;
        }

        public override void RecordEvent()
        {
            _isComplete = true;
        }

        public override bool IsComplete()
        {
            return _isComplete;
        }

        public override string GetDetailsString()
        {
            return $"{_name}: {_description} - Points: {_points} - Complete: {_isComplete}";
        }

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal:{_name},{_description},{_points},{_isComplete}";
        }
    }

    // EternalGoal class
    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void RecordEvent()
        {
            // No completion logic, just add points
        }

        public override bool IsComplete()
        {
            return false; // Eternal goals are never complete
        }

        public override string GetDetailsString()
        {
            return $"{_name}: {_description} - Points: {_points} per completion";
        }

        public override string GetStringRepresentation()
        {
            return $"EternalGoal:{_name},{_description},{_points}";
        }
    }

    // ChecklistGoal class
    class ChecklistGoal : Goal
    {
        private int _target;
        private int _bonus;
        private int _amountCompleted;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _target = target;
            _bonus = bonus;
            _amountCompleted = 0;
        }

        public override void RecordEvent()
        {
            _amountCompleted++;
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public int GetBonus() => _bonus;

        public override string GetDetailsString()
        {
            return $"{_name}: {_description} - Points: {_points} - Completed: {_amountCompleted}/{_target} - Bonus: {_bonus}";
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_name},{_description},{_points},{_target},{_bonus},{_amountCompleted}";
        }
    }

    // GoalManager class
    class GoalManager
    {
        private List<Goal> _goals;
        private int _score;
        private int _level;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
            _level = 1;
        }

        public void Start()
        {
            LoadGoals();
            while (true)
            {
                Console.WriteLine("Eternal Quest Program");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. List Goals");
                Console.WriteLine("4. Show Score");
                Console.WriteLine("5. Save and Exit");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateGoal();
                        break;
                    case 2:
                        RecordEvent();
                        break;
                    case 3:
                        ListGoals();
                        break;
                    case 4:
                        ShowScore();
                        break;
                    case 5:
                        SaveGoals();
                        return;
                }
            }
        }

        private void CreateGoal()
        {
            Console.WriteLine("Select Goal Type: ");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal description: ");
            string description = Console.ReadLine();
            Console.Write("Enter goal points: ");
            int points = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    _goals.Add(new SimpleGoal(name, description, points));
                    break;
                case 2:
                    _goals.Add(new EternalGoal(name, description, points));
                    break;
                case 3:
                    Console.Write("Enter target count: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void RecordEvent()
        {
            Console.WriteLine("Select a goal to record an event: ");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < _goals.Count)
            {
                _goals[choice].RecordEvent();
                _score += _goals[choice].GetPoints();
                if (_goals[choice].IsComplete() && _goals[choice] is ChecklistGoal checklistGoal)
                {
                    _score += checklistGoal.GetBonus();
                }
                CheckLevelUp();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        private void ListGoals()
        {
            Console.WriteLine("Your Goals:");
            foreach (var goal in _goals)
            {
                Console.WriteLine(goal.GetDetailsString());
            }
        }

        private void ShowScore()
        {
            Console.WriteLine($"Your current score is: {_score}");
            Console.WriteLine($"Your current level is: {_level}");
        }

        private void CheckLevelUp()
        {
            int newLevel = _score / 1000 + 1; // leveling system
            if (newLevel > _level)
            {
                _level = newLevel;
                Console.WriteLine($"Congratulations! You leveled up to level {_level}!");
            }
        }

        private void SaveGoals()
        {
            using (StreamWriter outputFile = new StreamWriter("goals.txt"))
            {
                outputFile.WriteLine(_score);
                outputFile.WriteLine(_level); // Save the level
                foreach (var goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }
        }

        private void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                string[] lines = File.ReadAllLines("goals.txt");
                _score = int.Parse(lines[0]);
                _level = int.Parse(lines[1]); // Load the level
                for (int i = 2; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(':');
                    string goalType = parts[0];
                    string[] goalDetails = parts[1].Split(',');

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            _goals.Add(new SimpleGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2])));
                            break;
                        case "EternalGoal":
                            _goals.Add(new EternalGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2])));
                            break;
                        case "ChecklistGoal":
                            _goals.Add(new ChecklistGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]), int.Parse(goalDetails[3]), int.Parse(goalDetails[4])));
                            break;
                    }
                }
            }
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager goalManager = new GoalManager();
            goalManager.Start();
        }
    }
}
