using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/* Description.cs
 *
 * Auxilliary class to the Maintenance form
 */

namespace cosc3380_group4_themepark.Models
{
    public class Description
    {
        [BindProperty]
        public String Maint_Description { get; set; }

        [BindProperty]
        public Int32 Maint_ID { get; set; }
    }
}
