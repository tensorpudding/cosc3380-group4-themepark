using Microsoft.AspNetCore.Mvc;

namespace cosc3380_group4_themepark
{
    public class EmployeeSchedule
    {
        [BindProperty]
        public DateTime shift_start { get; set; }

        [BindProperty]
        public DateTime shift_end { get; set; }

        [BindProperty]
        public Int32 employee_SSN { get; set; }

        [BindProperty]
        public Decimal ssn { get; set; }

        [BindProperty]
        public String last_four { get; set; }

        [BindProperty]
        public String lname { get; set; }

        [BindProperty]
        public String fname { get; set; }






    }
}
