using HotelWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using testMobilePark.Data;

namespace testMobilePark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchNewsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public readonly NewsDb _dbContext;
        private const string engVovels = "aeiou";
        private const string rusVovels = "аеЄиоуыэю€";

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

            //собираем и в бд
            foreach (var item in response)
            {
                _dbContext.News.Add(new News()
                {
                    Keyword = keyword,
                    Fragment = item.Fragment,
                    Language = language.ToString(),
                    RequestTime = DateTime.Now,
                    FragmentType = fragment.ToString(),
                    VowelCount = item.VowelCount
                });
            }

            _dbContext.SaveChanges();

            return Ok(response);
        }

        private int CountVowels(string text, Lang language)
        {
            var vowels = language == Lang.eng ? engVovels : rusVovels;

            return text.ToLower().Count(vowels.Contains);
        }
    }
}
