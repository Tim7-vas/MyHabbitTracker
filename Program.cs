using System;
using System.Text;

namespace MyHabitTracker
{
class Program
{
static void Main()
{
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

HabitManager manager = new HabitManager("habits.txt");
manager.Load();

bool isRunning = true;
while (isRunning)
{
Console.WriteLine("\n=== ТРЕКЕР ПРИВЫЧЕК ===");
Console.WriteLine("1. Показать все привычки");
Console.WriteLine("2. Добавить новую привычку");
Console.WriteLine("3. Отметить привычку выполненной сегодня");
Console.WriteLine("0. Сохранить и выйти");
Console.Write("Выберите действие: ");

string? choice = Console.ReadLine();
switch (choice)
{
case "1":
ShowHabits(manager);
break;

case "2":
Console.Write("Введите название привычки: ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
manager.AddHabit(name.Trim());
Console.WriteLine("Привычка добавлена.");
}
else
{
Console.WriteLine("Название не может быть пустым.");
}
break;

case "3":
ShowHabits(manager);
if (manager.Habits.Count > 0)
{
Console.Write("Введите номер выполненной привычки: ");
if (int.TryParse(Console.ReadLine(), out int num) && manager.CompleteHabit(num - 1))
{
Console.WriteLine("Отлично! Привычка отмечена, серия увеличена.");
}
else
{
Console.WriteLine("Неверный номер привычки.");
}
}
break;

case "0":
manager.Save();
isRunning = false;
break;

default:
Console.WriteLine("Неизвестная команда. Попробуйте снова.");
break;
}
}
}

static void ShowHabits(HabitManager manager)
{
Console.WriteLine("\nСписок привычек:");
if (manager.Habits.Count == 0)
{
Console.WriteLine("Список пока пуст.");
return;
}

for (int i = 0; i < manager.Habits.Count; i++)
{
var h = manager.Habits[i];
string status = h.IsCompletedToday ? "[Выполнено]" : "[Не выполнено]";
Console.WriteLine($"{i + 1}. {h.Name} — Серия: {h.Streak} дн. {status}");
}
}
}
}
