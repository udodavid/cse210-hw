using System;
using System.Collections.Generic;

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string FullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Event
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public Address Address { get; set; }

    public virtual string StandardDetails()
    {
        return $"Title: {Title}\nDescription: {Description}\nDate: {Date}\nTime: {Time}\nAddress: {Address.FullAddress()}";
    }

    public virtual string FullDetails()
    {
        return StandardDetails();
    }

    public virtual string ShortDescription()
    {
        return $"Type: {GetType().Name}, Title: {Title}, Date: {Date}";
    }
}

class Lecture : Event
{
    public string Speaker { get; set; }
    public int Capacity { get; set; }

    public override string FullDetails()
    {
        return $"{StandardDetails()}\nType: Lecture\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    }
}

class Reception : Event
{
    public string RSVP_Email { get; set; }

    public override string FullDetails()
    {
        return $"{StandardDetails()}\nType: Reception\nRSVP Email: {RSVP_Email}";
    }
}

class OutdoorGathering : Event
{
    public string WeatherForecast { get; set; }

    public override string FullDetails()
    {
        return $"{StandardDetails()}\nType: Outdoor Gathering\nWeather: {WeatherForecast}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address { Street = "123 Elm St", City = "Springfield", State = "IL", Country = "USA" };
        Event lecture = new Lecture { Title = "Advanced C#", Description = "An in-depth look at C# features.", Date = "02/06/2024", Time = "10:00 AM", Address = address1, Speaker = "Dr. Smith", Capacity = 100 };

        Address address2 = new Address { Street = "456 Maple St", City = "Seattle", State = "WA", Country = "USA" };
        Event reception = new Reception { Title = "Company Annual Meet", Description = "Annual company-wide meeting.", Date = "01/06/2024", Time = "6:00 PM", Address = address2, RSVP_Email = "rsvp@company.com" };

        Address address3 = new Address { Street = "789 Pine St", City = "Denver", State = "CO", Country = "USA" };
        Event outdoorGathering = new OutdoorGathering { Title = "Summer Picnic", Description = "Outdoor picnic for all employees.", Date = "06/20/2023", Time = "11:00 AM", Address = address3, WeatherForecast = "Sunny" };

        List<Event> events = new List<Event> { lecture, reception, outdoorGathering };

        foreach (var ev in events)
        {
            Console.WriteLine(ev.StandardDetails());
            Console.WriteLine();
            Console.WriteLine(ev.FullDetails());
            Console.WriteLine();
            Console.WriteLine(ev.ShortDescription());
            Console.WriteLine("\n-------------------------\n");
        }
    }
}
