namespace MyORMLibrary
{
    public class Films
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int release_year { get; set; }
        public int genre_id { get; set; }
        public int country_id { get; set; }
        public int rating { get; set; }
        public string poster_url { get; set; }
    }
}