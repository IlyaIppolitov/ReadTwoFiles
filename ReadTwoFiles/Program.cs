Console.WriteLine("Начало: " + Environment.CurrentManagedThreadId);
var task = Task.Run(() =>
{
    if (File.Exists("file1.txt"))
    {
        using var sr = File.OpenText("file1.txt");
        var content = sr.ReadToEnd();
        var spacesCount = content.Count(chr => chr == ' ');
        Thread.Sleep(TimeSpan.FromSeconds(1)); // если так сделать, то этот поток не успеет закончится до завершения
                                               // потока основной программы и мы не увидим результат выполнения этого подсчёта
        Console.WriteLine("Количество пробелов в файле1: " + spacesCount);
        Console.WriteLine("Поток файла 1: " + Environment.CurrentManagedThreadId);
    }
});

if (File.Exists("file2.txt"))
{
    var content = File.ReadAllText("file2.txt");
    var spacesCount = content.Count(chr => chr == ' ');
    Console.WriteLine("Количество пробелов в файле2: " + spacesCount);
    Console.WriteLine("Поток файла 2: " + Environment.CurrentManagedThreadId);
}
task.Wait();
