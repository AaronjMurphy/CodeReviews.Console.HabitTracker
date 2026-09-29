using HabitTrackerProject.Models;
using Spectre.Console;

namespace HabitTrackerProject.UI
{
    public class TablePrinter
    {
        public static void ViewHabits(List<Habit> habits)
        {
            Table habitTable = new Table()
                .Title("[yellow bold]Habit Tracker[/]")
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Gray)
                .ShowRowSeparators();

            habitTable.AddColumn("[cyan bold]Id[/]");
            habitTable.AddColumn("[cyan bold]Date[/]");
            habitTable.AddColumn("[cyan bold]Name[/]");
            habitTable.AddColumn("[cyan bold]Unit[/]");
            habitTable.AddColumn("[cyan bold]Quantity[/]");

            foreach (Habit habit in habits)
            {
                habitTable.AddRow(
                    habit.Id.ToString(),
                    habit.Date.ToString("yyyy-MM-dd"),
                    habit.Name,
                    habit.Unit,
                    habit.Quantity.ToString());
            }
            AnsiConsole.Write(habitTable);
        }

    }
}
