using System.Diagnostics;
using System.Collections.Generic;
using cosc3380_group4_themepark;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using cosc3380_group4_themepark.Models;



namespace cosc3380_group4_themepark.Pages;

public class AddEmployeeModel : PageModel
{
    public List<Departments> departments { get; set; }
    public List<Employee> supervisors { get; set;  }

    public IActionResult OnGet()
    {

        SqlDataReader DeptReader = SqlHelper.ExecuteProcReader(
            "[Theme_Park].[proc_departments]");
        this.departments = new List<Departments>();

       while (DeptReader.Read())
        {
            Departments department = new Departments();
            department.dept_id = DeptReader.GetInt32(1);
            department.dept_name = DeptReader.GetString(0);
            this.departments.Add(department);

        }

       SqlDataReader SSNReader = SqlHelper.ExecuteProcReader(
        "[Theme_Park].[proc_superssn]");
        this.supervisors = new List<Employee>();

        while (SSNReader.Read())
       {
           Employee supervisor = new Employee();
           supervisor.ssn = SSNReader.GetDecimal(2);
           supervisor.last_four = SSNReader.GetString(3);
           supervisor.fname = SSNReader.GetString(0);
           supervisor.lname = SSNReader.GetString(1);
           this.supervisors.Add(supervisor);

        }

        return Page();
    }


    public IActionResult OnPostAddEmployee(Employee employee)
    {
        Console.WriteLine(employee.supervisor_ssn);
        if (employee.date_joined is null)
        {
            return Redirect("/Nowhere");
        }

       
        Int32 rows_affected = SqlHelper.ExecuteProcNonQuery(
          "[Theme_Park].[Proc_Add_Employee]",
          new SqlParameter("@fname", employee.fname),
          new SqlParameter("@lname", employee.lname),
          new SqlParameter("@ssn", Decimal.ToInt32(employee.ssn)),
          new SqlParameter("@gender", employee.gender),
          new SqlParameter("@address", employee.address),
          new SqlParameter("@phone", employee.phone),
          new SqlParameter("@date_joined", employee.date_joined.GetValueOrDefault()),
          new SqlParameter("@dept_id", employee.dept_id),
          new SqlParameter("@role", employee.role),
          new SqlParameter("@supervisor_ssn", Decimal.ToInt32(employee.supervisor_ssn.GetValueOrDefault())),
          new SqlParameter("@salaried", employee.salaried),
          new SqlParameter("@payrate", employee.payrate),
          new SqlParameter("@vacation_days", employee.vacation_days),
          new SqlParameter("@db_user", employee.db_user));

        return Redirect("/AddEmployee");
    }
    public class Employee
    {
        [BindProperty]
        public String lname { get; set; }

        [BindProperty]
        public String fname { get; set; }

        [BindProperty]
        public Decimal ssn { get; set; }

        [BindProperty]
        public char gender { get; set; }

        [BindProperty]
        public String address { get; set; }

        [BindProperty]
        public Decimal phone { get; set; }

        [BindProperty]
        public DateTime? date_joined { get; set; }

        [BindProperty]
        public Int32 dept_id { get; set; }

        [BindProperty]
        public String role { get; set; }

        [BindProperty]
        public Decimal? supervisor_ssn { get; set; }

        [BindProperty]
        public bool salaried { get; set; }

        [BindProperty]
        public Decimal? payrate { get; set; }

        [BindProperty]
        public decimal? vacation_days { get; set; }

        [BindProperty]
        public String last_four { get; set; }

        [BindProperty]
        public String db_user { get; set; }
        public Employee()

        { }
    
    }

}

