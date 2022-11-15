using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using cosc3380_group4_themepark.Models;

namespace cosc3380_group4_themepark.Pages
{
    [Authorize(Policy="CustomerSpecific")]
    public class TicketsModel : PageModel
    {

        public int customer_id { get; set; }

        public String username { get; set; }

        public List<TicketReservation> MyTickets { get; set; }

        public String today { get; set; }

        public IActionResult OnGet()
        {
            // Initialize context information using authentication cookie

            String? username = GetUsernameFromContext();
            if (username == null)
                return Redirect("/");
            else
                this.username = username;
            Int32? customer_id = GetCustomerID(this.username);
            if (customer_id == null)
                return Redirect("/");
            else
                this.customer_id = customer_id.Value;
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Customer_Get_Current_Reservations]",
                new SqlParameter("@customer_id", this.customer_id));

            this.MyTickets = new List<TicketReservation>();

            // We set the date here for the purposes of our form's initial value and for date validation
            // Customers should not be able to choose dates in the past for reservations

            this.today = DateTime.Now.Date.ToString("yyyy-MM-dd");

            while (reader.Read())
            {
                TicketReservation current_Reservation = new TicketReservation();
                current_Reservation.Reservation_ID = reader.GetInt32(0);
                current_Reservation.Date_Placed = reader.GetDateTime(1);
                current_Reservation.Date_of_Visit = reader.GetDateTime(2);
                current_Reservation.Ticket_Class = reader.GetString(3);
                current_Reservation.Price = reader.GetDecimal(4);
                current_Reservation.FirstName = reader.GetString(6);
                current_Reservation.LastName = reader.GetString(7);
                if (reader.IsDBNull(5) == false)
                    current_Reservation.Ticket_ID = reader.GetInt32(5);
                this.MyTickets.Add(current_Reservation);
            }

            return Page();
        }

        public String? GetUsernameFromContext()
        {
            foreach (var claim in HttpContext.User.Claims)
            {
                //Console.WriteLine("Found a claim of type {0}, value {1}", claim.Type, claim.Value);
                if (claim.Type == "Username")
                {
                    return claim.Value;
                }
            }
            return null;
        }

        public Int32? GetCustomerID(String username)
        {
            SqlDataReader reader = SqlHelper.ExecuteQueryReader(
                "SELECT customer_id FROM [Theme_Park].[Customer] WHERE username='" + username + "';");
            while (reader.Read())
            {
                return reader.GetInt32(0);
            }
            return null;
        }

        public Int64 ParseCCNumber(String ccnumber)
        {
            // We accept a string from the form, now we can convert it to a number
            return Int64.Parse(ccnumber.Replace(" ", ""));
        }

        public IActionResult OnPostBuyTicket(TicketReservation reservation)
        {
            String? username = GetUsernameFromContext();
            if (username == null)
                return Redirect("/");
            Int32? customer_id = GetCustomerID(username);
            if (customer_id == null)
                return Redirect("/");
            else
                this.customer_id = customer_id.Value;
            Int32 rows_affected = SqlHelper.ExecuteProcNonQuery(
                "[Theme_Park].[Proc_Customer_Buy_Reservations]",
                new SqlParameter("@customer_id", this.customer_id),
                new SqlParameter("@FirstName", reservation.FirstName),
                new SqlParameter("@LastName", reservation.LastName),
                new SqlParameter("@Date_of_Visit", reservation.Date_of_Visit),
                new SqlParameter("@Credit_Card_Number", ParseCCNumber(reservation.Credit_Card_Number)),
                new SqlParameter("@Ticket_Class", reservation.Ticket_Class));
            if (rows_affected == 1)
            {
                // do something to say we have bought a ticket
            }
            else
            {
                // do something to say we have failed to buy a ticket
            }
            Console.WriteLine("The current value of customer_id is {0}", this.customer_id);
            return Redirect("/Tickets");
        }

        public IActionResult OnPostCancelReservation(TicketReservation reservation)
        {
            if (reservation.Reservation_ID == 0)
            {
                Console.WriteLine("ERROR: attempted to cancel non-existent reservation {0}", reservation.Reservation_ID);
            }
            Int32 rows_affected = SqlHelper.ExecuteProcNonQuery(
                "[Theme_Park].[Proc_Customer_Cancel_Reservation]",
                new SqlParameter("@Reservation_ID", reservation.Reservation_ID));
            if (rows_affected == 0)
            {
                Console.WriteLine("ERROR: attempted to cancel non-existent reservation {0}", reservation.Reservation_ID);
            }
            return Redirect("/Tickets");
        }
    }
}
