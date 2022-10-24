using System.Net;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using cosc3380_group4_themepark.Models;

namespace cosc3380_group4_themepark.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }



    public void OnPostSubmit(Ticket ticket)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Credentials.getConnectionString()))
            {
                Console.WriteLine("Passed connection string is {0}", connection.ConnectionString);
                Console.WriteLine("Passed values {0}, {1}, {2}, {3}, {4}", ticket.ticketID*10, ticket.datetime, ticket.ticketClass, ticket.price*2, ticket.reservation);
                connection.Open();
                Console.WriteLine("Creating SQL query...");
                String sql = @"INSERT INTO [Theme_Park].[Ticket] ([Ticket_ID], [Date], [Ticket_Class], [Price], [Reservation]) VALUES (@ticketID, GETDATE(), @ticketClass, @price, NULL );";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@ticketID", ticket.ticketID));
                    command.Parameters.Add(new SqlParameter("@ticketClass", ticket.ticketClass));
                    command.Parameters.Add(new SqlParameter("@price", ticket.price));
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e);
        }
    }
}

