using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

using cosc3380_group4_themepark.Models;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark.Pages
{
    public class TicketsByWeekdayAggregate
    {
        public List<IDictionary<String, Int32>> totals_by_weekday { get; set; }
        
        public TicketsByWeekdayAggregate(Int32 year)
        {
            this.totals_by_weekday = new List<IDictionary<String, Int32>>();
            for (int i = 0; i < 7; i++)
            {
                this.totals_by_weekday.Add(new Dictionary<String, Int32>());
            }

            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Aggregate_Ticket_Sales_By_Weekday]",
                new SqlParameter("@year", year));

            while (reader.Read())
            {
                String ticketClass = reader.GetString(0);
                for (int i = 0; i < 7; i++)
                {
                    this.totals_by_weekday[i][ticketClass] = reader.GetInt32(i + 1);
                }
            }
        }
    }

    public class TicketReportModel : PageModel
    {
        public TicketsByWeekdayAggregate thisYearTickets { get; set; }

        public TicketsByWeekdayAggregate previousYearTickets { get; set; }

        public void OnGet()
        {
            // populate previous and this year tickets items
            this.thisYearTickets = new TicketsByWeekdayAggregate(2022);
            this.previousYearTickets = new TicketsByWeekdayAggregate(2021);

            Int32 i = 0;
            foreach (Dictionary<String, Int32> agg in this.thisYearTickets.totals_by_weekday)
            {
                Console.WriteLine("The total for day {0} is: ", i);
                foreach (var ticket_class in agg.Keys)
                {
                    Console.WriteLine("{0}: {1}", ticket_class, agg[ticket_class]);
                }
                Console.WriteLine("");
                i++;
            }
        }
    }
}
