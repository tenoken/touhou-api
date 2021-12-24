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
    public class AuthorIntegrationTests
    {
        private const string AUTHOR_CONTROLLER = "https://localhost:5001/authors";

        private HttpClient _httpClient;

        public AuthorIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetAuthor_WhenGiven_AuthenticatedUser()
        {
            //Arrange 

            //Act
            var result = await _httpClient.GetAsync(AUTHOR_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Should_PostAuthor_WhenGiven_ValidSection()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            //var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            var author = new Author(new Name("Flandre", "Scartlet"), new Title("Youkai"), "F1@ndre$", new Email("flandre.scarlet@gensoukyo.com"), validation);
            //var section = new Section(new Title("Gameplay"), new Text("This is a content about gameplay section."), validation);
            //article.AddSection(section);

            var json = JsonConvert.SerializeObject(author);
            //Act
            var result = await _httpClient.PostAsync(AUTHOR_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Author>(content);

            //Assert
            Assert.NotNull(contentResult);
            Assert.True(contentResult.IsValid);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
