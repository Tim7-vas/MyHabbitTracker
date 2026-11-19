using System;
using System.Collections.Generic;
using System.IO;

namespace MyHabitTracker
{
public class HabitManager
{
private readonly string _filePath;
public List<Habit> Habits { get; private set; } = new List<Habit>();

public HabitManager(string filePath = "habits.txt")
{
_filePath = filePath;
}

public void Load()
{
Habits.Clear();
if (!File.Exists(_filePath))
{
Console.WriteLine("Файл habits.txt не найден. Начинаем с пустого списка.");
return;
}

try
{
using (StreamReader reader = new StreamReader(_filePath))
{
string? line;
int lineNumber = 0;
while ((line = reader.ReadLine()) != null)
{
lineNumber++;
Habit? habit = Habit.FromCsvLine(line);
if (habit != null)
{
Habits.Add(habit);
}
else
{
Console.WriteLine($"Предупреждение: пропущена некорректная строка #{lineNumber}.");
}
}
}
Console.WriteLine($"Загружено привычек: {Habits.Count}");
}
catch (Exception ex)
{
Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
}
}

public void Save()
{
try
{
using (StreamWriter writer = new StreamWriter(_filePath, append: false))
{
foreach (var habit in Habits)
{
writer.WriteLine(habit.ToCsvLine());
}
}
Console.WriteLine("Привычки успешно сохранены в habits.txt.");
}
catch (Exception ex)
{
Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
}
}

public void AddHabit(string name)
{
string cleanName = name.Replace(";", ",");
Habits.Add(new Habit
{
Name = cleanName,
Streak = 0,
IsCompletedToday = false
});
}

public bool CompleteHabit(int index)
{
if (index < 0 || index >= Habits.Count)
return false;

if (!Habits[index].IsCompletedToday)
{
Habits[index].IsCompletedToday = true;
Habits[index].Streak++;
}
return true;
}
}
}
