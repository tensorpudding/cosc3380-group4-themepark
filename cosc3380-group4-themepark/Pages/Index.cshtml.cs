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
            using (SqlConnection connection = new SqlConnection(Credentials.buildConnectionString()))
            {
                Console.WriteLine("Passed values {0}, {1}, {2}, {3}, {4}", ticket.ticketID, ticket.datetime, ticket.ticketClass, ticket.price, ticket.reservation);
                connection.Open();
                Console.WriteLine("Creating SQL query...");
                String sql = @"INSERT INTO [Theme_Park].[Ticket] VALUES ('@ticketID', GETDATE(), '@ticketClass', '@price', NULL );";

                SqlParameter paramID = new SqlParameter("@ticketID", SqlDbType.Int);
                paramID.Value = ticket.ticketID;
                //SqlParameter paramDatetime = new SqlParameter("@datetime", ticket.datetime);
                SqlParameter paramTicketClass = new SqlParameter("@ticketClass", ticket.ticketClass);
                SqlParameter paramPrice = new SqlParameter("@price", ticket.price);
                //SqlParameter paramReservation = new SqlParameter("@reservation", null);

                SqlParameter[] parameters = new SqlParameter[3] { paramID, paramTicketClass, paramPrice };
                //SqlParameter[] parameters = new SqlParameter[5] { paramID, paramDatetime, paramTicketClass, paramPrice, paramReservation };
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddRange(parameters);
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

