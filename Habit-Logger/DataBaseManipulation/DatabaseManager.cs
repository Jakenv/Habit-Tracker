using Microsoft.Data.Sqlite;

namespace Habit_Loger;

public class DatabaseManager
{
    private readonly SqliteConnection _connection;
    
    public DatabaseManager(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var command = _connection.CreateCommand();
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

    private List<DrinkingWaterRecords> ExecuteSql(string commandText, params SqliteParameter[] parameters)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = commandText;
         
        foreach (var parameter in parameters)
        {
            command.Parameters.Add(parameter);
        }
        
        var records = new List<DrinkingWaterRecords>();
        try
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var date = reader.FieldCount > 1 ? reader.GetString(1) : null;
                var quantity = reader.FieldCount > 2 ? reader.GetInt32(2) : 0;
                records.Add(new DrinkingWaterRecords(id, date, quantity));
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"An error occured while executing the SQL command: {ex.Message}");
        }
        return records;
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

    private List<DrinkingWaterRecords> GetHabits()
    {
        return ExecuteSql("SELECT * FROM drinking_water");
    }
    
    public void ViewHabits()
    {
        var records = GetHabits();
        foreach (var record in records)
        {
            Console.WriteLine($"Id: {record.Id}, Date: {record.Date}, Quantity: {record.Quantity}");
        }
    }
    
    private bool RecordExits(int id)
    {
        var records = ExecuteSql(
            "SELECT Id FROM drinking_water WHERE Id = @id",
            new SqliteParameter("@id", id)
        );
        return records.Count != 0;
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