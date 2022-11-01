using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;

namespace cosc3380_group4_themepark.Pages;

public class Maintenance_CalendarModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<EmployeeEntryModel> _logger;

    public Maintenance_CalendarModel(ILogger<Maintenance_CalendarModel> logger)
    {
        _logger = logger;
    }

    public void OnPostSubmit(Maintenance_CalendarModel Maintenance_Calendar)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Maint_ID", Maintenance_Calendar.Maint_ID));
        parameters.Add(new SqlParameter("@Occurence_date", Maintenance_Calendar.Occurence_date));
        parameters.Add(new SqlParameter("@Vendor_ID", Maintenance_Calendar.Vendor_ID));
        parameters.Add(new SqlParameter("@AttractionID", Maintenance_Calendar.gender));
        parameters.Add(new SqlParameter("@address", Maintenance_Calendar.address));
        parameters.Add(new SqlParameter("@phone", Maintenance_Calendar.phone));
        parameters.Add(new SqlParameter("@date_joined", Maintenance_Calendar.date_joined));
        parameters.Add(new SqlParameter("@dept_id", Maintenance_Calendar.dept_id));
        parameters.Add(new SqlParameter("@supervisor_ssn", Maintenance_Calendar.supervisor_ssn));
        parameters.Add(new SqlParameter("@salaried", Maintenance_Calendar.salaried));
        parameters.Add(new SqlParameter("@payrate", Maintenance_Calendar.payrate));
        parameters.Add(new SqlParameter("@vacation_days", Maintenance_Calendar.vacation_days));

        Int32 rows_affected = SqlHelper.ExecuteProc("[Theme_Park].[Proc_Add_Employee]", parameters.ToArray());
    }
}