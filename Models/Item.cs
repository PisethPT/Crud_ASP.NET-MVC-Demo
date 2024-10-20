using System;

namespace MyApp.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
}
