using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark.Pages
{
    public class SalesModel : PageModel
    {
        public static int Test; //Delete Later
        public static string? ticketSalesArray;
        public static string? ticketSalesIncomeArray;
        public static string? merchSalesArray; //Convert to json
        public static string? foodSalesArray;
        public static int _year = 2022;
        
        public void OnGet(int year=2022)
        {
            _year = year;
            FetchTicketSalesInYear(year);
            FetchMonthlyMerchSalesInYear(year);
            FetchMonthlyFoodSalesInYear(year);
        }


        private void FetchTicketSalesInYear(int year = 2022) // I don't think there's a way to make param default to current year during compile time
        {
            List<int> MonthlyTicketSales = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<decimal> MonthlyIncomeFromTickets = new List<decimal> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            SqlDataReader reader = SqlHelper.ExecuteProcReader("[Theme_Park].[Proc_Query_Ticket_Purchases_Aggregate_In_Year]", parameters.ToArray());
            while (reader.Read()) 
            {
                int month = reader.GetInt32(0)-1;
                MonthlyTicketSales[month] = reader.GetInt32(1);
                MonthlyIncomeFromTickets[month] = reader.GetDecimal(2);
            }
            reader.Close();
            ticketSalesArray = String.Join(",", MonthlyTicketSales);
            ticketSalesIncomeArray = String.Join(",", MonthlyIncomeFromTickets);
            
        }

        private void FetchMonthlyMerchSalesInYear(int year = 2022)
        {
            List<decimal> MonthlyMerchIncome = new List<decimal> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            parameters.Add(new SqlParameter("@type", "Merch"));

            SqlDataReader reader = SqlHelper.ExecuteProcReader("[Theme_Park].[Query_Merch_Sales_In_Year]", parameters.ToArray());

            while (reader.Read())
            {
                int month = reader.GetInt32(0) - 1;
                MonthlyMerchIncome[month] = reader.GetDecimal(1);
            }
            reader.Close();
            merchSalesArray = String.Join(",", MonthlyMerchIncome);
        }
        private void FetchMonthlyFoodSalesInYear(int year = 2022)
        {
            List<decimal> MonthlyFoodIncome = new List<decimal> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            parameters.Add(new SqlParameter("@type", "Merch"));

            SqlDataReader reader = SqlHelper.ExecuteProcReader("[Theme_Park].[Query_Merch_Sales_In_Year]", parameters.ToArray());

            while (reader.Read())
            {
                int month = reader.GetInt32(0) - 1;
                MonthlyFoodIncome[month] = reader.GetDecimal(1);
            }
            reader.Close();
            foodSalesArray = String.Join(",", MonthlyFoodIncome);
        }

        private void FetchAllSalesInYear(int year = 2022)
        {
            
        }
        private string FetchTicketSalesOnYearAndType(int year, string ticketClass) // Do this after Checkpoint 3
        {
            return ("");
        }
        

        
        public void OnPostSubmit() // Do this after checkpoint
        {
            //year = Int32.Parse(Request.Form["yearSelect"]);
            //Console.WriteLine(Request.Form["yearSelect"]);
            //year = int.Parse(Request.Form["yearSelect"]);
            //Console.WriteLine("Pog");
        }
        
    }
}
