namespace DVDStorage2.Models
{
    public class FilmSerie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Dvd> Dvds { get; set; }
    }
}
