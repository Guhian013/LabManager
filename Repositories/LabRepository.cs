using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll() 
    {
        var labs = new List<Lab>(); 
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
           var lab = ReaderToLab(reader);
           labs.Add(lab);
        }

        reader.Close();
        connection.Close(); 

        return labs;
    }

    public void Save(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Labs VALUES($id, $number, $name, $block)";
        command.Parameters.AddWithValue("$id", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery();

        connection.Close();
    }

    public void Update(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Labs SET Number = $number, Name = $name, Block = $block WHERE Id = $id";
        command.Parameters.AddWithValue("$id", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery();

        connection.Close();
    }

    public void Delete(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Labs WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();

        connection.Close();
    }

    public Lab GetById(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();

        reader.Read();
        var lab = ReaderToLab(reader);

        reader.Close();
        connection.Close();

        return lab;
    }

    public bool ExistsByID(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM labs WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        // var reader = command.ExecuteReader();
        // reader.Read();
        // var result = reader.GetBoolean(0);

        var result = Convert.ToBoolean(command.ExecuteScalar());

        connection.Close();

        return result;
    }

    private Lab ReaderToLab(SqliteDataReader reader) {
        var lab = new Lab(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetChar(3));
        return lab; 
    }
}