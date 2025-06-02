namespace BasketService.API.Models;

public class CustomerBasket
{
    public string UserId { get; set; } = string.Empty;
    public List<BasketItem> Items { get; set; } = new();
}
