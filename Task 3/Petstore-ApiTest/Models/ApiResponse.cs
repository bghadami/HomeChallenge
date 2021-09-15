using System;

namespace PetStore.Models
{
    using Newtonsoft.Json;
   
    public class ApiResponse
    {
        [JsonProperty("code")]
        public Int32 Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
        
    }
}
