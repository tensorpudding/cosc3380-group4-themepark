using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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