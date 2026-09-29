using HabitTrackerProject.Helpers;
using HabitTrackerProject.UI;
using HabitTrackerProject.Enums;
using Spectre.Console;
using HabitTrackerProject.Controllers;


namespace HabitTrackerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DatabaseOperations.CreateDatabase();
                bool exit = false;

                while (!exit)
                {
                    AnsiConsole.Clear();
                    UserInterface.Welcome();
                    State choice = UserInterface.GetChoice();

                    switch (choice)
                    {
                        case State.ViewHabits:
                            HabitController.ViewHabitsController();
                            UserInterface.ReturnToMenu();
                            break;
                        case State.AddHabit:
                            HabitController.AddHabitController();
                            UserInterface.ReturnToMenu();
                            break;
                        case State.UpdateHabit:
                            HabitController.UpdateHabitController();
                            UserInterface.ReturnToMenu();
                            break;
                        case State.DeleteHabit:
                            HabitController.DeleteHabitController();
                            UserInterface.ReturnToMenu();
                            break;
                        case State.Exit:
                            exit = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("[red bold]A fatal error occured. The application must close.[/]");
            }
            
    
        }

    }
}
