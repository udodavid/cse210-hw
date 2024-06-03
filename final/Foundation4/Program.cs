using System;
using System.Collections.Generic;

class Activity
{
    public string Date { get; set; }
    public int Duration { get; set; } // in minutes

    public virtual double GetDistance()
    {
        return 0.0;
    }

    public virtual double GetSpeed()
    {
        return 0.0;
    }

    public virtual double GetPace()
    {
        return 0.0;
    }

    public virtual string GetSummary()
    {
        return $"{Date} Activity ({Duration} min)";
    }
}

class Running : Activity
{
    public double Distance { get; set; } // in miles

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / Duration) * 60;
    }

    public override double GetPace()
    {
        return Duration / Distance;
    }

    public override string GetSummary()
    {
        return $"{Date} Running ({Duration} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Cycling : Activity
{
    public double Speed { get; set; } // in mph

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetDistance()
    {
        return (Speed * Duration) / 60;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }

    public override string GetSummary()
    {
        return $"{Date} Cycling ({Duration} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Swimming : Activity
{
    public int Laps { get; set; }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0 * 0.62; // distance in miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Duration) * 60;
    }

    public override double GetPace()
    {
        return Duration / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{Date} Swimming ({Duration} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running { Date = "03 Nov 2022", Duration = 30, Distance = 3.0 });
        activities.Add(new Cycling { Date = "04 Nov 2022", Duration = 45, Speed = 15.0 });
        activities.Add(new Swimming { Date = "05 Nov 2022", Duration = 60, Laps = 40 });

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
