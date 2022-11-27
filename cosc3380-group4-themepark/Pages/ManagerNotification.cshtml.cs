using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

using cosc3380_group4_themepark.Models;

namespace cosc3380_group4_themepark.Pages
{
    [Authorize(Policy="ManagerSpecific")]
    public class ManagerNotificationModel : PageModel
    {
        public List<ManagerNotification> myPayNotifications { get; set; }

        public List<ManagerNotification> myRoleNotifications { get; set; }

        public String username { get; set; }

        public String? GetUsernameFromContext()
        {
            foreach (var claim in HttpContext.User.Claims)
            {
                if (claim.Type == "Username")
                {
                    return claim.Value;
                }
            }
            return null;
        }

        public IActionResult OnGet()
        {
            String? username = GetUsernameFromContext();
            if (username == null)
                return Redirect("/");
            else
                this.username = username;
            Console.WriteLine(username);
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Get_Manager_Notifications]",
                new SqlParameter("@username", username));

            this.myPayNotifications = new List<ManagerNotification>();
            this.myRoleNotifications = new List<ManagerNotification>();

            while (reader.Read())
            {
                ManagerNotification notification = new ManagerNotification();
                notification.employee_fname = reader.GetString(1);
                notification.employee_lname = reader.GetString(2);
                notification.dept_name = reader.GetString(3);
                notification.role = reader.GetString(4);
                notification.payrate = reader.GetDecimal(5);
                notification.salaried = reader.GetBoolean(6);
                notification.super_fname = reader.GetString(7);
                notification.super_lname = reader.GetString(8);
                Char type = reader.GetString(9)[0];
                if (type == 'P')
                {
                    this.myPayNotifications.Add(notification);
                }
                else if (type == 'R')
                {
                    this.myRoleNotifications.Add(notification);
                }
            }

            return Page();
        }

        public IActionResult OnPostClearNotification()
        {  
            String? username = GetUsernameFromContext();
            Console.WriteLine("Debug: we are trying to clear notifications");
            if (username == null)
            {
                return Redirect("/");
            }

            SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_Clear_Manager_Notifications]",
                new SqlParameter("@username", username));
            return Redirect("/Internal");
        }
    }
}
