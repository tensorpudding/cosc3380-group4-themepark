using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace cosc3380_group4_themepark.Pages
{
    public enum InternalPages
    {
        Sales,
        Merchandise,
        Tickets,
        Employees,
        Maintenance
    }
    public class InternalModel : PageModel
    {
        public InternalPages currPage = InternalPages.Sales;

        public MyViewModels viewModels = new MyViewModels();
        public int year = 2022;
        public string role { get; set; }
        public void OnGet(int? _year = null, string? _currPage = null)
        {

            //Find Role
            var user = HttpContext.User;
            foreach (var claim in user.Claims)
            {
                if (claim.Type == "Role")
                {
                    this.role = claim.Value;
                }
            }

            Console.WriteLine("the year is " + _year);
            year = _year.GetValueOrDefault(year);
            switch (_currPage)
            {
                case "Sales":
                    currPage = InternalPages.Sales;
                    break;
                case "Merchandise":
                    //currPage = InternalPages.Merchandise;
                    //break;
                case "Tickets":
                    //currPage = InternalPages.Tickets;
                    //break;
                case "Employees":
                case "Maintenance":
                default:
                    currPage = InternalPages.Sales;
                    break;
            }

            switch (currPage)
            {
                case InternalPages.Sales:
                    viewModels.sales.OnGet(year);
                    break;
                case InternalPages.Merchandise:
                    //break;
                case InternalPages.Tickets:
                    //break;
                default:
                    viewModels.sales.OnGet(year);
                    break;
            }

        }

        public IActionResult OnPostSelectInternalPage()
        {

            return Page();
        }

    }
}
