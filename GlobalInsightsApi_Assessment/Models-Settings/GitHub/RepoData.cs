using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlobalInsightsApi_Assessment.Models.GitHub
{
    public class RepoData
    {
        [JsonProperty("items")]
        public List<Repository> Items { get; set; } = new();
    }
}