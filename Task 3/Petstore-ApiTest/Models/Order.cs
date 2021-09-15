namespace PetStore.Models
{
    using System;

    using Newtonsoft.Json;

    public class Order
    {

        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("petId")]
        public Int64 PetId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("shipDate")]
        public DateTimeOffset ShipDate { get; set; }
        
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }
        
        [JsonProperty("complete")]
        public bool Complete { get; set; }

    }
}
