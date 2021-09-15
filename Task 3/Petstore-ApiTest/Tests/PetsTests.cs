using System.Linq;
using PetStore.Configuration;

namespace PetStore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Client;
    using FluentAssertions;
    using Models;
    using Xunit;
    
    public class PetsTests : IAsyncDisposable
    {
        #region Fields

        private static readonly Pet defaultPet1_available;
        private static readonly Pet defaultPet2_pending;
        private static readonly Pet defaultPet3_sold;
        private List<Int64> createdPetIds;

        private readonly PetStoreClient client;

        #endregion

        #region Constructors

        public PetsTests()
        {
            PetStoreClient PetStoreClient = PetStoreClientConfiguration.GetPetStoreClientConfiguration();
            client = PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreClient));
            createdPetIds = new List<Int64>();
        }

        static PetsTests()
        {
            defaultPet1_available = new Pet()
            {
                Name = "doggie",
                Status = PetStatus.available,
                Category = new Category() {Id = 1, Name = "Dogs"},
                Tags = new[] {new Tag() {Id = 0, Name = "string"}},
                PhotoUrls = new[] {"string"}
            };
            defaultPet2_pending = new Pet()
            {
                Name = "Pet2",
                Status = PetStatus.pending,
                Category = new Category() {Id = 2, Name = "Cats"},
                Tags = new[] {new Tag() {Id = 1, Name = "string1"}},
                PhotoUrls = new[] {"Test Photo URL2"}
            };
            defaultPet3_sold = new Pet()
            {
                Name = "Pet3",
                Status = PetStatus.sold,
                Category = new Category() {Id = 3, Name = "Category_03"},
                Tags = new[] {new Tag() {Id = 3, Name = "string2"}},
                PhotoUrls = new[] {"Test Photo URL3"}
            };

        }

        #endregion

        #region Methods

        [Fact]
        public async Task AddPetTest()
        {
            Pet returnPet = await client.AddPet(defaultPet1_available);
            returnPet.Should().BeEquivalentTo(defaultPet1_available, options => options.Excluding(o=> o.Id));
            Assert.NotNull(returnPet.Id);
            createdPetIds.Add(returnPet.Id);
        }
        
        [Fact]
        public async Task UpdateExistedPetTest()
        {
            Pet existPet = await client.AddPet(defaultPet1_available);
            existPet.Name = "Update Pet name";
            existPet.Status = PetStatus.pending;
            Pet updatedPet = await client.UpdatePet(existPet);
            Assert.Equal(updatedPet.Name, existPet.Name);
            updatedPet.Should().BeEquivalentTo(existPet);
            createdPetIds.Add(existPet.Id);
        }
        [Fact]
        public async Task UpdateNonExistedPetTest()
        {
            Pet nonExistPet = defaultPet1_available;
            nonExistPet.Id = 0;
            // Delete if exist
            await client.DeletePet(nonExistPet.Id);
            
            Pet actualPet = await client.UpdatePet(nonExistPet);
            
            Assert.Null(actualPet);
        }
        [Fact]
        public async Task GetPetByIdTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);
            Pet actualPet = await client.GetPetById(testPet.Id);
        
            actualPet.Should().BeEquivalentTo(defaultPet1_available, options => options.Excluding(o=>o.Id));
            actualPet.Should().BeEquivalentTo(testPet);
            
            createdPetIds.Add(testPet.Id);
        }
        [Fact]
        public async Task GetNonExistedPetByIdTest()
        {
            Pet nonExistPet = defaultPet1_available;
            nonExistPet.Id = 0;
            // Delete if exist
            await client.DeletePet(nonExistPet.Id);

            Pet actualPet = await client.GetPetById(0);
        
            actualPet.Should().BeNull();
        }
        [Fact]
        public async Task GetPetListByStatusTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);
            Pet testPet2 = await client.AddPet(defaultPet2_pending);

            IList<PetStatus> statusList = new List<PetStatus>()
            {
                PetStatus.available
            };
            IList<Pet> petsList = await client.GetPetByStatus(statusList);
        
            petsList.Count.Should().BeGreaterThan(0);
            foreach (var pet in petsList)
            {
                pet.Status.Should().Be(PetStatus.available);
            }
            
            
            createdPetIds.Add(testPet.Id);
            createdPetIds.Add(testPet2.Id);
        }
        [Fact]
        public async Task GetPetListByMultipleStatusTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);
            Pet testPet2 = await client.AddPet(defaultPet2_pending);

            IList<PetStatus> statusList = new List<PetStatus>()
            {
                PetStatus.pending,
                PetStatus.available
            };
            IList<Pet> petsList = await client.GetPetByStatus(statusList);
        
            petsList.Count.Should().BeGreaterThan(0);
            foreach (var pet in petsList)
            {
                Assert.Equal(true, statusList.Contains(pet.Status));
            }
            
            createdPetIds.Add(testPet.Id);
            createdPetIds.Add(testPet2.Id);
        }
        [Fact]
        public async Task GetPetListByTagTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);
            Pet testPet2 = await client.AddPet(defaultPet2_pending);

            IList<string> tagList = new List<string>()
            {
                "string"
            };
            IList<Pet> petsList = await client.GetPetByTag(tagList);
        
            petsList.Count.Should().BeGreaterThan(0);
            foreach (var pet in petsList)
            {
                pet.Tags.Where(o => o.Name == "string").Count().Should().BeGreaterThan(0);
            }
            
            
            createdPetIds.Add(testPet.Id);
            createdPetIds.Add(testPet2.Id);
        }
        [Fact]
        public async Task GetPetListByMultipleTagTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);
            Pet testPet2 = await client.AddPet(defaultPet2_pending);

            IList<string> tagList = new List<string>()
            {
                "string",
                "string1"
            };
            IList<Pet> petsList = await client.GetPetByTag(tagList);
        
            petsList.Count.Should().BeGreaterThan(0);
            foreach (var pet in petsList)
            {
                pet.Tags.Where(o => tagList.Contains(o.Name)).Count().Should().BeGreaterThan(0);
            }
            
            
            createdPetIds.Add(testPet.Id);
            createdPetIds.Add(testPet2.Id);
        }
        [Fact]
        public async Task PartialUpdatePetTest()
        {
            Pet testPet = await client.AddPet(defaultPet1_available);

            
            const string UPDATED_NAME = "Updated name";
            const PetStatus UPDATED_STATUS = PetStatus.sold;
            
        
            Pet updateStatus = await client.UpdatePet(testPet.Id, UPDATED_NAME, UPDATED_STATUS);
        
            Pet actualPet = await client.GetPetById(testPet.Id);

            actualPet.Should().BeEquivalentTo(updateStatus);
            actualPet.Status.Should().Be(UPDATED_STATUS);
            actualPet.Name.Should().Be(UPDATED_NAME);
            
            createdPetIds.Add(testPet.Id);
        }
        [Fact]
        public async Task PartialUpdateNonExistedPetTest()
        {
            Pet nonExistPet = defaultPet1_available;
            nonExistPet.Id = 1;
            // Delete if exist
            await client.DeletePet(nonExistPet.Id);
            
            Pet updateResponse = await client.UpdatePet(1, "Test Name", PetStatus.available);
            updateResponse.Should().BeNull();
        }
        [Fact]
        public async Task DeletePetTest()
        {
            Pet actualPet = await client.AddPet(defaultPet1_available);
        
            bool deletePetResponse = await client.DeletePet(actualPet.Id);
        
            Pet deletedPet = await client.GetPetById(actualPet.Id);
        
            deletePetResponse.Should().Be(true);
            deletedPet.Should().BeNull();
            
            createdPetIds.Add(actualPet.Id);
        }
        
        public async ValueTask DisposeAsync()
        {
            foreach (var id in createdPetIds)
            {
                await client.DeletePet(id);
            }
        }

        #endregion
    }
}
