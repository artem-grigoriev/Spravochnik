using Models;

namespace Spravochnik.Interfaces;

internal interface IRepository
{
    int NewId { get; }
    List<Worker> WorkersList { get; }

    void AddWorker(Worker worker);

    void DeleteWorker(int id);

    void EditWorkerById(int id, Worker newWorker);

    Worker GetWorkerById(int id);

    void SaveWorkers();
}