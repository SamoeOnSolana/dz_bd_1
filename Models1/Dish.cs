namespace App1.Models1;

public class Dish
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
    public List<DishCategory> DishCategories { get; set; } = new();
}
