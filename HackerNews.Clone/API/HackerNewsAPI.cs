using System;
using HackerNews.Database.Tables;
using Newtonsoft.Json.Linq;

namespace HackerNews.Clone.API
{
    public static class HackerNewsAPI
    {
        public static Item GetItem(long itemID)
        {
            string itemString = HTTPClient.Get($"item/{itemID}.json");
            JObject itemJObj = JObject.Parse(itemString);
            Item returnItem = new Item()
            {
                ID = Convert.ToInt64(itemJObj["id"]),
                Deleted = Convert.ToBoolean(itemJObj["deleted"]),
                Type = Convert.ToString(itemJObj["type"]),
                Author = Convert.ToString(itemJObj["by"]),
                Time = Convert.ToInt64(itemJObj["time"]),
                Text = Convert.ToString(itemJObj["text"]),
                Dead = Convert.ToBoolean(itemJObj["dead"]),
                Parent = Convert.ToInt64(itemJObj["parent"]),
                Poll = Convert.ToInt64(itemJObj["poll"]),
                Kids = Convert.ToString(itemJObj["kids"]),
                URL = Convert.ToString(itemJObj["url"]),
                Score = Convert.ToInt64(itemJObj["score"]),
                Title = Convert.ToString(itemJObj["title"]),
                Parts = Convert.ToString(itemJObj["parts"]),
                Descendants = Convert.ToInt64(itemJObj["descendants"])
            };
            return returnItem;
        }

        public static long GetMaxID()
        {
            return Convert.ToInt64(HTTPClient.Get("maxitem.json"));
        }
    }
}
