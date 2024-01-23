using Microsoft.Data.Sqlite;

namespace Habit_Logger;

public class DatabaseManager
{
    private readonly SqliteConnection _connection;
    private readonly SqliteCommand _command;

    public DatabaseManager(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _command = _connection.CreateCommand();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        _connection.Open();
        _command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DATE TEXT,
                        Quantity INTEGER
                    )
            """;
        _command.ExecuteNonQuery();
        _connection.Close();
    }

    private SqliteDataReader? ExecuteSql(string commandText)
    {
        try
        {
            _connection.Open();
            _command.CommandText = commandText;
            var reader = _command.ExecuteReader();
            return reader;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"An error occured while executing the SQL command: {ex.Message}");
            return null;
        }
        finally
        {
            _connection.Close();
        }
    }

    public void AddHabit()
    {
        var date = InputManager.GetDateInput();
        var quantity = InputManager.GetQuantityInput();
        using var reader = ExecuteSql($"INSERT INTO drinking_water (DATE, Quantity) VALUES ('{date}', {quantity})");
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

    private int RecordCount()
    {
        int count;
        using var command = new SqliteCommand("SELECT COUNT(*) FROM drinking_water", _connection);
        {  
            _connection.Open();
            count = Convert.ToInt32(command.ExecuteScalar());
            _connection.Close();
        }
        return count;
    }

    public void DeleteHabit()
    {
        Console.WriteLine("What do you want to delete?\n");
        ViewHabits();
        var id = InputManager.GetId();
        if (id > RecordCount())
            Console.WriteLine("Record don't exist, chose different ID");
        else
        {
            using var deletion = new SqliteCommand($"DELETE FROM drinking_water WHERE Id = {id}", _connection);
            _connection.Open();
            deletion.ExecuteNonQuery();
            _connection.Close();
        }
    }
}