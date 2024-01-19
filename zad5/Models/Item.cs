using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace zad5.Models
{
    public class Item
    {
        [JsonProperty(PropertyName ="id")]
        public string? Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
        
        [Required]
        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; } 
        
        [JsonProperty(PropertyName = "completed")]
        public string? Completed { get; set; }

    }
}
