using System.IO;
using System.Text;
using HttpServerLibrary;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.HttpResponce;

namespace MyHTTPServer.EndPoints;

public class IndexEndpoint : BaseEndPoint
{
    [Get("index")]
    public IHttpResponceResult GetIndex()
    {
        // Чтение основного HTML-шаблона
        var filePath = @"MyHTTPServer/Templates/Pages/Dashboard/index.html";
        var fileContent = File.ReadAllText(filePath);

        // Получение данных из базы
        var ormContext = new ORMContext("Host=localhost; Port=2288; Username=postgres; Password=1a2s3dqzwxec; Database=postgres");
        var moviesList = ormContext.GetAllMovies();

        // Генерация HTML для фильмов
        var moviesHtml = new StringBuilder();
        foreach (var movie in moviesList)
        {
            moviesHtml.Append($@"
                <div class='t-feed__grid-col t-col t-col_4'>
                    <div class='movie-card'>
                        <a href='/assets/home.html'>
                            <img src='{movie.poster_url}' alt='{movie.title}' width = '268' height = '148.88'/>
                        </a>
                        <div class='series-card-description'>
                            <h3>{movie.title}</h3>
                            <h6>{movie.genre_id} | {movie.release_year}</h6>
                        </div>
                    </div>
                </div>
            ");
        }

        // Замена плейсхолдеров в шаблоне
        fileContent = fileContent.Replace("{{MOVIES_CARDS}}", moviesHtml.ToString());

        return Html(fileContent);
    }
}
