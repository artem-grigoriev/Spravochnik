using Models;
using Spravochnik.Interfaces;

namespace Spravochnik;

internal class Repository : IRepository
{
    private const string FilePath = @"..\..\..\workers.txt";
    private List<Worker> workersList;

    public int NewId
    {
        get
        {
            return (workersList != null) ? workersList.Select(worker => worker.Id).Max() + 1 : 1;
        }
    }

    public List<Worker> WorkersList { get => workersList; }

    public Repository() => workersList = InitWorkers();

    public void AddWorker(Worker worker)
    {
        if(workersList != null)
            workersList.Add(worker);
        else
            workersList = new List<Worker>() { worker };
    }

    public void DeleteWorker(int id) => workersList.RemoveAll(w => w.Id == id);

    public void EditWorkerById(int id, Worker newWorker)
    {
        var workerIndex = workersList.FindIndex(w => w.Id == id);

        workersList[workerIndex] = newWorker;
    }

    public Worker GetWorkerById(int id) => workersList.Find(w => w.Id == id);

    public void SaveWorkers()
    {
        if (false)
        {
            File.WriteAllText(FilePath, ""); // просто чистка сразу, всё равно перезаписывать

            // Это если Юзать линк, так тоже можно, селект твои данные легко приводит к нужному формату
            var toSave = workersList.Select(w => $"{w.Id}#{w.DateAdd}#" +
                                                 $"{w.Fio}#{w.Age}#{w.Height}#" +
                                                 $"{w.BirthDate}#{w.BirthPlace}").ToArray();

            File.WriteAllLines(FilePath, toSave);
        }
        else
        {
            // И второй вариант.. Я тебе сказал, разобраться с потоками, проигнорил
            using var sw = new StreamWriter(FilePath);
            foreach (var w in workersList)
            {
                sw.WriteLine($"{w.Id}#{w.DateAdd}#" +
                             $"{w.Fio}#{w.Age}#{w.Height}#" +
                             $"{w.BirthDate}#{w.BirthPlace}");
            }
            sw.Close(); // Поток вообще сам убьется, как из метода выйдет, но лучше сделать так
            sw.Dispose();
        }
    }

    private static Worker ConvertStringToWorker(string[] item)
    {
        return new Worker()
        {
            Id = Int32.Parse(item[0]),
            DateAdd = DateTime.Parse(item[1]),
            Fio = item[2],
            Height = Int32.Parse(item[4]),
            BirthDate = DateTime.Parse(item[5]), // почему то возвращает со временем
            BirthPlace = item[6]
        };
    }

    private List<Worker>? InitWorkers()
    {
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
            return new();
        }
        StreamReader reader = new(FilePath);
        List<Worker> workers = new();
        workers = reader.ReadToEnd()?.Split('\n')
            .Where(x => x.Length > 0)
            .Select(x => x.Split('#'))
            .Select(ConvertStringToWorker)
            .ToList() ?? new();
        reader.Close();
        reader.Dispose();
        return workers;
        /*пока с yield не разобрался*/
        /*string? line;
        while ((line = reader.ReadLine())!= null)
        {
            string[] parsedLine = line.Split('#');
            yield return ConvertStringToWorker(parsedLine);
        }*/


        /*return File.ReadAllLines(FilePath)
            ?.Select(x => x.Split('#'))
            .Select(x => ConvertStringToWorker(x))
            .ToList() ?? new();*/

        // И да, тут тоже лучше StreamReader
        // yield return new Worker(); //ляляля
    }
}