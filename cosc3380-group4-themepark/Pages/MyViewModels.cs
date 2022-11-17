using Microsoft.Identity.Client;

namespace cosc3380_group4_themepark.Pages
{
    public class MyViewModels
    {
        //Client Side
        public LoginModel? login;
        public TicketsModel ticketsForm;

        //Server Side
        public SalesModel? sales;
        public TicketReportModel ticketReport;

        public MyViewModels()
        {
            login = new LoginModel();
            ticketsForm = new TicketsModel();

            sales = new SalesModel();
            ticketReport = new TicketReportModel();
        }
    }
}
