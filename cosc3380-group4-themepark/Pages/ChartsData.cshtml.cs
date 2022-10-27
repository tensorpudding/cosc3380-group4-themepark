using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cosc3380_group4_themepark.Pages
{
    public class ChartsDataModel : PageModel
    {
        public static int num;
        public void OnGet()
        {
            num = 5;

        }


    }
}
