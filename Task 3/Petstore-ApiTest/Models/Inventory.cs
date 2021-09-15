namespace PetStore.Models
{
    using Newtonsoft.Json;

    public class Inventory
    {
        [JsonProperty("0")]
        public long Not { get; set; }

        [JsonProperty("approved")]
        public long Approved { get; set; }
        
        [JsonProperty("placed")]
        public long Placed { get; set; }

        [JsonProperty("delivered")]
        public long Delivered { get; set; }
    }
}
