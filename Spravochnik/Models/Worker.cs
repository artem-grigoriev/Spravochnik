namespace Models
{
    struct Worker
    {
        public int Id { get; set; }
        public DateTime DateAdd { get; set; }
        public string Fio { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public static Worker AddWorker(int id, DateTime dateAdd)
        {
            Console.WriteLine("Введите через запятую(после запятой пробел не ставить) ФИО, возраст, рост, дату и место рождения:");
            string[] item = Console.ReadLine().Split(',');
            return new Worker()
            {
                Id = id,
                DateAdd = dateAdd,
                Fio = item[0],
                Age = Int32.Parse(item[1]),
                Height = Int32.Parse(item[2]),
                BirthDate = DateTime.Parse(item[3]), // почему то возвращает со временем
                BirthPlace = item[4]
            };
        }
        public static Worker AddWorker(string[] item)
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
