using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


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
