using System;
using Chupelupe.Models.Helpers;
using Newtonsoft.Json;

namespace Chupelupe.Models
{
    public class Promotion : ObservableObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }
    }
}
