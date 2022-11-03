using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace cosc3380_group4_themepark.Models
{
    public class Departments
    {

        [BindProperty]
        public int dept_id { get; set; }

        [BindProperty]
        public String dept_name { get; set; }

        [BindProperty]
        public int? supervisor_ssn { get; set; }

        public Departments()
        {
        }
    }
}