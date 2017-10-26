using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArcMovie.Models
{
    public class GenreList
    {
        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }
    }
}
