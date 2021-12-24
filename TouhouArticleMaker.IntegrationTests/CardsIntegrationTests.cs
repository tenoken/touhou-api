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
    public class CardsIntegrationTests
    {
        private const string CARD_CONTROLLER = "https://localhost:5001/cards";

        private HttpClient _httpClient;

        public CardsIntegrationTests()
        {
            _httpClient = IntegrationTests.DoSetup().Result;
        }

        [Fact]
        public async Task Should_GetCards_WhenGiven_AuthenticatedUser()
        {
            //Arrange

            //Act
            var result = await _httpClient.GetAsync(CARD_CONTROLLER);
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task Should_PostSection_WhenGiven_ValidCard()
        {
            //Arrange
            var validation = new Shared.EntityValidation();

            var article = new Article(new Title("The Embodiment of Scarlet Devil"), new Intro("This is an intro about Scarlet Devil"), ECategory.Games, validation);
            var card = new Card(
                    new Title("The Embodiment of Scarlet Devil"),//東方紅魔郷とうほうこうまきょう 
                    new Title("Team Shanghai Alice"),
                    new Title("Team Shanghai Alice"),
                    DateTime.Now,
                    new Title("Vertical Danmaku Shooting Game"),
                    new Title("Single-Player Story Mode"),
                    new Title("Windows 98/SE/ME/2000/XP"),
                    new Title("Pentium 500MHz;320MB hard disk;Direct3D;DirectX 8;4MB VRAM;DirectSound;SC - 88Pro(if MIDI is selected);32MB RAM"),
                    validation
                );
            article.AddCard(card);
            var json = JsonConvert.SerializeObject(card);

            //Act
            var result = await _httpClient.PostAsync(CARD_CONTROLLER, new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await result.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<Card>(content);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(contentResult.IsValid);
            Assert.NotNull(contentResult);
        }
    }
}
