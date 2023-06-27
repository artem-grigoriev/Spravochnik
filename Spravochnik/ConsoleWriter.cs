using Models;
using Spravochnik.Extensions;
using Spravochnik.Interfaces;

namespace Spravochnik;

internal class ConsoleWriter
{
    private readonly IRepository repository;

    public ConsoleWriter(IRepository repository)
    {
        this.repository = repository;
    }

    public void ShowWorker(int id)
    {
        ShowWorker(repository.GetWorkerById(id));
    }

    public void ShowWorkers() => ShowWorkers(repository.WorkersList);

    public void ShowWorkers(DateTime dateFrom, DateTime dateTo)
    {
        var toShow = repository.WorkersList.Where(w => w.DateAdd >= dateFrom && w.DateAdd <= dateTo).ToList();
        ShowWorkers(toShow);
    }

    public void AddWorker()
    {
        var worker = GetWorkerDataInConsole();

        repository.AddWorker(worker);
    }

    public void DeleteWorker(int id)
    {
        repository.DeleteWorker(id);
    }

    public void EditWorkerById(int id)
    {
        // Это условие число, чтоб эксепшн выкинуть, Тоже самое придется делать повторна в репо.
        var workerIndex = repository.WorkersList.FindIndex(w => w.Id == id);
        if (workerIndex == -1) throw new Exception();

        var worker = GetWorkerDataInConsole(id);

        repository.EditWorkerById(id, worker);
    }

    public void SaveWorkers()
    {
        repository.SaveWorkers();
    }

    public void ShowByBirthDate()
    {
        ShowWorkers(repository.WorkersList.OrderBy(x => x.BirthDate).ToList());
    }

    private Worker GetWorkerDataInConsole(int id = 0)
    {
        Console.WriteLine("Введите через запятую(после запятой пробел не ставить) ФИО, рост, дату и место рождения:");
        string[] item = Console.ReadLine().Split(',');
        return new Worker(id == 0 ? repository.NewId : id,
                                 item[0],
                                 Int32.Parse(item[1]),
                                 DateTime.Parse(item[2]),
                                 item[3]);
    }

    private void ShowWorker(Worker worker)
    {
        Console.WriteLine($"{worker.Id} " +
                          $"{worker.Fio} {worker.Age} {worker.Height} " +
                          $"{worker.BirthDate} {worker.BirthPlace}");
    }

    private void ShowWorkers(List<Worker>? workers)
    {
        if (workers == null)
            throw new Exception();
        foreach (Worker worker in workers)
            worker.WriteInConsole(); // Это просто как пример использования методов расширения, не более. Просто показать, как это работает
    }
}