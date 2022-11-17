
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
        //private readonly ILogger<LoginModel>? _logger;
        private readonly IHttpContextAccessor? _contextAccessor;
        public string? username { get; set; }
        public string? password { get; set; }



        public void OnGet()
        {

        }


        //public IActionResult? OnPostLogin(string username, string password, IHttpContextAccessor? _context = null)
        public IActionResult? OnPostLogin(string username, string password, HttpContext? _context = null)
        {
            Console.WriteLine("Attempting ot Log In ");
            Console.WriteLine("Username is " + username);
            //return null;
            return Redirect("/Index");
            //Console.WriteLine("Attempting to Log in");
            //SqlDataReader reader = SqlHelper.ExecuteProcReader(
            //    "[Theme_Park].[Proc_Validate_Login]",
            //    new SqlParameter("@username", username),
            //    new SqlParameter("@password", password));
            //Console.WriteLine("We received rows: {0}", reader.HasRows);
            //if (reader.HasRows)
            //{
            //    reader.Read();

            //    //_logger.LogDebug("We have authenticated");
            //    String role = reader.GetString(0);
            //    //_logger.LogDebug("We have authenticated user {username} as having role {role}", username, role);
            //    Console.WriteLine("We have authenticated user {0} as having role {1}", username, role);
            //    var claims = new List<Claim>
            //    {
            //        new Claim("Username", username),
            //        new Claim("Role", role)
            //    };

            //    var claimsIdentity = new ClaimsIdentity(claims, "Login");

            //    if (_context != null)
            //    {
            //        _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //            new ClaimsPrincipal(claimsIdentity));
            //    }
            //    else
            //    {
            //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //            new ClaimsPrincipal(claimsIdentity));
            //    }

            //    Console.WriteLine("You successfully logged in as " + username);
            //    return null;
            //    return RedirectToPage("/CustomerFacing");
            //}
            //else
            //{
            //    Console.Write("You failed a login");
            //    return null;
            //    return Redirect("/LoginFailed");
            //}
        }
         
        public LoginModel(IHttpContextAccessor? contextAccessor = null)
        {
            username = null;
            password = null;
            _contextAccessor = contextAccessor;
        }
        //public LoginModel(ILogger<LoginModel> logger )
        //{
        //    _logger = logger;
        //}
    }
}
