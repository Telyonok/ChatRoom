using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Services
{
    public interface ITestService
    {
        public Task<string> ProtectedPing();
    }
}
