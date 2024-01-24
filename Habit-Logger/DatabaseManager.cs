using Microsoft.Data.Sqlite;

namespace Habit_Logger;

public class DatabaseManager
{
    private readonly string _connectionString;
    
    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DATE TEXT,
                        Quantity INTEGER
                    )
            """;
        command.ExecuteNonQuery();
    }

    private SqliteDataReader? ExecuteSql(string commandText, params SqliteParameter[] parameters)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
         
        using var command = connection.CreateCommand();
        command.CommandText = commandText;
        
        foreach (var parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }
        
        try
        {
            return command.ExecuteReader();
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"An error occured while executing the SQL command: {ex.Message}");
            return null;
        }
    }

    public void AddHabit()
    {
        var date = InputManager.GetDateInput();
        var quantity = InputManager.GetQuantityInput();
        ExecuteSql(
            "INSERT INTO drinking_water (DATE, Quantity) VALUES (@date, @quantity)",
            new SqliteParameter("@date", date),
            new SqliteParameter("@quantity", quantity)
        );
    }

    public void ViewHabits()
    {
        using var reader = ExecuteSql("SELECT * FROM drinking_water");
        while (reader != null && reader.Read())
        {
            var id = reader.GetInt32(0);
            var date = reader.GetString(1);
            var quantity = reader.GetInt32(2);
            Console.WriteLine($"Id: {id}, Date: {date}, Quantity: {quantity}");
        }
    }

    private bool RecordExits(int id)
    {
        using var reader = ExecuteSql(
            "SELECT * FROM drinking_water WHERE Id = @id",
            new SqliteParameter("@id", id)
        );
        return reader != null && reader.Read();
    }

    public void DeleteHabit()
    {
        Console.WriteLine("What do you want to delete?\n");
        ViewHabits();
        var id = InputManager.GetId();
        if (!RecordExits(id))
            Console.WriteLine("Record don't exist, chose different ID");
        else
        {
            ExecuteSql(
                "DELETE FROM drinking_water WHERE Id = @id",
                new SqliteParameter("@id", id)
            );
        }
    }
    
    public void EditHabit()
    {
        Console.WriteLine("What do you want to edit?\n");
        ViewHabits();
        var id = InputManager.GetId();
        if (!RecordExits(id))
            Console.WriteLine("Record don't exist, chose different ID");
        else
        {
            var date = InputManager.GetDateInput();
            var quantity = InputManager.GetQuantityInput();
            ExecuteSql(
                "UPDATE drinking_water SET DATE = @date, Quantity = @quantity WHERE Id = @id",
                new SqliteParameter("@date", date),
                new SqliteParameter("@quantity", quantity),
                new SqliteParameter("@id", id)
            );
        }
    }
}