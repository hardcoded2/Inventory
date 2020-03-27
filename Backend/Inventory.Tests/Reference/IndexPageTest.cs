using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

//roughly based on docs from https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1
namespace WebApplication.Tests
{
    public class IndexPageTests :
        IClassFixture<WebApplicationFactorySetup<Startup>>
    {
        private readonly HttpClient _client;

        private readonly WebApplicationFactorySetup<Startup>
            _factory;

        public IndexPageTests(
            WebApplicationFactorySetup<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        
        [Fact]
        public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/");
            
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act on initial webpage items based on searching for elements
            var response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }
    }
}
