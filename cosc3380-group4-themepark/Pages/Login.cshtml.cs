
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using System.Security.Claims;

using cosc3380_group4_themepark;
using cosc3380_group4_themepark.Models;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(String username, String password)
        {
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Validate_Login]",
                new SqlParameter("@username", username),
                new SqlParameter("@password", password));
            Console.WriteLine("We received rows: {0}", reader.HasRows);
            if (reader.HasRows)
            {
                reader.Read();

                _logger.LogDebug("We have authenticated");
                String role = reader.GetString(0);
                _logger.LogDebug("We have authenticated user {username} as having role {role}", username, role);
                Console.WriteLine("We have authenticated user {0} as having role {1}", username, role);
                var claims = new List<Claim>
                {
                    new Claim("Username", username),
                    new Claim("Role", role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/Index");
            }
            else
            {
                return Redirect("/LoginFailed");
            }
        }

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }
    }
}
