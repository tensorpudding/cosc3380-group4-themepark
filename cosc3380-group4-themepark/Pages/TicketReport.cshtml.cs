using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
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

    public class TicketsByWeekdayByWeekAggregate
    {
        public List<Int32> totals_by_weekday_for_week { get; set; }

        public TicketsByWeekdayByWeekAggregate(Int32 year, Int32 week)
        {
            this.totals_by_weekday_for_week = new List<Int32>();

            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Aggregate_Ticket_Sales_By_Week_For_Given_Year]",
                new SqlParameter("@year", year));

            while (reader.Read())
            {
                if (reader.GetInt32(0) == week)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        this.totals_by_weekday_for_week.Add(reader.GetInt32(i+1));
                    }
                }
            }
        }
    }

    public class TicketReportModel : PageModel
    {
        public TicketsByWeekdayAggregate thisYearTicketsAverage { get; set; }

        public TicketsByWeekdayAggregate previousYearTicketsAverage { get; set; }

        public TicketsByWeekdayByWeekAggregate thisWeekThisYearTickets { get; set; }

        public TicketsByWeekdayByWeekAggregate thisWeekLastYearTickets { get; set; }

        public void OnGet()
        {
            // Code to use the GetWeekOfYear method was taken from the .NET documentation
            // See https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-7.0#calendars-05

            Int32 this_year = DateTime.Now.Year;
            Int32 last_year = this_year - 1;
            Calendar cal = new System.Globalization.GregorianCalendar();
            //Int32 week_number = cal.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay,DayOfWeek.Sunday);
            Int32 week_number = 22;

            // populate previous and this year tickets items
            this.thisYearTicketsAverage = new TicketsByWeekdayAggregate(this_year);
            this.previousYearTicketsAverage = new TicketsByWeekdayAggregate(last_year);

            this.thisWeekThisYearTickets = new TicketsByWeekdayByWeekAggregate(this_year, week_number);
            this.thisWeekLastYearTickets = new TicketsByWeekdayByWeekAggregate(last_year, week_number);
        }
    }
}
