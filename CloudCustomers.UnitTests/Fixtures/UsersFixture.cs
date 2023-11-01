

using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
           new User
            {
                Id = 1,
                Name = "Jane",
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Madison",
                    ZipCode = "53704"
                },
                Email = "jane@example.com"
            },
           new User
            {
                Id = 2,
                Name = "John",
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Madison",
                    ZipCode = "53704"
                },
                Email = "joe@example.com"
            },
        };
    }
}
