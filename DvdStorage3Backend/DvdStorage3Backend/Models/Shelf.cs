namespace DVDStorage2.Models
{
    public class Shelf
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Dvd> Dvds { get; set; }
    }
}
