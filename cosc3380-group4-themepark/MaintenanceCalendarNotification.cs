using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/* MaintenanceCalenderNotification.cs
 *
 * Class encapsulating MaintenanceNotification table
 */
namespace cosc3380_group4_themepark.Models
{
    public class MaintenanceCalendarNotification
    {
        [BindProperty]
        public Int32 notification_id { get; set; }

        [BindProperty]
        public Int32 maintenance_id { get; set; }

        [BindProperty]
        public DateTime completion { get; set; }

        [BindProperty]
        public Int32? billed_hours { get; set; }

        [BindProperty]
        public decimal? amount { get; set; }

        [BindProperty]
        public String? description { get; set; }

        public MaintenanceCalendarNotification()
        {
        }
    }
}

