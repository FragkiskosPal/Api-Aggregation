using Newtonsoft.Json;

namespace GlobalInsightsApi_Assessment.Models.GitHub
{
    public class Owner
    {
        [JsonProperty("login")]
        public string Login { get; set; } = "";
    }

    public class Repository
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; } = "";

        [JsonProperty("stargazers_count")]
        public int Stars { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; } = null!;
    }
}
