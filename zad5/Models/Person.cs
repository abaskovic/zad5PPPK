using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace zad5.Models
{
    public class Person

    {
        [JsonProperty(PropertyName ="id")]
        public string? Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set; }
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [Required]
        [JsonProperty(PropertyName = "surname")]
        public string? Surname { get; set; } 
        [Required]
        [JsonProperty(PropertyName = "age")]
        public string? Age { get; set; }

        public override string? ToString()
        {
            return Name + " " + Surname;
        }
    }
}
