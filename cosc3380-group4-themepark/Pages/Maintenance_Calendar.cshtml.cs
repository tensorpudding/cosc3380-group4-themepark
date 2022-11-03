using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;
using Microsoft.IdentityModel.Tokens;

namespace cosc3380_group4_themepark.Pages;

public class Maintenance_CalendarModel : PageModel
{
    private readonly ILogger<Maintenance_CalendarModel> _logger;

    public Maintenance_CalendarModel(ILogger<Maintenance_CalendarModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnPostCreateTicket(Maintenance_Calendar OpenTicket)
    {
        Console.WriteLine("Occurance date is {0}", OpenTicket.Occurence_datetime);
        Console.WriteLine("Maint start date is {0}", OpenTicket.Maint_Start);

        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Occurence_datetime", OpenTicket.Occurence_datetime));
        parameters.Add(new SqlParameter("@Vendor_ID", OpenTicket.Vendor_ID));
        parameters.Add(new SqlParameter("@Attraction_ID", OpenTicket.Attraction_ID));
        parameters.Add(new SqlParameter("@Priority", OpenTicket.Priority));
        parameters.Add(new SqlParameter("@Maint_Start", OpenTicket.Maint_Start));
  

        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_Add_Maint]", parameters.ToArray());
        return Redirect("/Maintenance_Calendar");

    }

    public IActionResult OnPostCloseTicket(Maintenance_Calendar CloseTicket)
    {
        Console.WriteLine("Occurance date is {0}", CloseTicket.Maint_Completion);
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Maint_ID", CloseTicket.Maint_ID));
        parameters.Add(new SqlParameter("@Maint_Completion", CloseTicket.Maint_Completion));
        parameters.Add(new SqlParameter("@Billed_Hours", CloseTicket.Billed_Hours));
        parameters.Add(new SqlParameter("@Invoice_amount", CloseTicket.Invoice_amount));
        /*parameters.Add(new SqlParameter(null, CloseTicket.Scanned_invoice));*/

        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_End_Maint]", parameters.ToArray());
        return Redirect("/Maintenance_Calendar");

    }
}