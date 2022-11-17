using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace cosc3380_group4_themepark.Pages;

public class Maintenance_CalendarModel : PageModel
{

    public List<Vendor> vendors { get; set; }
    public List<Attraction> attractions { get; set; }
    public List<Description> descriptions { get; set; }
    public List<Jobs> jobs { get; set; }
    public String today { get; set; }

    private readonly ILogger<Maintenance_CalendarModel> _logger;

    public Maintenance_CalendarModel(ILogger<Maintenance_CalendarModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        SqlDataReader VendReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[Proc_Get_Vendors]");
        this.vendors = new List<Vendor>();

        while (VendReader.Read())
        {
            Vendor vendor = new Vendor();
            vendor.vendor_id = VendReader.GetInt32(1);
            vendor.name = VendReader.GetString(0);
            this.vendors.Add(vendor);
        }

        SqlDataReader AttReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[Proc_Get_Attractions]");
        this.attractions = new List<Attraction>();

        while (AttReader.Read())
        {
            Attraction attraction = new Attraction();
            attraction.attraction_id = AttReader.GetInt32(1);
            attraction.name = AttReader.GetString(0);
            this.attractions.Add(attraction);
        }

        SqlDataReader DescReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[proc_see_maint]");
        this.descriptions = new List<Description>();

        while (DescReader.Read())
        {
            Description description = new Description();
            description.Maint_ID = DescReader.GetInt32(1);
            description.Maint_Description = DescReader.GetString(0);
            this.descriptions.Add(description);
        }

        SqlDataReader JobsReader = SqlHelper.ExecuteProcReader(
           "[Theme_Park].[proc_open_jobs]");
        this.jobs = new List<Jobs>();

        while (JobsReader.Read())
        {
            Jobs job = new Jobs();
            job.Attraction = JobsReader.GetString(0);
            job.Vendor = JobsReader.GetString(1);
            job.Maint_Description = JobsReader.GetString(3);
            job.Priority = JobsReader.GetString(2);
            job.Maint_ID = JobsReader.GetInt32(4);
            jobs.Add(job);
        }

        return Page();
    }

    public IActionResult OnPostCreateTicket(Maintenance_Calendar OpenTicket)
    {
        Console.WriteLine("Occurance date is {0}", OpenTicket.Occurence_datetime);
        this.today = DateTime.Now.Date.ToString("yyyy-MM-dd");

        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Occurence_datetime", OpenTicket.Occurence_datetime));
        parameters.Add(new SqlParameter("@Vendor_ID", OpenTicket.Vendor_ID));
        parameters.Add(new SqlParameter("@Attraction_ID", OpenTicket.Attraction_ID));
        parameters.Add(new SqlParameter("@Priority", OpenTicket.Priority));
        parameters.Add(new SqlParameter("@Maint_Description", OpenTicket.Maint_Description));

        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_Add_Maint]", parameters.ToArray());
        return Redirect("/Maintenance_Calendar");

    }

    public IActionResult OnPostCloseTicket(Maintenance_Calendar CloseTicket)
    {
        Console.WriteLine("Occurance date is {0}", CloseTicket.Maint_Completion);
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Maint_ID", CloseTicket.Maint_ID));
        parameters.Add(new SqlParameter("@Maint_Start", CloseTicket.Maint_Start));
        parameters.Add(new SqlParameter("@Maint_Completion", CloseTicket.Maint_Completion));
        parameters.Add(new SqlParameter("@Billed_Hours", CloseTicket.Billed_Hours));
        parameters.Add(new SqlParameter("@Invoice_amount", CloseTicket.Invoice_amount));
        /*parameters.Add(new SqlParameter(null, CloseTicket.Scanned_invoice));*/

        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_End_Maint]", parameters.ToArray());
        return Redirect("/Maintenance_Calendar");

    }
}