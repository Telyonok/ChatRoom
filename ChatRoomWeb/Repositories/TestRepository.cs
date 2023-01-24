using ChatRoomWeb.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Repositories
{
    public class TestRepository : RepositoryBase, ITestRepository
    {
        public TestRepository(IFlurlClientFactory flurlClientFactory, IHttpContextAccessor httpContextAccessor) : base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<string> ProtectedPing()
        {
				var text = await _flurlClient
				.Request("/api/protectedping")
				.GetJsonAsync<PingResponse>();
				return text.Response;
        }
    }
}
