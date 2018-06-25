using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Collections.Generic;
using ScraperDb.Models;

namespace ScraperDb.DataRetrieval
{
    class News
    {
        public static List<NewsInfo> GetNews()
        {
            List<NewsInfo> articleList = new List<NewsInfo>();

            List<string> sources = new List<string>(new string[] { "bloomberg,financial-times,fortune,financial-post,the-wall-street-journal" });
            
            var keys = new Keys();
            // init with your API key
            var newsApiClient = new NewsApiClient(keys.APIKey);
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
               // Q = "business",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                PageSize = 10,
                Sources = sources,
                From = DateTime.Now.AddDays(-1)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine("total: " + articlesResponse.TotalResults);
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    articleList.Add(new NewsInfo()
                    {
                       Title = article.Title,
                       Author = article.Author,
                       Source = article.Source.Name,
                       Description = article.Description,
                       Url = article.Url,
                       ImageUrl = article.UrlToImage,
                       DatePublished = article.PublishedAt.ToString()
                    });
                }
            }
            return articleList;
        }
    }
}