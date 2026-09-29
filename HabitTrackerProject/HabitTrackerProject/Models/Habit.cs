namespace HabitTrackerProject.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public int Quantity { get; set; }
    }
}
