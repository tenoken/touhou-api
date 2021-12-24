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
    public class GalleryIntegrationTests
    {
        private const string GALLERY_CONTROLLER = "https://localhost:5001/galleries";

        private HttpClient _httpClient;

        public GalleryIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetGallery_WhenGiven_AuthenticatedUser()
        {
            //Arrange

            //Act
            var result = await _httpClient.GetAsync(GALLERY_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<List<Gallery>>(content);

            //Assert
            Assert.NotNull(content);
            //Assert.NotEmpty(content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_PostGallery_WhenGiven_ValidSection()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            var gallery = new Gallery(validation);
            article.AddGallery(gallery);

            var json = JsonConvert.SerializeObject(gallery);

            //Act
            var result = await _httpClient.PostAsync(GALLERY_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Gallery>(content);

            //Assert
            Assert.NotNull(contentResult);
            Assert.True(contentResult.IsValid);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
