namespace Cled.TrivillinRaffaeleEsame.Models.Entities;

public class Product : Entity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; } 
    public virtual Category Category { get; set; }
}
