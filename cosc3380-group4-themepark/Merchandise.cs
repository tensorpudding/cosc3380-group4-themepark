using System;
using Microsoft.AspNetCore.Mvc;

namespace cosc3380_group4_themepark;

/* Merchandise.cs
 *
 * Class which encapsulates the Merchandise table
 */
public class Merchandise
{
    [BindProperty]
    public String itemType { get; set; }

    [BindProperty]
    public String itemName { get; set; }

    [BindProperty]
    public decimal price { get; set; }

    public Merchandise()
    {
    }
}


