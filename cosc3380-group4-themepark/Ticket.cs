using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models;

public class Ticket
{
    [BindProperty]
    public UInt32 ticketID { get; set; }

    [BindProperty]
    public DateTime datetime { get; set; }

    [BindProperty]
    public String ticketClass { get; set; }

    [BindProperty]
    public decimal price { get; set; }

    [BindProperty]
    public UInt32 reservation { get; set; }

    public Ticket()
    {
    }
}

