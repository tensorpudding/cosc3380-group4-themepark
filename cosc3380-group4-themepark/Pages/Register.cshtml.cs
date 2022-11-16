
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
    public class RegisterModel : PageModel
    {

        public void OnGet(String? success)
        {
            if (success == null || success == "1")
            {
                // do something
            }
        }

        public IActionResult OnPostRegister(String username, String password, String name, String email)
        {
            Int32 rows = SqlHelper.ExecuteProcNonQuery(
                "[Theme_Park].[Proc_Register_Customer_Login]",
                new SqlParameter("@username", username),
                new SqlParameter("@password", password),
                new SqlParameter("@name", name),
                new SqlParameter("@email", email));
            if (rows > 0)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                return RedirectToPage("/Register", new { success = "0" });
            }
        }
    }
}
