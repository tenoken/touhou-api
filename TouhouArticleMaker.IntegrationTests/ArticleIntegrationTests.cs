using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouhouAPI;
using TouhouArticleMaker.Domain;
using TouhouData;
using Xunit;

namespace TouhouArticleMaker.IntegrationTests
{
    
    public class ArticleIntegrationTests
    {
        private const string ARTICLE_CONTROLLER = "https://localhost:5001/Articles";
        private const string AUTHOR_CONTROLLER = "https://localhost:5001/Authors";

        private readonly HttpClient _httpClient;

        public ArticleIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetArticles_When_Given_AuthenticatedUser()
        {
            //Arrange

            //Act
            var result = await _httpClient.GetAsync(ARTICLE_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_PostArticle_When_Given_ValidArticle()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var author = new Author(new Name("Flandre", "Scartlet"), new Title(IntegrationTests.UserName), "F1@ndre$", new Email("flandre.scarlet@gensoukyo.com"), validation);
            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            article.SetAuthor(author);

            var json = JsonConvert.SerializeObject(article);
            var authorJson = JsonConvert.SerializeObject(author);

            //Act
            var authorResult = await _httpClient.PostAsync(AUTHOR_CONTROLLER, new StringContent(authorJson, Encoding.UTF8, "application/json"));
            var result = await _httpClient.PostAsync(ARTICLE_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Article>(content);

            //Assert
            Assert.NotNull(contentResult);
            Assert.True(contentResult.IsValid);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_EditArticle_When_Given_SuccessfulyPostedArticle()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var author = new Author(new Name("Flandre", "Scartlet"), new Title(IntegrationTests.UserName), "F1@ndre$", new Email("flandre.scarlet@gensoukyo.com"), validation);
            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            article.SetAuthor(author);

            var articleJson = JsonConvert.SerializeObject(article);
            var authorJson = JsonConvert.SerializeObject(author);            

            //Act
            var authorResult = await _httpClient.PostAsync(AUTHOR_CONTROLLER, new StringContent(authorJson, Encoding.UTF8, "application/json"));
            var articleResult = await _httpClient.PostAsync(ARTICLE_CONTROLLER, new StringContent(articleJson, Encoding.UTF8, "application/json"));
            var putArticleResult = await _httpClient.PutAsync(ARTICLE_CONTROLLER +"/"+ article.Id, new StringContent(articleJson, Encoding.UTF8, "application/json"));
            var result = await _httpClient.GetAsync(ARTICLE_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<List<Article>>(content);

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            //Assert.Single(contentResult);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_DeleteArticle_When_Given_SuccessfulyPostedArticle()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var author = new Author(new Name("Flandre", "Scartlet"), new Title(IntegrationTests.UserName), "F1@ndre$", new Email("flandre.scarlet@gensoukyo.com"), validation);
            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            article.SetAuthor(author);

            var articleJson = JsonConvert.SerializeObject(article);
            var authorJson = JsonConvert.SerializeObject(author);

            //Act
            var authorResult = await _httpClient.PostAsync(AUTHOR_CONTROLLER, new StringContent(authorJson, Encoding.UTF8, "application/json"));
            var articleResult = await _httpClient.PostAsync(ARTICLE_CONTROLLER, new StringContent(articleJson, Encoding.UTF8, "application/json"));
            var deleteArticleResult = await _httpClient.DeleteAsync(ARTICLE_CONTROLLER + "/" + article.Id);
            var result = await _httpClient.GetAsync(ARTICLE_CONTROLLER + "/" + article.Id);
            var content = await result.Content.ReadAsStringAsync();

            //Assert         
            Assert.NotNull(content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_GetArticle_When_Given_SuccessfulyPostedArticle()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var author = new Author(new Name("Flandre", "Scartlet"), new Title(IntegrationTests.UserName), "F1@ndre$", new Email("flandre.scarlet@gensoukyo.com"), validation);
            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            article.SetAuthor(author);

            var articleJson = JsonConvert.SerializeObject(article);
            var authorJson = JsonConvert.SerializeObject(author);

            //Act
            var authorResult = await _httpClient.PostAsync(AUTHOR_CONTROLLER, new StringContent(authorJson, Encoding.UTF8, "application/json"));
            var articleResult = await _httpClient.PostAsync(ARTICLE_CONTROLLER, new StringContent(articleJson, Encoding.UTF8, "application/json"));
            var result = await _httpClient.GetAsync(ARTICLE_CONTROLLER+"/"+article.Id);
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Article>(content);

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.IsType<Article>(contentResult);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
