C# and .net Finance Application

Uses selenium to log in to a yahoo finance account and scrapes stock data from the account's portfolio. Puts data into SQLite Database.

Requires user to be logged in to application to trigger a scrape of the yahoo finance data.

Retrieves S&P 500, Nasdaq and Dow information using HTML Agility Pack

Retrieves Top Headlines using API from newsapi.org

--Required to Run Application--

Keys.cs file in the following format:

    public class Keys
        {
            public string Password { get; private set; }
            public string Email { get; private set;}
            public string APIKey { get; private set;}

            public Keys()
            {
                Password = "Password to Yahoo Finance Account";
                Email = "Username for Yahoo Finance Account";
                APIKey = "api key from newsapi.org";
            }
        }

