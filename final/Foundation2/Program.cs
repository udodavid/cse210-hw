using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public string ProductID { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal TotalCost()
    {
        return Price * Quantity;
    }
}

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    public string FullAddress()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

class Customer
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Order
{
    public List<Product> Products { get; set; } = new List<Product>();
    public Customer Customer { get; set; }

    public decimal TotalCost()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.TotalCost();
        }

        if (Customer.IsInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }

        return total;
    }

    public string PackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in Products)
        {
            label += $"Product: {product.Name}, ID: {product.ProductID}\n";
        }
        return label;
    }

    public string ShippingLabel()
    {
        return $"Shipping Label:\n{Customer.Name}\n{Customer.Address.FullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address { Street = "123 Main St", City = "New York", State = "NY", Country = "USA" };
        Customer customer1 = new Customer { Name = "John Doe", Address = address1 };

        Product product1 = new Product { Name = "Laptop", ProductID = "A001", Price = 800, Quantity = 1 };
        Product product2 = new Product { Name = "Mouse", ProductID = "A002", Price = 25, Quantity = 2 };

        Order order1 = new Order { Customer = customer1 };
        order1.Products.Add(product1);
        order1.Products.Add(product2);

        Console.WriteLine(order1.PackingLabel());
        Console.WriteLine(order1.ShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.TotalCost()}");

        Address address2 = new Address { Street = "456 Oak St", City = "Toronto", State = "ON", Country = "Canada" };
        Customer customer2 = new Customer { Name = "Jane Smith", Address = address2 };

        Product product3 = new Product { Name = "Phone", ProductID = "B001", Price = 600, Quantity = 1 };
        Product product4 = new Product { Name = "Charger", ProductID = "B002", Price = 20, Quantity = 3 };

        Order order2 = new Order { Customer = customer2 };
        order2.Products.Add(product3);
        order2.Products.Add(product4);

        Console.WriteLine(order2.PackingLabel());
        Console.WriteLine(order2.ShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.TotalCost()}");
    }
}
