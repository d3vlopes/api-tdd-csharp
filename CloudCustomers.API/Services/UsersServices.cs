using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services
{
    public interface IUsersService {
        public Task<List<User>> GetAllUsers();
    }

    public class UsersServices: IUsersService
    {
        private readonly HttpClient _httpClient;

        public UsersServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync("https://example.com");

            return new List<User> { };
        }
    }
}
