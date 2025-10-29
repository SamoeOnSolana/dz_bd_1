namespace App1.Models1;

public class ProductOrder
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Price { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new();
}
