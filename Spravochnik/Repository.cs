using Models;

namespace Spravochnik
{
    internal class Repository
    {
        public static void AddWorker(List<Worker> workers)
        {
            int newId;
            int[] ids = (workers != null) ? workers.Select(w => w.Id).ToArray() : new int[1] { 0 };
            newId = (ids[0] == 0) ? 1 : ids.OrderBy(i => i).Reverse().First() + 1;
            Console.WriteLine("Введите через запятую(после запятой пробел не ставить) ФИО, возраст, рост, дату и место рождения:");            
            try 
            {
                string[] item = Console.ReadLine().Split(',');
                Worker worker = new()
                {
                    Id = newId,
                    DateAdd = DateTime.Now,
                    Fio = item[0],
                    Age = Int32.Parse(item[1]),
                    Height = Int32.Parse(item[2]),
                    BirthDate = DateTime.Parse(item[3]), // почему то возвращает со временем
                    BirthPlace = item[4]
                };
                if (workers != null)
                    workers.Add(worker);
                else workers = new List<Worker>() { worker };
                SaveWorkers(workers);
            }
            catch 
            {
                throw new Exception();
            }
        }
        public static void SaveWorkers(List<Worker> workers)
        {
            if (workers == null)
                File.Delete(@"..\..\..\workers.txt");
            else
            {
                string[] lines = new string[workers.Count];
                for (int i = 0; i < workers.Count; i++)
                {
                    lines[i] = $"{workers[i].Id}#{workers[i].DateAdd}#" +
                               $"{workers[i].Fio}#{workers[i].Age}#{workers[i].Height}#" +
                               $"{workers[i].BirthDate}#{workers[i].BirthPlace}";
                }
                File.WriteAllLines(@"..\..\..\workers.txt", lines);
            }
        }
        public static void ShowWorkers(List<Worker> workers)
        {
            if (workers == null)
                throw new Exception();
            for (int i = 0; i < workers.Count; i++)
                ShowWorkers(workers[i]);
        }
        public static void ShowWorkers(Worker worker)
        {
            Console.WriteLine($"{worker.Id} " +
                              $"{worker.Fio} {worker.Age} {worker.Height} " +
                              $"{worker.BirthDate} {worker.BirthPlace}");
        }
        public static List<Worker> GetAllWorkers()
        {
            if (!File.Exists(@"..\..\..\workers.txt"))
                return null;
            List<Worker> workers = new();
            List<string[]> lines = new();
            lines = File.ReadAllLines(@"..\..\..\workers.txt").Select(x => x.Split('#')).ToList();
            if (!(lines.Count > 0))
                return null;
            foreach (var item in lines)
            {
                Worker worker = new()
                {
                    Id = Int32.Parse(item[0]),
                    DateAdd = DateTime.Parse(item[1]),
                    Fio = item[2],
                    Age = Int32.Parse(item[3]),
                    Height = Int32.Parse(item[4]),
                    BirthDate = DateTime.Parse(item[5]), // почему то возвращает со временем
                    BirthPlace = item[6]
                };
                workers.Add(worker);
            }
            return workers;
        }
        public static Worker GetWorkerById(List<Worker> workers, int id)
        {
            return workers.Find(w => w.Id == id);
        }
        public static List<Worker> GetWorkersBetweenTwoDates(List<Worker> workers, DateTime dateFrom, DateTime dateTo)
        {
            return workers.Where(w => w.DateAdd >= dateFrom && w.DateAdd <= dateTo).ToList();            
        }
        public static void DeleteWorker(List<Worker> workers, int id)
        {
            workers.RemoveAll(w => w.Id == id);
        }
        public static void EditWorkerById(List<Worker> workers, int id)
        {
            var workerIndex = workers.FindIndex(w => w.Id == id);
            if (workerIndex == -1) throw new Exception();
            Console.WriteLine("Введите через запятую(после запятой пробел не ставить) ФИО, возраст, рост, дату и место рождения:");
            try
            {
                string[] item = Console.ReadLine().Split(',');
                Worker worker = new()
                {
                    Id = workers[workerIndex].Id,
                    DateAdd = workers[workerIndex].DateAdd,
                    Fio = item[0],
                    Age = Int32.Parse(item[1]),
                    Height = Int32.Parse(item[2]),
                    BirthDate = DateTime.Parse(item[3]), // почему то возвращает со временем
                    BirthPlace = item[4]
                };
                workers[workerIndex] = worker;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Вы ввели некорректные данные!");
            }
        }
    }
}
