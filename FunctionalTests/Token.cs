using xpanse.sdk;
using xpanse.sdk.Models;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Environment = xpanse.sdk.Environment;

namespace FunctionalTests
{
    public class Token : BaseTest
    {
        [Fact]
        public void Single()
        {
            var svc = new xpanse.sdk.Token();
            var tokenId = GetPaymentToken();

            var token = svc.Single(tokenId);

            Assert.Equal(tokenId, token.TokenId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var svc = new xpanse.sdk.Token();
            var tokenId = GetPaymentToken();

            var token = await svc.SingleAsync(tokenId);

            Assert.Equal(tokenId, token.TokenId);
        }

        [Fact]
        public void Search()
        {
            var svc = new xpanse.sdk.Token();
            var tokenSearch = new TokenSearch
            {
                Limit = 10
            };

            var tokenList = svc.Search(tokenSearch);

            Assert.Equal(tokenSearch.Limit, tokenList.Limit);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new xpanse.sdk.Token();
            var tokenSearch = new TokenSearch
            {
                Limit = 10
            };

            var tokenList = await svc.SearchAsync(tokenSearch);

            Assert.Equal(tokenSearch.Limit, tokenList.Limit);
        }
    }
}
