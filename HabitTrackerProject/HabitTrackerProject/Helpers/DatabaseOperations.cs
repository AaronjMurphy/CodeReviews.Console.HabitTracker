using Microsoft.Data.Sqlite;
using HabitTrackerProject.Models;

namespace HabitTrackerProject.Helpers
{
    public class DatabaseOperations
    {
        public static string connectionString = @"Data Source=habit-tracker.db";
        public static void CreateDatabase()
        {

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Habits (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date DATE,
                Name VARCHAR NOT NULL,
                Unit VARCHAR NOT NULL,
                Quantity INTEGER
                );";

            tableCmd.ExecuteNonQuery();

            TableSeeder.SeedData();
        }

        public static List<Habit> GetData()
        {
            List<Habit> habits = new List<Habit>();
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Date, Name, Unit, Quantity FROM Habits;";

            using var reader  = command.ExecuteReader();

            while (reader.Read())
            {
                habits.Add(
                    new Habit
                    {
                        Id = reader.GetInt32(0),
                        Date = DateOnly.ParseExact(reader.GetString(1), "yyyy-MM-dd"),
                        Name = reader.GetString(2),
                        Unit = reader.GetString(3),
                        Quantity = reader.GetInt32(4)
                    });
            }

            return habits;
        }

        public static void AddHabit(Habit habit)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Habits (Date, Name, Unit, Quantity)
                                    VALUES (@Date, @Name, @Unit, @Quantity);";
            command.Parameters.AddWithValue("@Date", habit.Date.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Name", habit.Name);
            command.Parameters.AddWithValue("@Unit", habit.Unit);
            command.Parameters.AddWithValue("@Quantity", habit.Quantity);

            command.ExecuteNonQuery();
        }

        public static void DeleteHabit(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Habits WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public static void UpdateHabit(Habit habit)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Habits
                                    SET Date = @Date, Name = @Name, Unit = @Unit, Quantity = @Quantity
                                    WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Date", habit.Date.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Name", habit.Name);
            command.Parameters.AddWithValue("@Unit", habit.Unit);
            command.Parameters.AddWithValue("@Quantity", habit.Quantity);
            command.Parameters.AddWithValue("@Id", habit.Id);

            command.ExecuteNonQuery();
        }

    }
}
