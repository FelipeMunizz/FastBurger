using FastBurger.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Login
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        var user = await _userManager.FindByNameAsync(loginVM.UserName);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }
        }
        ModelState.AddModelError("", "Falha ao realizar o login!!");
        return View(loginVM);
    }

    #region Login do Google (Tutorial de um indiano maluco(Apos testes faz o login mas quebra no return))
    //public async Task LoginGoogle()
    //{
    //    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
    //    {
    //        RedirectUri = Url.Action("GoogleResponse")
    //    });
    //}

    //public async Task<IActionResult> GoogleResponse()
    //{
    //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    //    var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
    //    {
    //        claim.Issuer,
    //        claim.OriginalIssuer,
    //        claim.Type,
    //        claim.Value
    //    });

    //    return Json(claims);
    //}

    //public async Task<IActionResult> LogoutGoogle()
    //{
    //    await HttpContext.SignOutAsync();

    //    return RedirectToAction("Login");
    //}
    #endregion

    #endregion

    #region Registro
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel registroVM)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = registroVM.UserName };
            var result = await _userManager.CreateAsync(user, registroVM.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Login", "Account");
            }
            else
                this.ModelState.AddModelError("Registro", "Falha ao registrar o Usuário");
        }
        return View(registroVM);
    }
    #endregion

    #region Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion

    #region Acesso Negado
    public IActionResult AccessDenied()
    {
        return View();
    }
    #endregion
}