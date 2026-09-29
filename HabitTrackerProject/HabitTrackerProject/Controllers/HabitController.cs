using HabitTrackerProject.Helpers;
using HabitTrackerProject.Models;
using HabitTrackerProject.UI;
using Spectre.Console;
namespace HabitTrackerProject.Controllers
{
    public class HabitController
    {
        public static void ViewHabitsController()
        {
            try
            {
                List<Habit> viewHabits = DatabaseOperations.GetData();
                if (!viewHabits.Any())
                {
                    AnsiConsole.MarkupLine("[red bold]There are no records in the tracker.[/]");
                }
                else TablePrinter.ViewHabits(viewHabits);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("[red bold]An error occured while trying to view your habits.[/]");
            }
        }

        public static void AddHabitController()
        {
            try
            {
                Habit newHabit = UserInterface.AddHabitUI();
                if (UserInterface.UserVerifyData("Add this habit to the tracker?"))
                {
                    DatabaseOperations.AddHabit(newHabit);
                    UserInterface.SuccessMessage();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("[red bold]An error occured while trying to Add a habit[/]");
            }
        }

        public static void UpdateHabitController()
        {
            try
            {
                List<Habit> updateHabits = DatabaseOperations.GetData();
                if (!updateHabits.Any())
                {
                    AnsiConsole.MarkupLine("[red bold]There are no records in the tracker to update[/]");
                }
                else
                {
                    TablePrinter.ViewHabits(updateHabits);
                    Habit updatedHabit = UserInterface.UpdateHabitUI(updateHabits);
                    if (UserInterface.UserVerifyData("Update this habit with the entered data?"))
                    {
                        DatabaseOperations.UpdateHabit(updatedHabit);
                        UserInterface.SuccessMessage();
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("[red bold]An error occured trying to update a habit.[/]");
            }
        }

        public static void DeleteHabitController()
        {
            try
            {
                List<Habit> deleteHabits = DatabaseOperations.GetData();
                if (!deleteHabits.Any())
                {
                    AnsiConsole.MarkupLine("[red bold]There are no records in the tracker to Delete[/]");
                }
                else
                {
                    TablePrinter.ViewHabits(deleteHabits);
                    int id = UserInterface.DeleteHabitUI(deleteHabits);
                    if (UserInterface.UserVerifyData($"Delete habit with Id [cyan bold]{id}[/] from the tracker?"))
                    {
                        DatabaseOperations.DeleteHabit(id);
                        UserInterface.SuccessMessage();
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("[/]An error occured trying to delete a habit[/]");
            }
        }
    }
}
