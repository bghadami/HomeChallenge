using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Configuration;

namespace PetStore.Tests
{
    using System;
    using System.Threading.Tasks;

    using Client;


    using FluentAssertions;

    using Models;

    using Xunit;

    public class StoreTests : IAsyncDisposable
    {
        #region Fields

        private readonly PetStoreClient client;
        private Int64 createdPetId;
        private Int64 createdOrderId;
        private static readonly Order defaultOrder;
        private static readonly Pet defaultPet;

        #endregion

        #region Constructors

        public StoreTests()
        {
            PetStoreClient PetStoreClient = PetStoreClientConfiguration.GetPetStoreClientConfiguration();
            client = PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreClient));
        }

        static StoreTests()
        {
            defaultOrder = new Order
            {
                Complete = false,
                Quantity = 23,
                ShipDate = DateTimeOffset.MaxValue.Date,
                Status = OrderStatus.placed
            };

            defaultPet = new Pet()
            {
                Name = "doggie",
                Status = PetStatus.available,
                Category = new Category() {Id = 1, Name = "Dogs"},
                Tags = new[] {new Tag() {Id = 0, Name = "string"}},
                PhotoUrls = new[] {"string"}
            };
        }

        #endregion

        #region Methods

        [Fact]
        public async Task AddOrderTest()
        {
            Pet testPet = await client.AddPet(defaultPet);
            createdPetId = testPet.Id;
            defaultOrder.PetId = testPet.Id;
        
            Order order = await client.AddOrder(defaultOrder);
            createdOrderId = order.Id;
        
            order.Should().BeEquivalentTo(defaultOrder, options => options.Excluding(o => o.Id));
            order.PetId.Should().Be(testPet.Id);
        }
        [Fact]
        public async Task DeleteOrderTest()
        {
            Pet testPet = await client.AddPet(defaultPet);
            createdPetId = testPet.Id;
            defaultOrder.PetId = testPet.Id;
            Order order = await client.AddOrder(defaultOrder);
        
            bool deleteOrderResponse = await client.DeleteOrder(order.Id);
        
            Order deletedOrder = await client.GetOrder(order.PetId);

            deleteOrderResponse.Should().Be(true);
            deletedOrder.Should().BeNull();
        }
        [Fact]
        public async Task GetInventoryTest()
        {
            Inventory inventory = await client.GetInventories();
            inventory.Should().NotBeNull();
        }
        [Fact]
        public async Task GetNonExistedOrderTest()
        {
            const Int64 TEST_PET_ID = Int64.MaxValue;
            await client.DeleteOrder(TEST_PET_ID);
            Order order = await client.GetOrder(TEST_PET_ID);
            order.Should().BeNull();
        }
        [Fact]
        public async Task GetOrderByIdTest()
        {
            Order order = await client.AddOrder(defaultOrder);
            createdOrderId = order.Id;
        
            Order getOrderresponse = await client.GetOrder(order.Id);
        
            getOrderresponse.Should().BeEquivalentTo(order);
        }
        
        #endregion

        #region IAsyncDisposable

        public async ValueTask DisposeAsync()
        {
            await client.DeleteOrder(createdOrderId);
            await client.DeletePet(createdPetId);
        }

        #endregion
    }
}
