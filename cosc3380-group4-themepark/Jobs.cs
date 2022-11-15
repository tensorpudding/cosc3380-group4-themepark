using Microsoft.AspNetCore.Mvc;

namespace cosc3380_group4_themepark
{
    public class Jobs
    {
        [BindProperty]
        public String Maint_Description { get; set; }

        [BindProperty]
        public String Attraction { get; set; }

        [BindProperty]
        public String Vendor { get; set; }

        [BindProperty]
        public String Priority { get; set; }


    }
}
