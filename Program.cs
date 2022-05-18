﻿using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;

var databaseSetup = new DatabaseSetup();
var computerRepository = new ComputerRepository();

// Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List: "); 
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
            
        }
    } 
    
    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];
        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery();

        connection.Close();
    }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List: ");
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader.GetInt32(0)}, {reader.GetInt32(1)}, {reader.GetString(2)}, {reader.GetChar(3)}"
            );
            
        }

        connection.Close();
    } 
    
    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];
        
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Lab VALUES($id, $number, $name, $block)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();

        connection.Close();
    }   
}
