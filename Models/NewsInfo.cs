using System;
using System.Collections.Generic;

namespace ScraperDb.Models
{
    public class NewsInfo
    {
            public string Title {get; set;}
            public string Author {get; set;}
            public string Source {get; set;}
            public string Description {get; set;}
            public string Url {get; set;}
            public string ImageUrl {get; set;}
            public string DatePublished {get; set;}
    }
}