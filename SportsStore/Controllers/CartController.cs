using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Controllers
{
  public class CartController : Controller
  {
    readonly IProductRepository repository;

    public CartController(IProductRepository repo)
    {
      repository = repo;
    }

    public RedirectToActionResult AddToCart(int productId, string returnUrl)
    {
      Product product = repository.Products
                                  .FirstOrDefault(p => p.ProductID == productId);

      if (product != null)
      {
        Cart cart = GetCart();
        cart.AddItem(product, 1);
        SaveCart(cart);
      }

      return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
    {
      Product product = repository.Products
                                  .FirstOrDefault(p => p.ProductID == productId);

      if (product != null)
      {
        Cart cart = GetCart();
        cart.RemoveLine(product);
        SaveCart(cart);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    Cart GetCart()
    {
      Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
      return cart;
    }

    void SaveCart(Cart cart)
    {
      HttpContext.Session.SetJson("Cart", cart);
    }
  }
}
