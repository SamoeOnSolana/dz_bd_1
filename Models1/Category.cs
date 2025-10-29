namespace App1.Models1;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<DishCategory> DishCategories { get; set; } = new();
}
