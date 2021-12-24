using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TouhouArticleMaker.Domain;
using Xunit;

namespace TouhouArticleMaker.IntegrationTests
{
    public class PhotoIntegrationTests
    {
        private const string PHOTO_CONTROLLER = "https://localhost:5001/photos";

        private HttpClient _httpClient;

        public PhotoIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetPhoto_WhenGiven_AuthenticatedUser()
        {
            //Arrange

            //Act
            var result = await _httpClient.GetAsync(PHOTO_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_PostPhoto_WhenGiven_ValidSection()
        {
            //Arrange
            var validation = new Shared.EntityValidation();
            var photo = new Photo(new Title("Back Cover"), new URL(@"https://en.touhouwiki.net/images/thumb/2/20/Th06backcover.jpg/120px-Th06backcover.jpg"),"",validation);
            var json = JsonConvert.SerializeObject(photo);

            //Act
            var result = await _httpClient.PostAsync(PHOTO_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Photo>(content);

            //Assert
            Assert.True(contentResult.IsValid);
            Assert.True(result.IsSuccessStatusCode);
            Assert.NotNull(contentResult);
        }
    }
}
