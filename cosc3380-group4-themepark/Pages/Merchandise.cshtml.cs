using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;

namespace cosc3380_group4_themepark.Pages
{
    public class MerchandiseModel : PageModel
    {





                
        public List<MerchandiseRow> MyMerch { get; set; }



        public void OnGet()
        {
            this.MyMerch = new List<MerchandiseRow>();

        }
        public IActionResult OnPostMerchandise(MerchandiseSelection selection)
        {

            SqlDataReader reader;
            Console.WriteLine(selection.type);
            Console.WriteLine(selection.starttime);
            Console.WriteLine(selection.endtime);


            if (selection.type == null && (selection.starttime == default(DateTime)) || selection.endtime == default(DateTime))
            {
                Console.WriteLine("1");
                reader = SqlHelper.ExecuteProcReader("Theme_Park.Merchandise_Report");
            }
            else if (selection.type == null)
            {
                Console.WriteLine("2");
                reader = SqlHelper.ExecuteProcReader("Theme_Park.Merchandise_Report_Datetime", new SqlParameter("starttime", selection.starttime),
                 new SqlParameter("endtime", selection.endtime));
            }
            else if(selection.starttime == default(DateTime) || selection.endtime == default(DateTime))
            {
                Console.WriteLine("3");
                reader = SqlHelper.ExecuteProcReader("Theme_Park.Merchandise_Report_Type", new SqlParameter("@type", selection.type));
            }
            else
            {
                Console.WriteLine("4");
                reader = SqlHelper.ExecuteProcReader("Theme_Park.Proc_Merchandise_Report_Date", new SqlParameter("starttime", selection.starttime),
                 new SqlParameter("endtime", selection.endtime),
                 new SqlParameter("@type", selection.type));
            }

            

            //create list of varaibles
            this.MyMerch = new List<MerchandiseRow>();




            while (reader.Read())
            {
                MerchandiseRow current_Row = new MerchandiseRow();
                current_Row.itemName = reader.GetString(0);
                current_Row.numSold = reader.GetInt32(1);
                current_Row.price = reader.GetDecimal(2);
                current_Row.itemRevenue = current_Row.numSold * current_Row.price;
                current_Row.mType = reader.GetString(3);
                this.MyMerch.Add(current_Row);
            }



            return Page();
            //display

        }
    }


}
