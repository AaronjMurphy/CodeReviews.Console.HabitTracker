using Spectre.Console;
using HabitTrackerProject.Models;
using HabitTrackerProject.Enums;

namespace HabitTrackerProject.UI
{
    public class UserInterface
    {
        public static State GetChoice()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<State>()
                .Title("[yellow bold]What would you like to do? [/]")
                .AddChoices(State.ViewHabits, State.AddHabit, State.UpdateHabit, State.DeleteHabit, State.Exit));
        }

        public static bool UserVerifyData(string message)
        {
            return AnsiConsole.Prompt(new ConfirmationPrompt($"[yellow bold]{message}[/]"));
        }

        public static void SuccessMessage()
        {
            AnsiConsole.MarkupLine("[green]Success![/]");
        }

        public static void ReturnToMenu()
        {
            AnsiConsole.MarkupLine("[yellow bold]Press enter to continue.[/]");
            Console.ReadLine();
        }
        public static Habit AddHabitUI()
        {
            DateOnly date = PromptDate();
            string name = PromptName();
            string unit = PromptUnit();
            int quantity = PromptQuantity();

            return new Habit
            {
                Date = date,
                Name = name,
                Unit = unit,
                Quantity = quantity
            };
        }

        public static int DeleteHabitUI(List<Habit> habits)
        {
            int id = PromptId("delete", habits);
            return id;
        }

        public static Habit UpdateHabitUI(List<Habit> habits)
        {
            int id = PromptId("update", habits);
            DateOnly date = PromptDate();
            string name = PromptName();
            string unit = PromptUnit();
            int quantity = PromptQuantity();


            return new Habit
            {
                Id = id,
                Date = date,
                Name = name,
                Unit = unit,
                Quantity = quantity
            };
        }

        internal static int PromptId(string operationType, List<Habit> habits)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>($"[yellow bold]Enter the Id of the habit you would like to {operationType}:[/] ")
                .Validate(enteredId =>
                {
                    if (enteredId <= 0)
                    {
                        return ValidationResult.Error("[red bold]Id must be greater than 0! Try again.[/]");
                    }

                    bool idExists = habits.Any(h => h.Id == enteredId);

                    return idExists
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red bold]Id [cyan bold]{enteredId}[/] does not exist in the tracker! Try again.[/]");
                }
                )
                .AllowEmpty()
                .ValidationErrorMessage("[red bold]Id cannot be null[/]"));
        }

        internal static string PromptName()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow bold]Enter the name of the habit[/]")
                .AllowEmpty()
                .Validate(enteredHabit =>
                {
                    if (!string.IsNullOrWhiteSpace(enteredHabit))
                    {
                        return ValidationResult.Success();
                    }
                    else return ValidationResult.Error("[red bold]Habit name cannot be empty![/]");

                }));
        }

        internal static DateOnly PromptDate()
        {
            string dateString = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow bold]Enter the date:[/] ")
                .Validate(date =>
                {
                    if (DateOnly.TryParseExact(date, "yyyy-MM-dd", out _))
                    {
                        return ValidationResult.Success();
                    }
                    return ValidationResult.Error("[red bold]Invalid format! Please use [cyan bold]yyyy-MM-dd[/][/]");
                })
                .DefaultValue(DateTime.Today.ToString("yyyy-MM-dd")
                ));

            return DateOnly.ParseExact(dateString, "yyyy-MM-dd");
        }

        internal static string PromptUnit()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[yellow bold]Select a unit of measurement:[/] ")
                .AddChoices(Unit.Glasses.ToString(), Unit.Times.ToString(), Unit.Pages.ToString(), Unit.Kilometers.ToString()));
        }

        internal static int PromptQuantity()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>("[yellow bold]Enter the quantity:[/] ")
                .DefaultValue(0)
                .Validate(qty => qty >= 0
                ? ValidationResult.Success()
                : ValidationResult.Error("[red bold]Quantities cannot be negative! Try again.[/]")
                ));
        }

        public static void Welcome()
        {
            AnsiConsole.MarkupLine("[white bold underline]Welcome To The Habit Tracker[/]\n\n\n");
        }
    }
}
