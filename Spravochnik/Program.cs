namespace Spravochnik
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var workers = Repository.GetAllWorkers();
            while (true)
            {
                Console.WriteLine("1 - вывести данные на экран, 2 - заполнить данные и добавить новую запись в конец файла.");
                Console.WriteLine("3 - получить рабочего по id, 4 - получить рабочих по дате добаления, 5 - удалить по id,");
                Console.WriteLine("6 - редактировать рабочего по id, 7 - сортировать по дате, 8 - сохранить и выйти");
                int chose;
                int id;
                try
                {
                    chose = Int32.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Вы ввели некорректные данные");
                    continue;
                }
                try
                {
                    if (chose == 2)
                    {
                        Repository.AddWorker(workers);
                        workers = Repository.GetAllWorkers();
                        continue;
                    }
                    if(workers == null)
                    {
                        Console.WriteLine("Файла с работниками не существует или он пустой!");
                        continue;
                    }
                    switch (chose)
                    {
                        case 1:
                            Repository.ShowWorkers(workers);
                            break;
                        case 3:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            Repository.ShowWorkers(Repository.GetWorkerById(workers, id));
                            break;
                        case 4:
                            Console.WriteLine("Введите дату начала выборки");
                            DateTime dateFrom = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Введите дату конца выборки");
                            DateTime dateTo = DateTime.Parse(Console.ReadLine());
                            Repository.ShowWorkers(Repository.GetWorkersBetweenTwoDates(workers, dateFrom, dateTo));
                            break;
                        case 5:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            Repository.DeleteWorker(workers, id);
                            break;
                        case 6:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            Repository.EditWorkerById(workers, id);
                            break;
                        case 8:
                            Repository.SaveWorkers(workers);
                            return;
                        case 7:
                            Repository.ShowWorkers(workers.OrderBy(w => w.BirthDate).ToList());
                            break;
                        default:
                            Console.WriteLine("Такой команды нет!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Вы ввели некорректные данные, файла не существует или нет такого работника!");
                    continue;
                }
            }
        }
    }
}
