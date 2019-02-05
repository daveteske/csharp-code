using Xunit;
using HttpClientFactoryExample;
using Moq;
using System.Threading.Tasks;

namespace Shared.Tests
{
    public class HttpClientFactoryExample
    {
        [Fact]
        public async Task EncapsulatedHttpClientReturnsString()
        {
            var clientReturnString = "Dave was here";

            var client = new Mock<IEncapsulatedWikiClient>();
            client.Setup(x => x.GetAltaVistaArticle()).Returns(Task.FromResult(clientReturnString));

            var webpageString = await client.Object.GetAltaVistaArticle(); 

            Assert.NotNull(webpageString);
            Assert.Equal("Dave was here", clientReturnString);
        }
    }
}
