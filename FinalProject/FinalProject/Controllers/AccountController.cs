using FinalProject.Data;
using FinalProject.Helpers.Enums;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels.Account;
using FinalProject.ViewModels.Cart;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ICartService cartService
                                 )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _cartService = cartService;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(model);
                //}

                AppUser newUser = new()
                {
                    UserName = string.Join("_", model.FirstName, model.LastName),
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    TempData["errors"] = model.ErrorMessages;
                    return View(model);
                }

                await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

                string subject = "Register confirmation";

                string html = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
                {
                    html = reader.ReadToEnd();
                }

                html = html.Replace("{{link}}", link);
                html = html.Replace("{{headerText}}", "Welcome");

                _emailService.Send(newUser.Email, subject, html);

                return RedirectToAction(nameof(VerifyEmail));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);
            List<CartVM> cartVMs = new();
            Cart dbCart = await _cartService.GetByUserIdAsync(userId);
            if (dbCart is not null)
            {
                List<FoodCart> foodsCart = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                foreach (var foodCart in foodsCart)
                {
                    cartVMs.Add(new CartVM
                    {
                        StarterMenuId = foodCart.Id,
                        DessertMenuId = foodCart.Id,
                        Count = foodCart.Count,

                    });

                }
                Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));

            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);

                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.EmailOrUsername);
                }
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }
                var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

                if (!res.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }

                List<CartVM> cartVMs = new();

                Cart dbCart = await _cartService.GetByUserIdAsync(user.Id);
                if (dbCart is not null)
                {
                    List<FoodCart> foodsCarts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                    foreach (var foodCart in foodsCarts)
                    {
                        cartVMs.Add(new CartVM
                        {
                            StarterMenuId = foodCart.Id,
                            DessertMenuId = foodCart.Id,
                            Count = foodsCarts.Count,

                        });

                    }
                    Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
                }
                return RedirectToAction("Index", "Home");


            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<CartVM> carts = _cartService.GetDatasFromCookie();


            Cart dbCart = await _cartService.GetByUserIdAsync(userId);

            if (carts.Count != null)
            {

                if (dbCart == null)
                {
                    dbCart = new()
                    {
                        AppUserId = userId,
                        foodCarts = new List<FoodCart>()
                    };
                    foreach (var cart in carts)
                    {
                        dbCart.foodCarts.Add(new FoodCart()
                        {
                          StarterMenuId=dbCart.Id, 
                            DessertMenuId = dbCart.Id,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    await _context.Carts.AddAsync(dbCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<FoodCart> foodCarts = new List<FoodCart>();
                    foreach (var cart in carts)
                    {
                        foodCarts.Add(new FoodCart()
                        {
                            StarterMenuId = dbCart.Id,
                            DessertMenuId = dbCart.Id,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    dbCart.foodCarts = foodCarts;

                    //await _context.Carts.AddAsync(dbCart);
                    _context.SaveChanges();

                }
                Response.Cookies.Delete("basket");
            }
            else
            {
                _context.Carts.Remove(dbCart);
            }



            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());


            string subject = "Verify password reset email";

            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Welcome");

            _emailService.Send(existUser.Email, subject, html);
            return RedirectToAction(nameof(VerifyEmail));
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return NotFound();
            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));
        }
    }
    }
    


     




