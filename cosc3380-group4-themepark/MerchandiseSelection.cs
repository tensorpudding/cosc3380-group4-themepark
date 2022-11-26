using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace cosc3380_group4_themepark.Models;

/* MerchandiseSelection.cs
 *
 * Class which encapsulates the Merchandise selection in the Merchandise report
 */
public class MerchandiseSelection
{
    [BindProperty]
    public DateTime starttime { get; set; }

    [BindProperty]
    public DateTime endtime { get; set; }

    [BindProperty]
    public String type { get; set; }

    public MerchandiseSelection()
    {
    }
}

