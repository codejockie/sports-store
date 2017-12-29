using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
  public class AdminController : Controller
  {
    readonly IProductRepository repository;

    public AdminController(IProductRepository repo)
    {
      repository = repo;
    }

    public ViewResult Index() => View(repository.Products);
  }
}
