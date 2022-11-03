using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models;

public class Maintenance_Calendar
{
    [BindProperty]
    public DateTime Occurence_datetime { get; set; }

    [BindProperty]
    public Int32 Vendor_ID { get; set; }

    [BindProperty]
    public Int32 Attraction_ID { get; set; }

    [BindProperty]
    public string Priority { get; set; }

    [BindProperty]
    public DateTime Maint_Start { get; set; }

    [BindProperty]
    public Int32 Maint_ID { get; set; }

    [BindProperty]
    public DateTime Maint_Completion { get; set; }

    [BindProperty]
    public Int32 Billed_Hours { get; set; }

    [BindProperty]
    public decimal Invoice_amount { get; set; }

    /*[BindProperty]
    public Byte[] Scanned_invoice { get; set; }*/

    public Maintenance_Calendar()
    {
    }
}

