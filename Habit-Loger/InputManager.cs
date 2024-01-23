namespace Habit_Loger;

public class InputManager
{
    public int GetQuantityInput()
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

    public string GetDateInput()
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

    public void PressSpaceToContinue()
    {
        Console.WriteLine("Press space to continue");
        Console.ReadKey();
    }
}