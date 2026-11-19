using System;

namespace MyHabitTracker
{
public class Habit
{
public string Name { get; set; } = string.Empty;
public int Streak { get; set; }
public bool IsCompletedToday { get; set; }

public string ToCsvLine()
{
return $"{Name};{Streak};{IsCompletedToday}";
}

public static Habit? FromCsvLine(string line)
{
if (string.IsNullOrWhiteSpace(line))
return null;

string[] parts = line.Split(';');
if (parts.Length != 3)
return null;

if (!int.TryParse(parts[1], out int streak) || !bool.TryParse(parts[2], out bool isCompleted))
return null;

return new Habit
{
Name = parts[0],
Streak = streak,
IsCompletedToday = isCompleted
};
}
}
}
