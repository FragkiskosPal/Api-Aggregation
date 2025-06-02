using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlobalInsightsApi_Assessment.Models.News
{
    public class NewsData
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("articles")]
        public List<Article> Articles { get; set; } = new();
    }
}
