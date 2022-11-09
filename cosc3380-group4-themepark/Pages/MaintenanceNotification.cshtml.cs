using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using cosc3380_group4_themepark.Models;

namespace cosc3380_group4_themepark.Pages
{
    [Authorize]
    public class MaintenanceNotificationModel : PageModel
    {
        public List<MaintenanceCalendarNotification> Notifications { get; set; }

        public void OnGet()
        {
            this.Notifications = new List<MaintenanceCalendarNotification>();

            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Get_Maintenance_Notifications]");

            while (reader.Read())
            {
                MaintenanceCalendarNotification notification = new MaintenanceCalendarNotification();
                notification.notification_id = reader.GetInt32(0);
                notification.maintenance_id = reader.GetInt32(1);
                notification.completion = reader.GetDateTime(2);
                if (!reader.IsDBNull(3))
                    notification.billed_hours = reader.GetInt32(3);
                else
                    notification.billed_hours = null;
                if (!reader.IsDBNull(4))
                    notification.amount = reader.GetDecimal(4);
                else
                    notification.amount = null;
                if (!reader.IsDBNull(5))
                    notification.description = reader.GetString(5);
                else
                    notification.description = null;
                this.Notifications.Add(notification);
            }
        }

        public IActionResult OnPostClearNotifications()
        {
            Int32 rows_affected = SqlHelper.ExecuteProcNonQuery(
                "[Theme_Park].[Proc_Mark_Maintenance_Notifications_Read]");

            return Redirect("/MaintenanceNotification");
        }
    }
}
