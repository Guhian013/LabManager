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

        var computers = connection.Query<Computer>("SELECT * FROM Computers"); 
       
        return computers;
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

        connection.Execute("UPDATE Computers SET Ram = @ram, Processor = @processor WHERE id = @Id", computer);
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

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id = @Id", new {Id = id}); 
    
        return computer;
    }

    public bool ExistsByID(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Computers WHERE id = @Id", new {Id = id});

        return result;
    }

    private Computer ReaderToComputer(SqliteDataReader reader) {
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        return computer; 
    }
}