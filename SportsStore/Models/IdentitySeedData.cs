using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
  public static class IdentitySeedData
  {
    const string adminUser = "Admin";
    const string adminPassword = "Secret123$";

    public static async void EnsurePopulated(IApplicationBuilder app)
    {
      UserManager<IdentityUser> userManager = app.ApplicationServices
                                                 .GetRequiredService<UserManager<IdentityUser>>();

      IdentityUser user = await userManager.FindByIdAsync(adminUser);
      if (user == null)
      {
        user = new IdentityUser("Admin");
        await userManager.CreateAsync(user, adminPassword);
      }
    }
  }
}
