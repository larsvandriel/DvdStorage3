namespace DVDStorage2.Models
{
    public class Cabinet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Shelf> Shelves { get; set; }
    }
}
