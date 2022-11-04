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
        public IActionResult OnPostSubmit(MerchandiseSelection selection)
        {
            SqlDataReader reader = SqlHelper.ExecuteProcReader("Theme_Park.Merchandise_Report");

            //create list of varaibles
            this.MyMerch = new List<MerchandiseRow>();




            while (reader.Read())
            {
                MerchandiseRow current_Row = new MerchandiseRow();
                current_Row.itemName = reader.GetString(0);
                current_Row.numSold = reader.GetInt32(1);
                current_Row.price = reader.GetDecimal(2);
                current_Row.itemRevenue = reader.GetDecimal(3);
                this.MyMerch.Add(current_Row);

            }



            return Page();
            //display

        }
    }


}
