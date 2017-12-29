using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
  public class OrderController : Controller
  {
    readonly IOrderRepository repository;
    readonly Cart cart;

    public OrderController(IOrderRepository repoService, Cart cartService)
    {
      repository = repoService;
      cart = cartService;
    }

    public ViewResult Checkout() => View(new Order());

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
      if (!cart.Lines.Any())
      {
        ModelState.AddModelError("", "Sorry, your cart is empty");
      }

      if (ModelState.IsValid)
      {
        order.Lines = cart.Lines.ToArray();
        repository.SaveOrder(order);
        return RedirectToAction(nameof(Completed));
      }
      return View(order);
    }

    public ViewResult Completed()
    {
      cart.Clear();
      return View();
    }
  }
}
