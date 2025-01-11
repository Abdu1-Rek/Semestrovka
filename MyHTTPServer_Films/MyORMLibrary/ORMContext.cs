using Npgsql;

public class ORMContext
{
    private readonly string _connectionString;

    public ORMContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    

    public IEnumerable<Movie> GetAllMovies()
    {
        var result = new List<Movie>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT id, title, description, release_year, genre_id, country_id, rating, poster_url FROM movies";
            var command = new NpgsqlCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var movie = new Movie
                    {
                        // id, title, description, release_year, genre_id, country_id, rating, poster_url
                        id = reader.GetInt32(0),
                        title = reader.GetString(1),
                        description = reader.GetString(2),
                        release_year = reader.GetInt32(3),
                        genre_id = reader.GetInt32(4),
                        country_id = reader.GetInt32(5),
                        rating = reader.GetInt32(6),
                        poster_url = reader.GetString(7)
                    };
                    result.Add(movie);
                }
            }
        }
        return result;
    }
    
    
    public MovieDetails GetMovieDetailsById(int movieId)
{
    MovieDetails movieDetails = null;

    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        
        // Запрос для получения основной информации о фильме
        var query = "SELECT title, description, release_year, genre_id, country_id, rating, poster_url FROM movies WHERE id = @id";
        var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", movieId);

        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                movieDetails = new MovieDetails
                {   
                    title = reader.GetString(0),
                    description = reader.GetString(1),
                    release_year = reader.GetInt32(2),
                    genre_id = reader.GetInt32(3),
                    country_id = reader.GetInt32(4),
                    rating = reader.GetInt32(5),
                    poster_url = reader.GetString(6)
                };
            }
        }
    }
    return movieDetails;  // Если фильма нет, вернется null
}
}
// id, title, description, release_year, genre_id, country_id, rating, poster_url
public class Movie
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

public class MovieDetails
{
    public string title { get; set; }
    public string description { get; set; }
    public int release_year { get; set; }
    public int genre_id { get; set; }
    public int country_id { get; set; }
    public int rating { get; set; }
    public string poster_url { get; set; }
}