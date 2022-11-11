using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models
{
    public class ManagerNotification
    {
        [BindProperty]
        public String employee_fname { get; set; }

        [BindProperty]
        public String employee_lname { get; set; }

        [BindProperty]
        public String dept_name { get; set; }

        [BindProperty]
        public String role { get; set; }

        [BindProperty]
        public Decimal payrate { get; set; }

        [BindProperty]
        public Boolean salaried { get; set; }

        [BindProperty]
        public String super_fname { get; set; }

        [BindProperty]
        public String super_lname { get; set; }

        [BindProperty]
        public Char type { get; set; }


        public ManagerNotification()
        {
        }
    }
}

