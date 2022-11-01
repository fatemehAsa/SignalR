using Dtos.AspNet;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        public AccountController(SignInManager<AspNetUser> signInManager, UserManager<AspNetUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = new AspNetUser { UserName = registerDto.Email, Email = registerDto.Email };
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                    return View(registerDto);
                }
            }

            return View(registerDto);
        }

        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        private string CreateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = string.Empty;
            var key = Encoding.ASCII.GetBytes("SDFsf35rsdf$TGsdf344");
            var claims = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, email) });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto, string returnUrl = "")
        {

            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = CreateToken(loginDto.Email);
                if (returnUrl != "")
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                return View(loginDto);
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
