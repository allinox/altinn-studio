using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Altinn.Common.PEP.Interfaces;
using Altinn.Platform.Storage.IntegrationTest.Mocks;
using Altinn.Platform.Storage.IntegrationTest.Mocks.Authentication;
using Altinn.Platform.Storage.IntegrationTest.Utils;
using Altinn.Platform.Storage.Interface.Models;
using Altinn.Platform.Storage.Repository;
using AltinnCore.Authentication.JwtCookie;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Altinn.Platform.Storage.IntegrationTest.TestingControllers
{
    [Collection("Sequential")]
    public class InstancesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string BasePath = "storage/api/v1/instances";

        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Mock<IInstanceRepository> _instanceRepository;

        public InstancesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _instanceRepository = new Mock<IInstanceRepository>();
        }

        /// <summary>
        /// Test case: User has to low authentication level. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Get_UserHasTooLowAuthLv_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(1, 0);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.GetAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: Response is deny. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Get_ReponseIsDeny_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.GetAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: User has to low authentication level. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Post_UserHasTooLowAuthLv_ReturnsStatusForbidden()
        {
            // Arrange
            string appId = "1";
            string requestUri = $"{BasePath}?appId={appId}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(1, 0);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Instance instance = new Instance();

            // Act
            HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: Response is deny. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Post_ReponseIsDeny_ReturnsStatusForbidden()
        {
            // Arrange
            string appId = "1";
            string requestUri = $"{BasePath}?appId={appId}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Instance instance = new Instance();

            // Act
            HttpResponseMessage response = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: User has to low authentication level. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Put_UserHasTooLowAuthLv_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(1, 0);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Instance instance = new Instance();

            // Act
            HttpResponseMessage response = await client.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: Response is deny. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Put_ReponseIsDeny_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Instance instance = new Instance();

            // Act
            HttpResponseMessage response = await client.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(instance), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: User has to low authentication level. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Delete_UserHasTooLowAuthLv_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(1, 0);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        /// <summary>
        /// Test case: Response is deny. 
        /// Expected: Returns status forbidden.
        /// </summary>
        [Fact]
        public async void Delete_ReponseIsDeny_ReturnsStatusForbidden()
        {
            // Arrange
            int instanceOwnerPartyId = 1;
            string instanceGuid = "cbdb00b1-4134-490d-b02b-3e33f7d8da33";
            string requestUri = $"{BasePath}/{instanceOwnerPartyId}/{instanceGuid}";

            HttpClient client = GetTestClient(_instanceRepository.Object);
            string token = PrincipalUtil.GetToken(2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            HttpResponseMessage response = await client.DeleteAsync(requestUri);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        private HttpClient GetTestClient(IInstanceRepository instanceRepository)
        {
            // No setup required for these services. They are not in use by the ApplicationController
            Mock<IApplicationRepository> applicationRepository = new Mock<IApplicationRepository>();
            Mock<IDataRepository> dataRepository = new Mock<IDataRepository>();
            Mock<IInstanceEventRepository> instanceEventRepository = new Mock<IInstanceEventRepository>();

            HttpClient client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(applicationRepository.Object);
                    services.AddSingleton(dataRepository.Object);
                    services.AddSingleton(instanceEventRepository.Object);
                    services.AddSingleton(instanceRepository);
                    services.AddSingleton<IPDP, PDPMock>();
                    services.AddSingleton<ISigningKeysRetriever, SigningKeysRetrieverStub>();
                    services.AddSingleton<IPostConfigureOptions<JwtCookieOptions>, JwtCookiePostConfigureOptionsStub>();
                });
            }).CreateClient();

            return client;
        }
    }
}