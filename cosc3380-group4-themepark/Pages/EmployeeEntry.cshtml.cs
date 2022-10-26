using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;

namespace cosc3380_group4_themepark.Pages;

public class EmployeeEntryModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<EmployeeEntryModel> _logger;

    public EmployeeEntryModel(ILogger<EmployeeEntryModel> logger)
    {
        _logger = logger;
    }

    public static List<Int32> GetValidSupervisorSSNs()
    {
        return null;
    }
                //String sql = "exec " + "[Theme_Park].[Proc_Add_Employee]" +
                    // "@fname, @lname, @ssn, @gender, @address, @phone, @date_joined, @dept_id, @role, @supervisor_ssn, @salaried, @payrate, @vacation_days;";
    public void OnPostSubmit(Employee employee)
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@fname", employee.fname));
        parameters.Add(new SqlParameter("@lname", employee.lname));
        parameters.Add(new SqlParameter("@ssn", employee.ssn));
        parameters.Add(new SqlParameter("@gender", employee.gender));
        parameters.Add(new SqlParameter("@address", employee.address));
        parameters.Add(new SqlParameter("@phone", employee.phone));
        parameters.Add(new SqlParameter("@date_joined", employee.date_joined));
        parameters.Add(new SqlParameter("@dept_id", employee.dept_id));
        parameters.Add(new SqlParameter("@supervisor_ssn", employee.supervisor_ssn));
        parameters.Add(new SqlParameter("@salaried", employee.salaried));
        parameters.Add(new SqlParameter("@payrate", employee.payrate));
        parameters.Add(new SqlParameter("@vacation_days", employee.vacation_days));

        Int32 rows_affected = SqlHelper.ExecNonQueryProc("[Theme_Park].[Proc_Add_Employee]", parameters.ToArray());
    }
}