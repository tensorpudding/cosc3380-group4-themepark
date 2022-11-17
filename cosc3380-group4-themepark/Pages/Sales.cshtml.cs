using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark.Pages
{
    public class SalesModel : PageModel
    {
        public static string? ticketSalesArray;
        public static string? ticketSalesIncomeArray;
        public static string? merchSalesArray;
        public static string? foodSalesArray;
        public static int _year = 2022;

        public static decimal? expenses;
        public static decimal? revenue;
        public static decimal? profit;
        public static string? expenseColor;
        public static string? revenueColor;
        public static string? profitColor;

        public static List<FinanceItem>? itemizedFinances;

        public SalesModel()
        {
            _year = 2022; //find a way to dynamically get curr year?
            FetchTicketSalesInYear(_year);
            FetchMonthlyMerchSalesInYear(_year);
            FetchMonthlyFoodSalesInYear(_year);
            itemizedFinances = FetchItemizedFinances(_year);

            expenseColor = "color: " + ((expenses > 0) ? "red" : "green") + ";";
            revenueColor = "color: " + ((revenue < 0) ? "red" : "green") + ";";
            profitColor = "color: " + ((profit < 0) ? "red" : "green") + ";";
        }
        public void OnGet(int year = 2022)
        {
            _year = year;
            FetchTicketSalesInYear(year);
            FetchMonthlyMerchSalesInYear(year);
            FetchMonthlyFoodSalesInYear(year);
            itemizedFinances = FetchItemizedFinances(year);

            expenseColor = "color: " + ((expenses > 0) ? "red" : "green") + ";";
            revenueColor = "color: " + ((revenue < 0) ? "red" : "green") + ";";
            profitColor = "color: " + ((profit < 0) ? "red" : "green") + ";";

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
               int month = reader.GetInt32(0) - 1;
               MonthlyTicketSales[month] = reader.GetInt32(1);
               MonthlyIncomeFromTickets[month] = reader.GetDecimal(2);
            }
            reader.Close();
            ticketSalesArray = String.Join(",", MonthlyTicketSales);
            ticketSalesIncomeArray = String.Join(",", MonthlyIncomeFromTickets);
            revenue += MonthlyIncomeFromTickets.Sum();
        }

        private void FetchMonthlyMerchSalesInYear(int year = 2022)
        {
            List<decimal> MonthlyMerchIncome = new List<decimal> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));
            parameters.Add(new SqlParameter("@type", "Food"));

            SqlDataReader reader = SqlHelper.ExecuteProcReader("[Theme_Park].[Query_Merch_Sales_In_Year]", parameters.ToArray());

            while (reader.Read())
            {
                int month = reader.GetInt32(0) - 1;
                MonthlyMerchIncome[month] = reader.GetDecimal(1);
            }
            reader.Close();
            merchSalesArray = String.Join(",", MonthlyMerchIncome);
            revenue += MonthlyMerchIncome.Sum();
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
            revenue += MonthlyFoodIncome.Sum();
        }

        private List<FinanceItem> FetchItemizedFinances(int year = 2022)
        {
            expenses = 0;
            revenue = 0;

            List<FinanceItem> ary = new List<FinanceItem>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@year", year));

            SqlDataReader reader = SqlHelper.ExecuteProcReader("[Theme_Park].[Query_Itemized_Finances]", parameters.ToArray());

            while (reader.Read())
            {
                DateTime date = reader.GetDateTime(2);
                string type = reader.GetString(0);
                string desc = reader.GetString(1);
                decimal amount = reader.GetDecimal(3);
                ary.Add(new FinanceItem(date, type, desc, amount));
                if(type == "Revenue")
                {
                    revenue += amount;
                }
                else if(type == "Expense")
                {
                    expenses += amount;
                }
            }
            profit = revenue - expenses;
            reader.Close();

            return ary;
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

    public class FinanceItem 
    {
        public DateTime billingDate { get; set; }
        public string type { get; set; } //Expense or Revenue item
        public string desc { get; set; }
        public decimal amount { get; set; }

        public FinanceItem(DateTime _billingDate, string _type, string _desc, decimal _amount)
        {
            billingDate = _billingDate;
            type = _type;
            desc = _desc;
            amount = _amount;
        }
    }
}
