using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Repositories
{
    public interface ITestRepository
    {
        public Task<string> ProtectedPing();
    }
}
