using System;
using System.IO;

namespace MyHabitTracker
{
public static class StreamExamples
{
public static void RunDemo()
{
string path = "example.txt";

using (StreamWriter writer = new StreamWriter(path))
{
writer.WriteLine("Первая строка");
writer.WriteLine("Вторая строка");
}

using (StreamReader reader = new StreamReader(path))
{
string? line;
int lineNumber = 1;
while ((line = reader.ReadLine()) != null)
{
Console.WriteLine($"{lineNumber}: {line}");
lineNumber++;
}
}

if (File.Exists(path))
File.Delete(path);
}
}
}
