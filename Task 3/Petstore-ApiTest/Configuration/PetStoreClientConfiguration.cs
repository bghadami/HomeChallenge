using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Client;

namespace PetStore.Configuration
{
    public class PetStoreClientConfiguration
    {
        public static  PetStoreClient GetPetStoreClientConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables().Build();
            var services = new ServiceCollection();
            services.AddOptions().Configure<PetStoreClientConfiguration>(configuration.GetSection(nameof(PetStoreClientConfiguration)));
            services.AddHttpClient<PetStoreClient, PetStoreClient>();
            IServiceProvider _serviceProvider = services.BuildServiceProvider();

            return _serviceProvider.GetRequiredService<PetStoreClient>();
        }
   
        public string ApiVersion { get; set; }
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string PetPath { get; set; }
        public string StorePath { get; set; }
        public string UserPath { get; set; }
        
        
    }
}
