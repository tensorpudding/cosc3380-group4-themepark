using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace cosc3380_group4_themepark.Models
{
    public class Employee
{

    [BindProperty]
    public String lname { get; set; }

    [BindProperty]
    public String fname { get; set; }

    [BindProperty]
    public Int32 ssn { get; set; }

    [BindProperty]
    public char gender { get; set; }

    [BindProperty]
    public String address { get; set; }

    [BindProperty]
    public Int32 phone { get; set; }

    [BindProperty]
    public DateTime date_joined { get; set; }

    [BindProperty]
    public Int32 dept_id { get; set; }

    [BindProperty]
    public String role { get; set; }

    [BindProperty]
    public Int32 supervisor_ssn { get; set; }

    [BindProperty]
    public bool salaried { get; set; }

    [BindProperty]
    public decimal payrate { get; set; }

    [BindProperty]
    public decimal vacation_days { get; set; }

    public Employee()
    {
    }
}
}