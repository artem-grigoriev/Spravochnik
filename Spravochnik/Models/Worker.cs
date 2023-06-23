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
    }
}
