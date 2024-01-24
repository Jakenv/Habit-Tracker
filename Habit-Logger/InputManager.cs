namespace Habit_Loger;

public abstract class InputManager
{
    public static int GetQuantityInput()
    {
        while (true)
        {
            Console.WriteLine("Please enter quantity: ");
            var quantityInput = Console.ReadLine();
            if (int.TryParse(quantityInput, out var quantity))
            {
                Console.WriteLine("Quantity is valid");
                return quantity;
            }
            Console.WriteLine("Quantity is not valid");
        }
    }

    public static string GetDateInput()
    {
        while (true)
        {
            Console.WriteLine("Please enter valid date (dd/mm/yyyy): ");
            var dateInput = Console.ReadLine();
            if (DateTime.TryParse(dateInput, out _))
            {
                Console.WriteLine("Date is valid");
                return dateInput;
            }
            Console.WriteLine("Date is not valid");
        }
    }

    public static int GetId()
    {
        while (true)
        {
            Console.WriteLine("Please enter ID: ");
            var idInput = Console.ReadLine();
            if (int.TryParse(idInput, out var id))
            {
                Console.WriteLine("ID is valid");
                return id;
            }
            Console.WriteLine("ID is not valid");
        }
    }

    public static void PressSpaceToContinue()
    {
        Console.WriteLine("Press space to continue");
        Console.ReadKey();
    }

    public static void ExitApp()
    {
        Console.Clear();
        Console.WriteLine("Closing App...");
    }
}