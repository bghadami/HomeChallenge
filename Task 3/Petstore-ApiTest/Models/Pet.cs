using System;

namespace PetStore.Models
{
    using Newtonsoft.Json;

    public class Pet
    {
        
        [JsonProperty("id")]
        public Int64 Id { get; set; }
        
        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public string[] PhotoUrls { get; set; }
        
        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("status")]
        public PetStatus Status { get; set; }

    }
}
