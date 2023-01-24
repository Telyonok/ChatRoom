using Flurl.Http;
using Flurl.Http.Configuration;

namespace ChatRoomWeb.Repositories
{
    public class RepositoryBase
    {
        protected readonly IFlurlClient _flurlClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public RepositoryBase(IFlurlClientFactory flurlClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _flurlClient = flurlClientFactory.Get("https://localhost:7158");

            _flurlClient.BeforeCall(flurlCall =>
            {
                var token = _httpContextAccessor.HttpContext.Request.Cookies["X-Access-Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    flurlCall.HttpRequestMessage.SetHeader("Authorization", $"bearer {token}");
                }
                else
                {
                    flurlCall.HttpRequestMessage.SetHeader("Authorization", string.Empty);
                }
            });
        }
    }
}
