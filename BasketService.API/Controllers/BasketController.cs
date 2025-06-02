using BasketService.API.Models;
using BasketService.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CustomerBasket>> GetBasket(string userId)
    {
        var basket = await _repository.GetBasket(userId);
        return Ok(basket ?? new CustomerBasket { UserId = userId });
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        return Ok(await _repository.UpdateBasket(basket));
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteBasket(string userId)
    {
        var deleted = await _repository.DeleteBasket(userId);
        return deleted ? NoContent() : NotFound();
    }
}
