using Habit_Loger.DataBaseManipulation;
using Habit_Loger.InputHandling;
using JetBrains.Annotations;
using Moq;

namespace TestHabit_Logger;

[TestSubject(typeof(InputManager))]
public class HabitLoggerTests
{

    [Fact]
    public void AddHabit_Test()
    {
        var mockInputManager = new Mock<IInputManager>();
        var mockDatabaseManager = new Mock<IDatabaseManager>();
        mockInputManager.Setup(m => m.GetDateInput()).Returns("01/01/2021");
        mockInputManager.Setup(m => m.GetQuantityInput()).Returns(1);
        mockDatabaseManager.Setup(m => m.AddHabit());
        mockDatabaseManager.Object.AddHabit();
        mockDatabaseManager.Verify(m => m.AddHabit(), Times.Once);
    }
    
    [Fact]
    public void ViewHabits_Test()
    {
        var mockDatabaseManager = new Mock<IDatabaseManager>();
        mockDatabaseManager.Setup(m => m.ViewHabits());
        mockDatabaseManager.Object.ViewHabits();
        mockDatabaseManager.Verify(m => m.ViewHabits(), Times.Once);
    }
    
    [Fact]
    public void DeleteHabit_Test()
    {
        var mockDatabaseManager = new Mock<IDatabaseManager>();
        mockDatabaseManager.Setup(m => m.DeleteHabit());
        mockDatabaseManager.Object.DeleteHabit();
        mockDatabaseManager.Verify(m => m.DeleteHabit(), Times.Once);
    }
    
    [Fact]
    public void EditHabit_Test()
    {
        var mockDatabaseManager = new Mock<IDatabaseManager>();
        mockDatabaseManager.Setup(m => m.EditHabit());
        mockDatabaseManager.Object.EditHabit();
        mockDatabaseManager.Verify(m => m.EditHabit(), Times.Once);
    }
    
    [Fact]
    public void GetQuantityInput_Test()
    {
        var mockInputManager = new Mock<IInputManager>();
        mockInputManager.Setup(m => m.GetQuantityInput()).Returns(1);
        mockInputManager.Object.GetQuantityInput();
        mockInputManager.Verify(m => m.GetQuantityInput(), Times.Once);
    }
    
    [Fact]
    public void GetDateInput_Test()
    {
        var mockInputManager = new Mock<IInputManager>();
        mockInputManager.Setup(m => m.GetDateInput()).Returns("01/01/2021");
        mockInputManager.Object.GetDateInput();
        mockInputManager.Verify(m => m.GetDateInput(), Times.Once);
    }
    
    [Fact]
    public void GetId_Test()
    {
        var mockInputManager = new Mock<IInputManager>();
        mockInputManager.Setup(m => m.GetId()).Returns(1);
        mockInputManager.Object.GetId();
        mockInputManager.Verify(m => m.GetId(), Times.Once);
    }
    
    [Fact]
    public void PressSpaceToContinue_Test()
    {
        var mockInputManager = new Mock<IInputManager>();
        mockInputManager.Setup(m => m.PressSpaceToContinue());
        mockInputManager.Object.PressSpaceToContinue();
        mockInputManager.Verify(m => m.PressSpaceToContinue(), Times.Once);
    }
    
    [Fact]
    public void RecordExist_Test()
    {
        var mockDatabaseManager = new Mock<IDatabaseManager>();
        mockDatabaseManager.Setup(m => m.RecordExist(1)).Returns(true);
        mockDatabaseManager.Object.RecordExist(1);
        mockDatabaseManager.Verify(m => m.RecordExist(1), Times.Once);
    }
}