using Dapper;
using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Computer> GetAll() 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
       
        return connection.Query<Computer>("SELECT * FROM Computers");
    }

    public Computer Save(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);

        return computer;
    }

    public void Update(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET Ram = @Ram, Processor = @Processor WHERE id = @Id", computer);
    }

    public void Delete(int id) 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id = @Id", new {Id = id});
    }

    public Computer GetById(int id) 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
    
        return connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id", new {Id = id}); 
    }

    public bool ExistsByID(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Computers WHERE id = @Id", new {Id = id});
    }
}