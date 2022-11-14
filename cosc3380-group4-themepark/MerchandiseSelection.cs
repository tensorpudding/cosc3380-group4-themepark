using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models;

public class MerchandiseSelection
{
    [BindProperty]
    public DateTime starttime { get; set; }

    [BindProperty]
    public DateTime endtime { get; set; }

    [BindProperty]
    public String type { get; set; }

    public MerchandiseSelection()
    {
    }
}

