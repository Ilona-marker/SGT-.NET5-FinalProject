using System;
using MySql.Data.MySqlClient;

namespace SGT_.NET5_FinalProject
{
  class Program
  {
    static void Main(string[] args)
    {
      //establish connection with DB
      string connectionString = @"server=localhost;userid=user;password=password;database=final project";
      MySqlConnection connection = new MySqlConnection(connectionString);

      try {
        connection.Open();
        Console.WriteLine("Connection successfully established");
      }
      catch(Exception) {
        Console.WriteLine("Connection could not be established");
      };

      //ask user's name and create a new entry in DB
      Console.Write("Enter your name: ");
      string user_name = Console.ReadLine();

      Console.WriteLine("Welcome, {0}! Soon your journey begins...", user_name);

      //creating new entry in DB
      string queryString = "INSERT INTO `final project`.`progress` (user_name, status, progress) VALUES(@user_name, 'started', '1');";
      MySqlCommand cmd = new MySqlCommand(queryString, connection);
      cmd.Parameters.AddWithValue("@user_name", user_name);
      cmd.ExecuteNonQuery();

      //close connection
      connection.Close();

    }
  }
}
