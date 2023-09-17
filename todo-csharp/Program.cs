using System.Diagnostics;
using todo_csharp;

var stopWatch = new Stopwatch();
var jsonData = File.ReadAllText("todo.json");

stopWatch.Start();
var list = TodoList.FromJson(jsonData);
stopWatch.Stop();

var elapsedFrom = stopWatch.ElapsedMilliseconds * 0.001;

if (list != null)
{
    // list.Print();
    
    stopWatch.Restart();
    list.ToJson();
    stopWatch.Stop();

    var elapsedTo = stopWatch.ElapsedMilliseconds * 0.001;
    
    Console.WriteLine($"fromJson - {elapsedFrom} seconds, toJson - {elapsedTo} seconds");
}