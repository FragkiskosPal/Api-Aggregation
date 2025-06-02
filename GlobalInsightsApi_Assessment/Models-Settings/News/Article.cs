// Models/News/Article.cs
using Newtonsoft.Json;

namespace GlobalInsightsApi_Assessment.Models.News
{
    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }

    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("source")]
        public Source Source { get; set; } = null!;

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = "";
    }
}
