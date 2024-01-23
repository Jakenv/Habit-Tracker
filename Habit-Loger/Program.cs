using Microsoft.Data.Sqlite;

const string connectionString = "Data Source=habits.db";

using var connection = new SqliteConnection(connectionString);
connection.Open();

var command = connection.CreateCommand();
command.CommandText =
    @"CREATE TABLE IF NOT EXISTS drinking_water (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DATE TEXT,
    Quantity INTEGER
)";

command.ExecuteNonQuery();
connection.Close();

Console.Clear();
bool exitApp = false;
do
{
    Console.WriteLine("MAIN MENU\n");
    Console.WriteLine("1. Add a new habit");
    Console.WriteLine("2. View all habits");
    Console.WriteLine("3. Edit a habit");
    Console.WriteLine("4. Delete a habit");
    Console.WriteLine("0. Exit");
    Console.WriteLine("--------------------");

    var readInput = Console.ReadLine();
    
    switch (readInput)
    {
        case "1":
            AddHabit();
            break;
        case "2":
            Console.WriteLine("dupa");
            // ViewHabits();
            break;
        case "3":
            Console.WriteLine("dupa");
            // EditHabit();
            break;
        case "4":
            Console.WriteLine("dupa");
            // DeleteHabit();
            break;
        case "0":
            exitApp = true;
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
    
} while (exitApp != true);

void AddHabit()
{ 
    var data = GetDateInput();
    var quantity = GetQuantityInput();
    connection.Open();
    command.CommandText = @$"INSERT INTO drinking_water (DATE, Quantity) VALUES ('{data}', {quantity})";
    command.ExecuteNonQuery();
    connection.Close();
}

string GetDateInput()
{
    Console.WriteLine("Please enter valid date (dd/mm/yyyy): ");
    var dateInput = Console.ReadLine();
    if (DateTime.TryParse(dateInput, out _))
    {
        Console.WriteLine("Date is valid");
        return dateInput;
    }
    Console.WriteLine("Date is not valid");
    return GetDateInput();
}

int GetQuantityInput()
{
    Console.WriteLine("Please enter quantity: ");
    var quantityInput = Console.ReadLine();
    if (int.TryParse(quantityInput, out _))
    {
        Console.WriteLine("Quantity is valid");
        return int.Parse(quantityInput);
    }
    Console.WriteLine("Quantity is not valid");
    return GetQuantityInput();
}