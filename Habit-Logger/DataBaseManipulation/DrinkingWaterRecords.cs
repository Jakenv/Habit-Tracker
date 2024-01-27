namespace Habit_Loger.DataBaseManipulation;

public class DrinkingWaterRecords(int id, string? date, int quantity)
{
    public int Id { get; } = id;
    public string? Date { get; } = date;
    public int Quantity { get; } = quantity;
}