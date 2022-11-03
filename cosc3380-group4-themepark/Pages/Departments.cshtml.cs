using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Microsoft.Data.SqlClient;
using cosc3380_group4_themepark;
using cosc3380_group4_themepark.Models;
namespace cosc3380_group4_themepark.Pages
{
    //[Authorize]
    public class DepartmentsModel : PageModel
    {
        public List<Departments> departmentList { get; set; }

        public void OnGet()
        {
            List<Departments> departments = new List<Departments>();

            String queryString = @"SELECT * FROM [Theme_Park].[Department] ORDER BY (dept_name) ASC;";

            SqlDataReader reader = SqlHelper.ExecuteQueryReader(queryString);

            while (reader.Read())
            {
                Departments department = new Departments();
                department.dept_id = reader.GetInt32(0);
                department.dept_name = reader.GetString(1);
                department.supervisor_ssn = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                departments.Add(department);
            }
            this.departmentList = departments;
        }
    }
}
