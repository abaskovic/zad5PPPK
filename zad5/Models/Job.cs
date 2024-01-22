using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace zad5.Models
{
    public class Job
    {
        [JsonProperty(PropertyName ="id")]
        public string? Id { get; set; }


        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set; }

        [Required]
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
        
        [Required]
        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; } 

        [JsonProperty(PropertyName = "personId")]
        public string? PersonId { get; set; }

        [JsonProperty(PropertyName = "personFullName")]
            public string? PersonFullName { get; set; }

    }
}
