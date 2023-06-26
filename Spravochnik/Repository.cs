using Models;

namespace Spravochnik
{
    internal class Repository
    {
        private List<Worker> Workers { get; set; }
        public Repository() => Workers = GetAllWorkers();
        public void AddWorker()
        {
            int newId;
            int[] ids = (Workers != null) ? Workers.Select(w => w.Id).ToArray() : new int[1] { 0 };
            newId = (ids[0] == 0) ? 1 : ids.OrderBy(i => i).Reverse().First() + 1;            
            Worker worker = Worker.AddWorker(newId, DateTime.Now);
            if (Workers != null)
                Workers.Add(worker);
            else Workers = new List<Worker>() { worker };
        }
        public void SaveWorkers()
        {
            if (Workers == null)
                File.WriteAllText(@"..\..\..\workers.txt", "");
            else
            {
                string[] lines = new string[Workers.Count];
                for (int i = 0; i < Workers.Count; i++)
                    lines[i] = $"{Workers[i].Id}#{Workers[i].DateAdd}#" +
                               $"{Workers[i].Fio}#{Workers[i].Age}#{Workers[i].Height}#" +
                               $"{Workers[i].BirthDate}#{Workers[i].BirthPlace}";
                File.WriteAllLines(@"..\..\..\workers.txt", lines);
            }
        }
        public void ShowWorkers() => ShowWorkers(Workers);
        public void ShowWorkers(Worker worker)
        {
            Console.WriteLine($"{worker.Id} " +
                              $"{worker.Fio} {worker.Age} {worker.Height} " +
                              $"{worker.BirthDate} {worker.BirthPlace}");
        }
        public void ShowWorkers(List<Worker> workers)
        {
            if (workers == null)
                throw new Exception();
            foreach (Worker worker in workers)
                ShowWorkers(worker);
        }
        private List<Worker> GetAllWorkers()
        {
            if (!File.Exists(@"..\..\..\workers.txt"))
                return null;
            List<Worker> workers = new();
            List<string[]> lines = File.ReadAllLines(@"..\..\..\workers.txt").Select(x => x.Split('#')).ToList();
            if (!(lines.Count > 0))
                return null;
            foreach (var item in lines)
                workers.Add(Worker.AddWorker(item));
            return workers;
        }
        public void EditWorkerById(int id)
        {
            var workerIndex = Workers.FindIndex(w => w.Id == id);
            if (workerIndex == -1) throw new Exception();
            Workers[workerIndex] = Worker.AddWorker(Workers[workerIndex].Id, Workers[workerIndex].DateAdd);
        }
        public Worker GetWorkerById(int id) => Workers.Find(w => w.Id == id);
        public List<Worker> GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo) => Workers.Where(w => w.DateAdd >= dateFrom && w.DateAdd <= dateTo).ToList();
        public void DeleteWorker(int id) => Workers.RemoveAll(w => w.Id == id);
        public List<Worker> ShowByBirthDate() => Workers.OrderBy(w => w.BirthDate).ToList();
    }
}
