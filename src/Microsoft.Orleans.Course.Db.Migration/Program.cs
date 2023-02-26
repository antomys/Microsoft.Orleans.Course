using Microsoft.Orleans.Course.Db.Migration;

var connectionString =
    args.FirstOrDefault()
    ?? "Server=localhost;Database=Microsoft_Orleans_Course;Uid=root;Pwd=password;";

var engine = connectionString.BuildMySqlEngine();

var result = engine.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif                
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();

return 0;