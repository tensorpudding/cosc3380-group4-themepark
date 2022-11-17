using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using cosc3380_group4_themepark.Models;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark.Pages
{

    [Authorize(Policy="SalesSpecific")]
    public class AttractionReportModel : PageModel
    {
        public class AttractionData
        {
            public String attraction_name { get; set; }
            public Int32 attraction_days_in_maintenance { get; set; }
            public Int32 attraction_rides { get; set; }
            public Decimal attraction_rides_average_this_season { get; set; }

            public AttractionData()
            {
            }
        }

        public List<AttractionData> AttractionDataList { get; set; }
        public List<Tuple<String, Int32>> top10 { get; set; }


        public void GetAttractionReports(Int32 year)
        {
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                    "[Theme_Park].[Proc_Attraction_Report_1]",
                    new SqlParameter("@year", year)
            );

            this.AttractionDataList = new List<AttractionData>();
            while (reader.Read())
            {
                AttractionData attraction = new AttractionData();
                attraction.attraction_name = reader.GetString(0);
                attraction.attraction_rides = reader.GetInt32(1);
                attraction.attraction_days_in_maintenance = reader.GetInt32(2);

                attraction.attraction_rides_average_this_season = reader.GetDecimal(3);
                this.AttractionDataList.Add(attraction);
            }
        }

        public void OnGet(Int32? year)
        {
            Int32 year_value;
            if (year == null)
            {
                year_value = DateTime.Now.Year;
            }
            else
            {
                year_value = Convert.ToInt32(year);
            }
            Console.WriteLine(year_value);
            GetAttractionReports(year_value);
        }

        public IActionResult OnPostChangeYear(Int32 year)
        {
            return RedirectToPage("/AttractionReport", new { year = year });
        }
    }
}
