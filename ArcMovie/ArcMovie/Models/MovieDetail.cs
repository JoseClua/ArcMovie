using Newtonsoft.Json;

namespace ArcMovie.Models
{
    public class MovieDetail : Movie
    {                
        [JsonProperty(PropertyName = "budget")]
        public int? Budget { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public Genre[] Genres { get; set; }

        [JsonProperty(PropertyName = "homepage")]
        public string Homepage { get; set; }        

        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }
   
        [JsonProperty(PropertyName = "revenue")]
        public int? Revenue { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public int? Runtime { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "tagline")]
        public string Tagline { get; set; }        
    }
}
