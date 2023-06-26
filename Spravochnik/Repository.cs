using Models;

namespace Spravochnik
{
    internal class Repository
    {
        private List<Worker> workersList;

        public Repository() => workersList = GetAllWorkers();
        public void AddWorker()
        {
            int newId;
            int[] ids = (workersList != null) ? workersList.Select(w => w.Id).ToArray() : new int[1] { 0 };
            newId = (ids[0] == 0) ? 1 : ids.OrderBy(i => i).Reverse().First() + 1;            
            Worker worker = AddWorker(newId, DateTime.Now);
            if (workersList != null)
                workersList.Add(worker);
            else workersList = new List<Worker>() { worker };
        }
        public void SaveWorkers()
        {
            if (workersList == null)
                File.WriteAllText(@"..\..\..\workers.txt", "");
            else
            {
                string[] lines = new string[workersList.Count];
                for (int i = 0; i < workersList.Count; i++)
                    lines[i] = $"{workersList[i].Id}#{workersList[i].DateAdd}#" +
                               $"{workersList[i].Fio}#{workersList[i].Age}#{workersList[i].Height}#" +
                               $"{workersList[i].BirthDate}#{workersList[i].BirthPlace}";
                File.WriteAllLines(@"..\..\..\workers.txt", lines);
            }
        }
        public void ShowWorkers() => ShowWorkers(workersList);
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
                workers.Add(AddWorker(item));
            return workers;
        }
        public void EditWorkerById(int id)
        {
            var workerIndex = workersList.FindIndex(w => w.Id == id);
            if (workerIndex == -1) throw new Exception();
            workersList[workerIndex] = AddWorker(workersList[workerIndex].Id, workersList[workerIndex].DateAdd);
        }
        public Worker GetWorkerById(int id) => workersList.Find(w => w.Id == id);
        public List<Worker> GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo) => workersList.Where(w => w.DateAdd >= dateFrom && w.DateAdd <= dateTo).ToList();
        public void DeleteWorker(int id) => workersList.RemoveAll(w => w.Id == id);
        public List<Worker> ShowByBirthDate() => workersList.OrderBy(w => w.BirthDate).ToList();
        private static Worker AddWorker(int id, DateTime dateAdd)
        {
            Console.WriteLine("Введите через запятую(после запятой пробел не ставить) ФИО, рост, дату и место рождения:");
            string[] item = Console.ReadLine().Split(',');
            DateTime birthDate = DateTime.Parse(item[2]);
            var totalmonths = (DateTime.Now.Year - birthDate.Year) * 12 + DateTime.Now.Month - birthDate.Month;
            totalmonths += DateTime.Now.Day < birthDate.Day ? -1 : 0;
            return new Worker()
            {
                Id = id,
                DateAdd = dateAdd,
                Fio = item[0],
                Age = totalmonths / 12,
                Height = Int32.Parse(item[1]),
                BirthDate = birthDate, // почему то возвращает со временем
                BirthPlace = item[3]
            };
        }
        private static Worker AddWorker(string[] item)
        {
            return new Worker()
            {
                Id = Int32.Parse(item[0]),
                DateAdd = DateTime.Parse(item[1]),
                Fio = item[2],
                Age = Int32.Parse(item[3]),
                Height = Int32.Parse(item[4]),
                BirthDate = DateTime.Parse(item[5]), // почему то возвращает со временем
                BirthPlace = item[6]
            };
        }
    }
}
