using HttpServerLibrary;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.HttpResponce;

namespace MyHTTPServer.EndPoints
{
    
    public class MovieEndpoint : BaseEndPoint
    {
        private readonly ORMContext _context;

        // Конструктор с параметром для передачи ORMContext
        public MovieEndpoint()
        {
            var connectionString = "Host=localhost; Port=2288; Username=postgres; Password=1a2s3dqzwxec; Database=postgres"; // Укажите вашу строку подключения
            _context = new ORMContext(connectionString);  // Создаем ORMContext с параметром
        }

        [Get("movie")]
        public IHttpResponceResult GetMovie(int id) // Получаем id из запроса
        {
            var movieDetails = _context.GetMovieDetailsById(id); // Используем переданный id
            if (movieDetails == null)
            {
                return Html("<h1>Фильм не найден</h1>");
            }


            var filePath = @"Templates/Pages/Movie/movie.html";
            var fileContent = File.ReadAllText(filePath);

            // Вставляем данные в шаблон
            fileContent = fileContent.Replace("{{title}}", movieDetails.title)
                .Replace("{{description}}", movieDetails.description)
                .Replace("{{release_year}}", movieDetails.release_year.ToString())
                .Replace("{{genre}}", movieDetails.genre_id.ToString());
            
            return Html(fileContent);
        }

    }

}