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
    [Authorize(Policy="SalesSpecific")]
    public class AttractionReportModel : PageModel
    {


        public List<AttractionData> AttractionDataList { get; set; }

        public Int32 _year {get; set;}


        public List<AttractionData> GetAttractionReportsByYear(Int32 year)
        {
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                    "[Theme_Park].[Proc_Attraction_Report_1]",
                    new SqlParameter("@year", year)
            );

            var AttractionDataList = new List<AttractionData>();
            while (reader.Read())
            {
                AttractionData attraction = new AttractionData();
                attraction.attraction_name = reader.GetString(0);
                attraction.attraction_rides = reader.GetInt32(1);
                attraction.attraction_days_in_maintenance = reader.GetInt32(2);

                attraction.attraction_rides_average_this_season = reader.GetDecimal(3);
                AttractionDataList.Add(attraction);
            }

            return AttractionDataList;
        }

        public List<AttractionData> GetAttractionReportsByRange(DateTime startdate, DateTime enddate)
        {
            SqlDataReader reader = SqlHelper.ExecuteProcReader(
                "[Theme_Park].[Proc_Attraction_Report_3]",
                new SqlParameter("@startdate", startdate),
                new SqlParameter("@enddate", enddate)
            );

            var AttractionDataList = new List<AttractionData>();
            while (reader.Read())
            {
                AttractionData attraction = new AttractionData();
                attraction.attraction_name = reader.GetString(0);
                attraction.attraction_rides = reader.GetInt32(1);
                attraction.attraction_days_in_maintenance = reader.GetInt32(2);

                attraction.attraction_rides_average_this_season = reader.GetDecimal(3);
                AttractionDataList.Add(attraction);
            }

            return AttractionDataList;
        }

        public PartialViewResult OnGetAttractionReport(String checkyearorrange, Int32? year, String? startdate, String? enddate)
        {
            this.AttractionDataList = new List<AttractionData>();
            Console.WriteLine("Debug test OnPostGenerateItemized");
            if (checkyearorrange == "year")
            {
                this.AttractionDataList = GetAttractionReportsByYear(year.GetValueOrDefault());
            }
            else if (checkyearorrange == "range")
            {
                this.AttractionDataList = GetAttractionReportsByRange(DateTime.Parse(startdate), 
                                                     DateTime.Parse(enddate));
            }
            return Partial("_AttractionReport", this.AttractionDataList);
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
            this._year = year_value;
            Console.WriteLine(year_value);
            GetAttractionReportsByYear(year_value);
        }

        public IActionResult OnPostChangeYear(Int32 year)
        {
            return RedirectToPage("/AttractionReport", new { year = year });
        }
    }
}
