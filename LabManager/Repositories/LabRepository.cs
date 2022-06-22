using Dapper;
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

    public IEnumerable<Lab> GetAll() 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
       
        return connection.Query<Lab>("SELECT * FROM Labs");
    }

    public Lab Save(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Labs VALUES(@Id, @Number, @Name, @Block)", lab);

        return lab;
    }

    public void Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Labs SET Number = @Number, Name = @Name, Block = @Block WHERE Id = @id", lab);
    }

    public void Delete(int id) 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Labs WHERE id = @Id", new {Id = id});
    }

    public Lab GetById(int id) 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
    
        return connection.QuerySingle<Lab>("SELECT * FROM Labs WHERE id = @Id", new {Id = id});
    }

    public bool ExistsByID(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Labs WHERE id = @Id", new {Id = id});
    }
}