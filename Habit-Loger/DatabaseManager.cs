using Microsoft.Data.Sqlite;

namespace Habit_Loger;

public class DatabaseManager
{
    private readonly SqliteConnection _connection;
    private readonly SqliteCommand _command;
    private readonly InputManager _inputManager;
    
    public DatabaseManager(string connectionString, InputManager inputManager)
    {
        this._inputManager = inputManager;
        _connection = new SqliteConnection(connectionString);
        _command = _connection.CreateCommand();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        _connection.Open();
        _command.CommandText =
            @"CREATE TABLE IF NOT EXISTS drinking_water (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            DATE TEXT,
            Quantity INTEGER
        )";
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
        var date = _inputManager.GetDateInput();
        var quantity = _inputManager.GetQuantityInput();
        var reader = ExecuteSql($@"INSERT INTO drinking_water (DATE, Quantity) VALUES ('{date}', {quantity})");
        if (reader != null)
            _inputManager.PressSpaceToContinue();
    }

    public void ViewHabits()
    {
        var reader = ExecuteSql("SELECT * FROM drinking_water");
        if (reader != null)
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var date = reader.GetString(1);
                var quantity = reader.GetInt32(2);
                Console.WriteLine($"Id: {id}, Date: {date}, Quantity: {quantity}");
            }
            _inputManager.PressSpaceToContinue();
        }
    }
}