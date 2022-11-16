using cosc3380_group4_themepark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;
using cosc3380_group4_themepark;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;


namespace cosc3380_group4_themepark.Pages
{
    public class EmployeeScheduleModel : PageModel
    {
        private readonly ILogger<EmployeeScheduleModel> _logger;

        public List<Employee> employees { get; set; }
        public List<EmployeeSchedule> schedules { get; set; }

        public EmployeeScheduleModel(ILogger<EmployeeScheduleModel> logger)
        {
            _logger = logger;
        }


        public IActionResult OnGet()
        {
            SqlDataReader SchedReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[Proc_See_Emp_Schedule]");
            this.schedules = new List<EmployeeSchedule>();

            while (SchedReader.Read())
            {
                EmployeeSchedule schedule = new EmployeeSchedule();
                /*schedule.ssn = SchedReader.GetDecimal(0);*/
                schedule.last_four = SchedReader.GetString(0);
                schedule.fname = SchedReader.GetString(1);
                schedule.lname = SchedReader.GetString(2);
                schedule.shift_start = SchedReader.GetDateTime(3);
                schedule.shift_end = SchedReader.GetDateTime(4);
                this.schedules.Add(schedule);

            }

            SqlDataReader SsnReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[proc_superssn]");
            this.employees = new List<Employee>();

            while (SsnReader.Read())
            {
                Employee employee = new Employee();
                employee.ssn = SsnReader.GetDecimal(2);
                employee.last_four = SsnReader.GetString(3);
                employee.fname = SsnReader.GetString(0);
                employee.lname = SsnReader.GetString(1);
                this.employees.Add(employee);

            }
            return Page();
        }

        public IActionResult OnPostCreateSchedule(EmployeeSchedule EnterSchedule)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@employee_SSN", EnterSchedule.employee_SSN));
            parameters.Add(new SqlParameter("@shift_start", EnterSchedule.shift_start));
            parameters.Add(new SqlParameter("@shift_end", EnterSchedule.shift_end));

            Int32 rows_affected = SqlHelper.ExecuteProcNonQuery("[Theme_Park].[Proc_Emp_Schedule]", parameters.ToArray());
            return Redirect("/EmployeeSchedule");

        }
    }
}
