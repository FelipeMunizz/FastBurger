using FastBurger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FastBurger.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUser()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                IdentityResult result = _userManager.CreateAsync(user, "Numsey@2022").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Member").Wait();
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser admin = new IdentityUser();
                admin.UserName = "admin@localhost";
                admin.Email = "admin@localhost";
                admin.NormalizedUserName = "ADMIN@LOCALHOST";
                admin.NormalizedEmail = "ADMIN@LOCALHOST";
                admin.EmailConfirmed = true;
                admin.LockoutEnabled = false;
                admin.SecurityStamp = Guid.NewGuid().ToString();
                IdentityResult result = _userManager.CreateAsync(admin, "Numsey@2022").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(admin, "Admin").Wait();
            }
        }
    }
}
