namespace App1.Models1;

public class DishCategory
{
    public int DishId { get; set; }
    public Dish Dish { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
