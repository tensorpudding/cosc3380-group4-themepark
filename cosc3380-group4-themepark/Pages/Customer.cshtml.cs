using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
namespace cosc3380_group4_themepark
{
    public class CustomerModel : PageModel
    {
        public bool TicketPurchaseSuccess { get; set; }

        public void OnGet()
        {
        }

        public void OnPostBuyTicket(Ticket ticket)
        {
            Int32 rows = SqlHelper.ExecuteProcNonQuery(
                "[Theme_Park].[Proc_Customer_Buy_Ticket]",
                new SqlParameter("@date", ticket.datetime),
                new SqlParameter("@class", ticket.ticketClass),
                new SqlParameter("@price", 11.50)
                );
            if (rows == 0)
            {
                this.TicketPurchaseSuccess = false;
            }
            else
            {
                this.TicketPurchaseSuccess = true;
            }
            Console.WriteLine("The result was {0}", this.TicketPurchaseSuccess);
        }
    }
}
