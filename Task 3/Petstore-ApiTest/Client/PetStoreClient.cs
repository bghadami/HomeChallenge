namespace PetStore.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Configuration;

    using Microsoft.Extensions.Options;

    using Models;

    using Newtonsoft.Json;

    public class PetStoreClient 
    {
        #region Fields

        private readonly HttpClient client;
        private readonly string apiVersion;
        private readonly string apiKey;
        private readonly string baseUrl;
        private readonly string petPath;
        private readonly string storePath;
        private readonly string userPath;
        
        #endregion

 

        #region Constructors

        public PetStoreClient(IOptions<PetStoreClientConfiguration> configuration, HttpClient client)
        {
            PetStoreClientConfiguration clientConfiguration = configuration.Value;

            apiVersion = clientConfiguration.ApiVersion;
            apiKey = clientConfiguration.ApiKey;
            baseUrl = clientConfiguration.BaseUrl;
            petPath = clientConfiguration.PetPath;
            storePath = clientConfiguration.StorePath;
            userPath = clientConfiguration.UserPath;
            
          

            this.client = client;
            this.client.BaseAddress = new Uri($"{baseUrl}/{apiVersion}/");
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Add("api_key", $"{apiKey}");
        }

        #endregion

        #region PetApi

        public async Task<Pet> UpdatePet(Pet pet)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{petPath}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Pet> AddPet(Pet pet)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(pet), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{petPath}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<List<Pet>> GetPetByStatus(IList<PetStatus> statuslist)
        {
            string status = "";
            foreach (var str in statuslist)
                status = status != "" ? $"{status}&status={str}" : $"status={str}";

            
            HttpResponseMessage response = await client.GetAsync($"{petPath}/findByStatus?{status}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Pet>>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<List<Pet>> GetPetByTag(IList<string> tagList)
        {
            string tag = "";
            foreach (var str in tagList)
                tag = tag != "" ? $"{tag}&tags={str}" : $"tags={str}";

            HttpResponseMessage response = await client.GetAsync($"{petPath}/findByTags?{tag}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<Pet>>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Pet> GetPetById(Int64 id)
        {
            HttpResponseMessage response = await client.GetAsync($"{petPath}/{id}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Pet> UpdatePet(Int64 id, string name, PetStatus status)
        {
            string qstring = "";
            if (name != "")
                qstring = $"name={name}";
            if (status != null)
                qstring = (qstring == "" ? "" : qstring + "&") + $"status={status.ToString()}";
            
            HttpResponseMessage response = await client.PostAsync($"{petPath}/{id}?{qstring}", null);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Pet>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Boolean> DeletePet(Int64 id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{petPath}/{id}");
            return response.IsSuccessStatusCode ? true : false;
        }

        #endregion
        
        #region StoreApi

        public async Task<Inventory> GetInventories()
        {
            HttpResponseMessage response = await client.GetAsync($"{storePath}/inventory");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Inventory>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Order> AddOrder(Order order)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{storePath}/order", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Order>(response.Content.ReadAsStringAsync().Result) : null;
        }
        public async Task<Order> GetOrder(Int64 orderId)
        {
            HttpResponseMessage response = await client.GetAsync($"{storePath}/order/{orderId}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Order>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<Boolean> DeleteOrder(Int64 orderId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{storePath}/order/{orderId}");
            return response.IsSuccessStatusCode ? true : false;
            
        }
        

        #endregion      

       
        #region UserApi
        public async Task<User> AddUser(User user)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{userPath}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<List<User>> AddUserWithList(List<User> users)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{userPath}/createWithList", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<User>>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<string> UserLogin(string userName, string usePassword)
        {
            HttpResponseMessage response = await client.GetAsync($"{userPath}/login?username={userName}&password={usePassword}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<string> UserLogout()
        {
            HttpResponseMessage response = await client.GetAsync($"{userPath}/logout");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result) : null;
        }
        
        public async Task<string> GetUserByUserName(string userName)
        {
            HttpResponseMessage response = await client.GetAsync($"{userPath}/{userName}");
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result) : null;
        }
        public async Task<User> UpdateUser(string userName, User user)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{userPath}/{userName}", payload);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result) : null;
        }
        public async Task<Boolean> DeleteUser(string userName)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{userPath}/{userName}");
            return response.IsSuccessStatusCode ? true : false;
        }
        #endregion
        
    }
}
