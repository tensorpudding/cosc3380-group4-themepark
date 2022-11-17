using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using cosc3380_group4_themepark.Models;

namespace cosc3380_group4_themepark.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public String role { get; set; }

    public String? name { get; set; }

    public MyViewModels models = new MyViewModels();
    public LoginModel loginModel { get; set; }
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult? OnGet(int? user_id)
    {
        models = new MyViewModels();

        //// Get the ASP.NET Core Identity for the request
        //// Redirect user if they are not logged in
        var user = HttpContext.User;
        this.name = null;
        foreach (var claim in user.Claims)
        {
            Console.WriteLine("Found a claim of type {0}, value {1}", claim.Type, claim.Value);
            if (claim.Type == "Username")
            {
                this.name = claim.Value;
                Console.WriteLine("CHECKPOINT");
            }
            if (claim.Type == "Role")
            {
                this.role = claim.Value;
            }
        }
        if (this.name is null)
        {
            Console.WriteLine("Login failed!");
        }
        else
        {
            Console.WriteLine("The user is named {0} and has role {1}", this.name, this.role);
        }
        return Page();
    }

    public IActionResult OnGetLogout()
    {
        Console.WriteLine("CHECKPOINT");
        HttpContext.SignOutAsync();
        return Redirect("/Login");
    }

    public IActionResult? OnPostLogin(string username, string password)
    {
        Console.WriteLine("Attempting to Log in");
        SqlDataReader reader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[Proc_Validate_Login]",
            new SqlParameter("@username", username),
            new SqlParameter("@password", password));
        Console.WriteLine("We received rows: {0}", reader.HasRows);
        if (reader.HasRows)
        {
            reader.Read();

            //_logger.LogDebug("We have authenticated");
            String role = reader.GetString(0);
            //_logger.LogDebug("We have authenticated user {username} as having role {role}", username, role);
            Console.WriteLine("We have authenticated user {0} as having role {1}", username, role);
            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("Role", role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            

            Console.WriteLine("You successfully logged in as " + username);
            return null;
        }
        else
        {
            Console.Write("You failed a login");
            return null;
        }
        return Page();
    }
}

