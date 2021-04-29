﻿using System;
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

      try
      {
        connection.Open();
        Console.WriteLine("Connection successfully established");
      }
      catch (Exception)
      {
        Console.WriteLine("Connection could not be established");
      };

      //ask user's name and create a new entry in DB
      Console.Write("Enter your name: ");
      string user_name = Console.ReadLine();

      Console.WriteLine("Welcome, {0}! Soon your journey begins...", user_name);

      //creating new entry in DB
      /*
      string queryString = "INSERT INTO `final project`.`progress` (user_name, status, progress) VALUES(@user_name, 'started', '1');";
      MySqlCommand cmd = new MySqlCommand(queryString, connection);
      cmd.Parameters.AddWithValue("@user_name", user_name);
      cmd.ExecuteNonQuery();
      */

      //declare variable for current node
      //it equals to 1 as it is a starting point
      int cNode = 1;

      //select nodes from DB which connected to current node (cNode)
      //and put it into the loop

      //flag that there are connected nodes
      //it is true, as the first node always has connection
      bool thereAreNodes = true;

      //while there are connected nodes
      //show them to user
      while(thereAreNodes) {

        //ugly workaround
        //when we start searching connected nodes, set variable to false
        //that we had a chanse to exit loop, if nothing found
        thereAreNodes = false;

        //TODO select infomation about current node
        //Console.WriteLine(@"You're staing near {0}");


        //selecting connected nodes
        string queryString = @"
          select c.ending_node, l.name from `final project`.`connections` c
            join `final project`.`locations` l on l.id = c.ending_node
          where starting_node = @cNode";
        MySqlCommand cmd = new MySqlCommand(queryString, connection);
        cmd.Parameters.AddWithValue("@cNode", cNode);
        MySqlDataReader reader = cmd.ExecuteReader();

        //if nothing was selected
        //close connection and exit program
        /*
        if ()) {
          connection.Close();
          Console.WriteLine("Nothing to show");
          Environment.Exit(1);
        };
        */

        //if we found any connected node, printout results
        while (reader.Read())
        {
          Console.WriteLine(String.Format(@"Press [{0}] to go to {1}", reader[0], reader[1]));
          //set flag again to true
          thereAreNodes = true;
        };
        reader.Close();

        //check if user puts integer
        if (thereAreNodes && !Int32.TryParse(Console.ReadLine(), out cNode))
        {
          Console.WriteLine("Choose once again!");
        };

      };

      //close connection
      connection.Close();

    }
  }
}