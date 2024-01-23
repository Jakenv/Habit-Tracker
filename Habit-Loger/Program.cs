using Habit_Loger;

const string connectionString = "Data Source=habits.db";
var inputManager = new InputManager();
var databaseManager = new DatabaseManager(connectionString, inputManager);


var exitApp = false;
do
{
    Console.Clear();
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
            databaseManager.AddHabit();
            break;
        case "2":
            databaseManager.ViewHabits();
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
