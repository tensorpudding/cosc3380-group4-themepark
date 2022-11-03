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

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet(int? user_id)
    {
        // Get the ASP.NET Core Identity for the request
        // Redirect user if they are not logged in
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
            return Redirect("/Login");
        }
        else
        {
            this.SetChildPageModels(role);
            Console.WriteLine("The user is named {0} and has role {1}", this.name, this.role);
            return Page();
        }
        //return Redirect("/Customer");
    }

    public IActionResult OnGetLogout()
    {
        Console.WriteLine("CHECKPOINT");
        HttpContext.SignOutAsync();
        return Redirect("/Login");
    }
}

