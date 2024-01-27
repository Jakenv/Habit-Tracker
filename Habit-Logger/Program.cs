using Habit_Loger.DataBaseManipulation;
using Habit_Loger.InputHandling;

const string connectionString = "Data Source=habits.db";
IDatabaseManager db = new DatabaseManager(connectionString);
IInputManager input = new InputManager();

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
            db.AddHabit();
            break;
        case "2":
            db.ViewHabits();
            input.PressSpaceToContinue();
            break;
        case "3":
            db.EditHabit();
            input.PressSpaceToContinue();
            break;
        case "4":
            db.DeleteHabit();
            input.PressSpaceToContinue();
            break;
        case "0":
            InputManager.ExitApp();
            exitApp = true;
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
    
} while (exitApp != true);
