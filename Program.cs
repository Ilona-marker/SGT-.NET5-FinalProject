﻿using System;
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
        string queryString =
        @"INSERT INTO `final project`.`progress`
        (user_name, created_date, status, progress)
        VALUES(@user_name, Now(), 'started', @cNode);";
        MySqlCommand cmd = new MySqlCommand(queryString, connection);
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
        MySqlDataReader reader = cmd.ExecuteReader();
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

        //creating new node integer variable
        int newNode = 0;

        //check if user puts integer
        if (thereAreNodes && !Int32.TryParse(Console.ReadLine(), out newNode))
        {
          Console.WriteLine("Choose once again!");
        }
        else if (thereAreNodes)
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
        cNode = newNode;
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
