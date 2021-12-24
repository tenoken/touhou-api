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
    public class SectionIntegrationTests
    {
        private const string SECTION_CONTROLLER = "https://localhost:5001/sections";

        private HttpClient _httpClient;

        public SectionIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetSections_WhenGiven_AuthenticatedUser()
        {
            //Arrange 

            //Act
            var result = await _httpClient.GetAsync(SECTION_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(content);
            //Assert.NotEmpty(content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_PostSection_WhenGiven_ValidSection() 
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            var section = new Section(new Title("Gameplay"), new Text("This is a content about gameplay section."), validation);
            article.AddSection(section);

            var json = JsonConvert.SerializeObject(section);
            //Act
            var result = await _httpClient.PostAsync(SECTION_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Section>(content);

            //Assert
            Assert.NotNull(contentResult);
            Assert.True(contentResult.IsValid);
            Assert.True(result.IsSuccessStatusCode);
        }

    }
}
