using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/* Vendor.cs
 * 
 * Class definition to encapsulate the Vendor table
 */


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
