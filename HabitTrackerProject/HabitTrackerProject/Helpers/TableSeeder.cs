using HabitTrackerProject.Enums;
using Microsoft.Data.Sqlite;

namespace HabitTrackerProject.Helpers
{
    internal class TableSeeder
    {
        public static string connectionString = DatabaseOperations.connectionString;

        public static void SeedData()
        {
            Random rnd = new Random();

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "SELECT EXISTS(SELECT 1 FROM Habits);";
            if ((long)checkCommand.ExecuteScalar() == 1)
            {
                return;
            }

            var habitPool = new[]
            {
                new {Name = "Water Drunk", Unit = Unit.Glasses.ToString()},
                new {Name = "Meals", Unit = Unit.Times.ToString()},
                new {Name = "Reading", Unit = Unit.Pages.ToString()},
                new {Name = "Distance Walked", Unit = Unit.Kilometers.ToString()}
            };

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Habits (Date, Name, Unit, Quantity)
                                        VALUES (@Date, @Name, @Unit, @Quantity);";

            var startDate = DateOnly.FromDateTime((DateTime.Now)).AddDays(-100);

            var nameParam = command.Parameters.Add("@Name", SqliteType.Text);
            var unitParam = command.Parameters.Add("@Unit", SqliteType.Text);
            var dateParam = command.Parameters.Add("@Date", SqliteType.Text);
            var quantityParam = command.Parameters.Add("@Quantity", SqliteType.Integer);



            for (int i = 1; i <= 100; i++)
            {
                var randomHabit = habitPool[rnd.Next(0, habitPool.Length)];

                dateParam.Value = startDate.ToString("yyyy-MM-dd");
                nameParam.Value = randomHabit.Name;
                unitParam.Value = randomHabit.Unit;
                quantityParam.Value = rnd.Next(1, 11);

                command.ExecuteNonQuery();
                startDate = startDate.AddDays(1);
            }
            
        }
    }
}
