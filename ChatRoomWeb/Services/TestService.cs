using ChatRoomWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomWeb.Services
{
    public class TestService: ITestService
    {
        public ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<string> ProtectedPing()
        {
            return await _testRepository.ProtectedPing();
        }
    }
}
