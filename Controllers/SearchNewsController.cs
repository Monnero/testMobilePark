using HotelWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace testMobilePark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchNewsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public readonly NewsDb _dbContext;

        public SearchNewsController(NewsDb dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;


        }
        [HttpGet(Name = "SearchNews")]
        public async Task<ActionResult<List<NewsItem>>> SearchNews(string keyword, Fragment fragment, Lang language)
        {
            List<NewsItem> response = new List<NewsItem>();
            var NewsApiKey = _configuration["APISettings:NewsApiKey"];
            List<News> news = new List<News>();

            //получаем новости
            var newsApiClient = new NewsApiClient(NewsApiKey);
            var articles = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = keyword,
                Language = language == 0 ? Languages.EN : Languages.RU
            });

            // считаем гласные
            switch (fragment)
            {
                case Fragment.Title:
                    response = (from article in articles.Articles
                                select new NewsItem
                                {
                                    Fragment = article.Title,
                                    VowelCount = CountVowels(article.Title ?? "", language)
                                })
                                     .OrderByDescending(item => item.VowelCount)
                                     .ToList();
                    break;
                case Fragment.Description:
                    response = (from article in articles.Articles
                                select new NewsItem
                                {
                                    Fragment = article.Description,
                                    VowelCount = CountVowels(article.Description ?? "", language)
                                })
                                .OrderByDescending(item => item.VowelCount)
                                .ToList();
                    break;
                case Fragment.Content:
                    response = (from article in articles.Articles
                                select new NewsItem
                                {
                                    Fragment = article.Content,
                                    VowelCount = CountVowels(article.Content ?? "", language)
                                })
                                .OrderByDescending(item => item.VowelCount)
                                .ToList();
                    break;
            }


            // 3. (Опционально) Запись лога в базу данных
            _dbContext.News.Add(new News() { FragmentType = response.First().Fragment,Language = language.ToString() });
            _dbContext.SaveChanges();
            //await LogRequest(keyword, fragment, language, newsItems);

            return Ok(response);
        }

        public enum Fragment
        {
            Title, Description, Content
        }
        public enum Lang
        {
            eng, rus
        }

        private int CountVowels(string text, Lang language)
        {
            var vowels = language == 0 ? "aeiou" : "аеёиоуыэюя";
            return text.ToLower().Count(x => vowels.Contains(x));
        }



    }
}
