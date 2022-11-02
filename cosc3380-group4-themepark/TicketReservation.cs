using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models;

public class TicketReservation
{
    [BindProperty]
    public Int32 Reservation_ID { get; set; }

    [BindProperty]
    public Int32? Customer_ID { get; set; }

    [BindProperty]
    public String FirstName { get; set; }

    [BindProperty]
    public String LastName { get; set; }

    [BindProperty]
    public DateTime Date_Placed { get; set; }

    [BindProperty]
    public DateTime Date_of_Visit { get; set; }

    [BindProperty]
    public Int64 Credit_Card_Number { get; set; }

    [BindProperty]
    public String Ticket_Class { get; set; }

    [BindProperty]
    public bool? Expired { get; set; }

    [BindProperty]
    public decimal Price { get; set; }

    [BindProperty]
    public int? Ticket_ID { get; set; }

    public TicketReservation()
    {
    }
}

