using System;

namespace PetStore.Models
{
    using Newtonsoft.Json;

    public class Tag
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
