namespace DVDStorage2.Models
{
    public class Dvd
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Language> SpokenLanguages { get; set; }

        public List<Language> Subtitles { get; set; }

        public int Playtime { get; set; }

        public MpaaRating Rating { get; set; }

        public DvdState State { get; set; }
    }
}
