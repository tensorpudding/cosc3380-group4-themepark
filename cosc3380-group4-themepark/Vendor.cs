using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace cosc3380_group4_themepark.Models
{
    public class Vendor
    {
        [BindProperty]
        public String name { get; set; }

        [BindProperty]
        public Int32 vendor_id { get; set; }
    }
}
