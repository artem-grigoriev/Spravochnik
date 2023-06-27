using Models;

namespace Spravochnik.Extensions;

internal static class WorkerExtensions
{
    public static void WriteInConsole(this Worker worker)
    {
        Console.WriteLine($"{worker.Id} " +
              $"{worker.Fio} {worker.Age} {worker.Height} " +
              $"{worker.BirthDate} {worker.BirthPlace}");
    }
}