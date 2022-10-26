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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cosc3380_group4_themepark.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnPostLogin(string username, string password, string returnURL)
        {
            Console.WriteLine("We are in the Login action");
            // Here we need to use the SQL database Login table to verify a valid login and get the user role
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@username", username));
            parameters.Add(new SqlParameter("@password", password));
            SqlDataReader? reader = SqlHelper.ExecProc("[Theme_Park].[Proc_Validate_Login]", parameters.ToArray());

            if (reader is not null && reader.HasRows)
            {
                reader.Read();
                string role = reader.GetString(0);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                Console.WriteLine("We have been authenticated and logged in!");
                return Redirect(returnURL == null ? "/Secured" : returnURL);
            }
            else
            {
                Console.WriteLine("We failed our authentication");
                return Redirect("/LoginFailed");
            }

        }
    }
}

