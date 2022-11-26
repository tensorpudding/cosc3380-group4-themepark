using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/* Attractions.cs
 *
 * Auxilliary class for encapsulating information about Attractions
 */
namespace cosc3380_group4_themepark.Models
{
    public class Attraction
    {
        [BindProperty]
        public String name { get; set; }

        [BindProperty]
        public Int32 attraction_id { get; set; }
    }
}
