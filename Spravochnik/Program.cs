namespace Spravochnik
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var workers = new Repository();
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
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели некорректные данные");
                    continue;
                }
                try
                {
                    if (chose == 2)
                    {
                        workers.AddWorker();
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
                            workers.ShowWorkers();
                            break;
                        case 3:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            workers.ShowWorkers(workers.GetWorkerById(id));
                            break;
                        case 4:
                            Console.WriteLine("Введите дату начала выборки");
                            DateTime dateFrom = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Введите дату конца выборки");
                            DateTime dateTo = DateTime.Parse(Console.ReadLine());
                            workers.ShowWorkers(workers.GetWorkersBetweenTwoDates(dateFrom, dateTo));
                            break;
                        case 5:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            workers.DeleteWorker(id);
                            break;
                        case 6:
                            Console.WriteLine("Введите id");
                            id = Int32.Parse(Console.ReadLine());
                            workers.EditWorkerById(id);
                            break;
                        case 8:
                            workers.SaveWorkers();
                            return;
                        case 7:
                            workers.ShowWorkers(workers.ShowByBirthDate());
                            break;
                        default:
                            Console.WriteLine("Такой команды нет!");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели некорректные данные, файла не существует или нет такого работника!");
                    continue;
                }
            }
        }
    }
}
