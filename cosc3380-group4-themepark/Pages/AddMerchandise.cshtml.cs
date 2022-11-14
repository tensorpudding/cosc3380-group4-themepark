using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace cosc3380_group4_themepark.Pages;

public class AddMerchandiseModel : PageModel
{
    public void OnGet()
    {


    }

    public IActionResult OnPostAddMerchandise(Merchandise merchandise)
    {
        Console.WriteLine(merchandise.itemName);


        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery(
          "[Theme_Park].[AddMerchandise]",
          new SqlParameter("@itemname", merchandise.itemName),
          new SqlParameter("@price", merchandise.price),
          new SqlParameter("@type", merchandise.itemType));



        return Page();
    }
}



