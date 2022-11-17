using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark.Models;
using cosc3380_group4_themepark;
using Microsoft.IdentityModel.Tokens;

namespace cosc3380_group4_themepark.Pages;

public class EmployeeModel : PageModel


{
    public List<Employee> ?employees { get; set; }
    public void OnGet()
    {
        // code to call ExecuteProcReader()
        SqlDataReader reader = SqlHelper.ExecuteProcReader(
           "[Theme_Park].[proc_All_Employee]");
        this.employees = new List<Employee>();

        this.employees = new List<Employee>();
        while (reader.Read())
        {
            Employee employee = new Employee();
            employee.fname = reader.GetString(0);
            employee.lname = reader.GetString(1);
            employee.ssn = reader.GetDecimal(2);
            employee.gender = reader.GetString(3).ToCharArray()[0];
            employee.address = reader.GetString(4);
            employee.phone = reader.GetDecimal(5);
            employee.date_joined = reader.GetDateTime(6);
            employee.dept_id = reader.GetInt32(7);
            employee.role = reader.GetString(8);
            employee.supervisor_ssn = reader.GetDecimal(9);
            employee.salaried = reader.GetBoolean(10);
            employee.payrate = reader.GetDecimal(11);
            employee.vacation_days = reader.GetDecimal(12);
            /* if (reader.IsDBNull(6))
              {
                  employee.date_joined = "";
              }
              else
              {
                  employee.date_joined = reader.GetDateTime(6);
              }/*


              employee.dept_id = reader.GetInt32(7);
              employee.role = reader.GetString(8);

             /* if (reader.IsDBNull(9))
              {
                  employee.supervisor_ssn = "";
              }
              else
              {
                  employee.supervisor_ssn = reader.GetDecimal(9);
              }

              employee.salaried = reader.GetBoolean(10);

              if (reader.IsDBNull(11))
              {
                  employee.payrate = "";
              }
              else
              {
                  employee.payrate = reader.GetDecimal(11);
              }


              if (reader.IsDBNull(12))
              {
                  employee.vacation_days = "";
              }
              else
              {
                  employee.vacation_days = reader.GetDecimal(12);
              }

              if (reader.IsDBNull(13))
              {
                  employee.db_user = "";
              }
              else
              { 
                  employee.db_user = reader.GetString(13);
              }*/




            this.employees.Add(employee);
           
        }
    }
}
