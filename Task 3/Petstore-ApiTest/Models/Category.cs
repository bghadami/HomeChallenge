using System;

namespace PetStore.Models
{
    using Newtonsoft.Json;

    public class Category
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
