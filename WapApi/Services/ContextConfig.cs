using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace WapApi.Services
{
    public class ContextConfig
    {
        
        private static readonly string seedAdminEmail = "admin@gmail.com";
        public static async Task SeedDataAsync(ShippingContext context,
            UserManager<ApplicationUser> userManger,
            RoleManager<IdentityRole> roleManger)
        {
            await SeedUserAsync(userManger, roleManger);
        
        }
        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManger
            , RoleManager<IdentityRole> roreManger)
        {
            if (!await roreManger.RoleExistsAsync("Admin")) {
                await roreManger.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roreManger.RoleExistsAsync("User")) {
                await roreManger.CreateAsync(new IdentityRole("User"));
            }
            var adminEmail =seedAdminEmail;
            var adminUser =await userManger .FindByEmailAsync(adminEmail);
            if (adminUser == null) { 
                var id = Guid.NewGuid().ToString();
                adminUser = new ApplicationUser
                {
                    Id = id,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                };
                var result =await userManger .CreateAsync(adminUser,"admin123");
                await userManger.AddToRoleAsync(adminUser, "Admin");
            
            }
        }











    }
}
