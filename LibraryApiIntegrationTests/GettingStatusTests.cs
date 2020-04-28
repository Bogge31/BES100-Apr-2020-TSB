using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingStatusTests : IClassFixture<WebTestFixture>
    {
        private readonly HttpClient client;

        public GettingStatusTests(WebTestFixture factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task WeGetAnOkStatusCode()
        {
            var response = await client.GetAsync("/status");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WeGetSomeJsonDataBack()
        {
            var response = await client.GetAsync("/status");
            var contentType = response.Content.Headers.ContentType.MediaType;
            Assert.Equal("application/json", contentType);
        }

        [Fact]
        public async Task ReturnsProperResponse()
        {
            var response = await client.GetAsync("/status");

            var content = await response.Content.ReadAsAsync<StatusResponse>();

            Assert.Equal("Everything is golden!", content.message);
            Assert.Equal("Joe Schmidt", content.checkedBy);
            Assert.Equal(new DateTime(1969, 4, 20, 23, 59, 59), content.whenLastChecked);
        }
    }

    public class StatusResponse
    {
        public string message { get; set; }
        public string checkedBy { get; set; }
        public DateTime whenLastChecked { get; set; }
    }
}
