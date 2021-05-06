using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SGT_.NET5_FinalProject
{
  class Program
  {
    static void Main(string[] args)
    {
      //establish connection with DB
      string connectionString = @"server=localhost;userid=user;password=FRISTAD1969;database=final project";
      MySqlConnection connection = new MySqlConnection(connectionString);

      try
      {
        connection.Open();
        Console.WriteLine("Connection successfully established");
      }
      catch (Exception)
      {
        Console.WriteLine("Connection could not be established");
        Environment.Exit(1);
      };

      //ask user's name and create a new entry in DB
      Console.Write("Enter your name: ");
      string user_name = Console.ReadLine();

      //check if user already exists in DB
      string queryString =
        @"select * from `final project`.`progress`
        where user_name = @user_name
        order by created_date desc
        limit 1";
      MySqlCommand cmd = new MySqlCommand(queryString, connection);
      cmd.Parameters.AddWithValue("@user_name", user_name);
      MySqlDataReader reader = cmd.ExecuteReader();
      if(reader.Read()) {
        reader.Close();
        Console.WriteLine("User with username [" + user_name + "] already exists in DB");
        Console.WriteLine(@"Your previous progress will be erased.
        Press [Y] to continue
        Press any other key to exit programm");

        string user_response = Console.ReadLine();
        //if user accepted, erase previous progress for this user
        if(user_response.ToLower() == "y") {
          queryString =
          @"delete from `final project`.`progress`
          where user_name = @user_name
          ";
          cmd = new MySqlCommand(queryString, connection);
          cmd.Parameters.AddWithValue("@user_name", user_name);
          cmd.ExecuteNonQuery();
          reader.Close();
        }
        else {
          Environment.Exit(1);
        }
      };


      Console.WriteLine("Welcome, {0}! Soon your journey begins...", user_name);

      //declare variable for current node
      //it equals to 1 as it is a starting point
      int cNode = 1;

      //flag that there are connected nodes
      //it is true, as the first node always has connection
      bool thereAreNodes = true;

      //while there are connected nodes
      //show them to user
      while (thereAreNodes)
      {

        //when we start searching connected nodes, set variable to false
        //that we had a chanse to exit loop, if nothing found
        thereAreNodes = false;

        //creating new entry in DB
        queryString =
        @"INSERT INTO `final project`.`progress`
        (user_name, created_date, status, progress)
        VALUES(@user_name, Now(), 'started', @cNode);";
        cmd = new MySqlCommand(queryString, connection);
        cmd.Parameters.AddWithValue("@user_name", user_name);
        cmd.Parameters.AddWithValue("@cNode", cNode);
        cmd.ExecuteNonQuery();

        //TODO select infomation about current node
        //Console.WriteLine(@"You're staing near {0}");
        string currentNode = @"
        select * from `Final project`.`Locations`
        where id = @cNode ";
        cmd = new MySqlCommand(currentNode, connection);
        cmd.Parameters.AddWithValue("@cNode", cNode);
        reader = cmd.ExecuteReader();
        reader.Read();
        Console.WriteLine("You`re staying near " + reader[1] + "." + reader[3]);
        reader.Close();


        //selecting connected nodes
        queryString = @"
          select c.ending_node, l.name from `final project`.`connections` c
            join `final project`.`locations` l on l.id = c.ending_node
          where starting_node = @cNode";
        cmd = new MySqlCommand(queryString, connection);
        cmd.Parameters.AddWithValue("@cNode", cNode);
        reader = cmd.ExecuteReader();

        //if we found any connected node, printout results
        while (reader.Read())
        {
          Console.WriteLine(String.Format(@"Press [{0}] to go to {1}", reader[0], reader[1]));
          //set flag again to true
          thereAreNodes = true;
        };
        reader.Close();

        string newNode = cNode.ToString();
        bool correct_input = false;

        while (!correct_input && thereAreNodes)
        {
          newNode = Console.ReadLine();
          cmd = new MySqlCommand("select 1 from `final project`.`connections` where starting_node = @cNode and ending_node = @newNode", connection);
          cmd.Parameters.AddWithValue("@cNode", cNode);
          cmd.Parameters.AddWithValue("@newNode", newNode);
          object result = cmd.ExecuteScalar();
          if(result == null) {
            Console.WriteLine("Wrong path chosen. Choose once again!");
          }
          else {
            correct_input = true;
          };
        };

        if (thereAreNodes)
        {
          cmd = new MySqlCommand("UPDATE Progress SET status = 'visited' where progress = @cNode", connection);
          cmd.Parameters.AddWithValue("@cNode", cNode);
          cmd.ExecuteNonQuery();
        }
        else
        {
          cmd = new MySqlCommand("UPDATE Progress SET status = 'finished' where progress = @cNode", connection);
          cmd.Parameters.AddWithValue("@cNode", cNode);
          cmd.ExecuteNonQuery();
        }
        cNode = Int32.Parse(newNode);
      };

      //show user progress
      MySqlCommand cmdFinal = new MySqlCommand(@"
        select progress, status, l.name FROM Progress p
        JOIN Locations l ON l.id = p.progress
        where user_name = @user_name ", connection);
      cmdFinal.Parameters.AddWithValue("@user_name", user_name);
      MySqlDataReader readerFinal = cmdFinal.ExecuteReader();

      Console.WriteLine("progress \t| status \t| location name");
      while (readerFinal.Read())
      {
        uint progress = (uint)readerFinal[0];
        string status = (string)readerFinal[1];
        string name = (string)readerFinal[2];
        Console.WriteLine($"{progress} \t| {status} \t| {name}");
      }
      Console.WriteLine();
      readerFinal.Close();

      //close connection
      connection.Close();

    }
  }
}
