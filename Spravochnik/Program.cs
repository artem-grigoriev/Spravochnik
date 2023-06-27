namespace Spravochnik;

internal class Program
{
    private static readonly ConsoleWriter writer = new(new Repository());

    private static void Main(string[] args)
    {
        while (true)
        {
            int chose;
            int id;
            Menu();
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
                switch (chose)
                {
                    case 1:
                        writer.ShowWorkers();
                        continue;
                    case 2:
                        writer.AddWorker();
                        continue;
                    case 3:
                        Console.WriteLine("Введите id");
                        writer.ShowWorker(Int32.Parse(Console.ReadLine()));
                        break;

                    case 4:
                        Console.WriteLine("Введите дату начала выборки");
                        DateTime dateFrom = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введите дату конца выборки");
                        DateTime dateTo = DateTime.Parse(Console.ReadLine());
                        writer.ShowWorkers(dateFrom, dateTo);
                        break;

                    case 5:
                        Console.WriteLine("Введите id");
                        id = Int32.Parse(Console.ReadLine());
                        writer.DeleteWorker(id);
                        break;

                    case 6:
                        Console.WriteLine("Введите id");
                        id = Int32.Parse(Console.ReadLine());
                        writer.EditWorkerById(id);
                        break;

                    case 8:
                        writer.SaveWorkers();
                        return;

                    case 7:
                        writer.ShowByBirthDate();
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

    private static void Menu()
    {
        Console.WriteLine("1 - вывести данные на экран, 2 - заполнить данные и добавить новую запись в конец файла.");
        Console.WriteLine("3 - получить рабочего по id, 4 - получить рабочих по дате добаления, 5 - удалить по id,");
        Console.WriteLine("6 - редактировать рабочего по id, 7 - сортировать по дате, 8 - сохранить и выйти");
    }
}