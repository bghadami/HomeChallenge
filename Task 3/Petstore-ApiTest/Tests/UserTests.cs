using System.Collections.Generic;
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

    public class UserTests : IAsyncDisposable
    {
        #region Fields

        private readonly PetStoreClient client;
        private IList<string> createdUsername;
        private static readonly User defaultUser;

        #endregion

        #region Constructors

        public UserTests()
        {
            createdUsername = new List<string>();
            PetStoreClient PetStoreClient = PetStoreClientConfiguration.GetPetStoreClientConfiguration();
            client = PetStoreClient ?? throw new ArgumentNullException(nameof(PetStoreClient));
        }

        static UserTests()
        {
            defaultUser = new User
            {
                Username = "theUser",
                FirstName = "John",
                LastName = "James",
                Email = "john@email.com",
                Password = "12345",
                Phone = "12345",
                UserStatus = 1
            };

        }

        #endregion

        #region Methods
        [Fact]
        public async Task AddUserTest()
        {
            User returnUser = await client.AddUser(defaultUser);
            returnUser.Should().BeEquivalentTo(defaultUser, options => options.Excluding(o=> o.Id));
            createdUsername.Add(returnUser.Username);
        }
        /// <summary>
        /// User tests cases should be implemented
        /// </summary>
        /// <returns></returns>
        #endregion

        #region IAsyncDisposable

        public async ValueTask DisposeAsync()
        {
            foreach (var username in createdUsername)
            {
                await client.DeleteUser(username);
            }
        }

        #endregion
    }
}
