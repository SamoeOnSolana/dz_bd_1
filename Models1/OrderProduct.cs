namespace App1.Models1;

public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public ProductOrder Product { get; set; } = null!;
    public int Quantity { get; set; }
}
