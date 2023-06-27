namespace Models;

public struct Worker
{
    public int Id { get; init; }
    public DateTime DateAdd { get; init; }
    public string Fio { get; init; }

    public int Age
    {
        get
        {
            int age = DateAdd.Year - BirthDate.Year;
            int rounding = BirthDate >= DateTime.Now.AddYears(-age) ? 1 : 0;
            return age + rounding;
        }
    }

    public int Height { get; init; }
    public DateTime BirthDate { get; init; }
    public string BirthPlace { get; init; }

    public Worker(int id, string fio, int height, DateTime birthDate, string birthPlace)
    {
        Id = id;
        DateAdd = DateTime.Now;
        Fio = fio;
        Height = height;
        BirthDate = birthDate;
        BirthPlace = birthPlace;
    }
}