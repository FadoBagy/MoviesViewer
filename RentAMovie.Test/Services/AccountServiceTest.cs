namespace RentAMovie.Test.Services
{
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data.Models;
    using RentAMovie.Services.Account;
    using RentAMovie.Test.Mocks;

    public class AccountServiceTest
    {
        private readonly string testUserId = "TestUserId";

        [Fact]
        public void GetUserShouldReturnUser()
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = testUserId
            });
            data.SaveChanges();
            var accountService = new AccountService(data, null);

            var result = accountService.GetUser(testUserId);

            Assert.NotNull(result);
            Assert.IsType<User>(result);
        }

        [Fact]
        public void GetAllUsersShouldReturnListOfUsers() 
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = testUserId
            });
            data.SaveChanges();
            var accountService = new AccountService(data, MapperMock.Instance);

            var result = accountService.GetAllUsers();

            Assert.NotNull(result);
            Assert.IsType<List<ViewUserModel>>(result);
        }

        [Fact]
        public void RemoveUserShouldRemoveUser()
        {
            using var data = DatabaseMock.Instance;
            data.Users.Add(new User
            {
                Id = testUserId
            });
            data.SaveChanges();
            var accountService = new AccountService(data, null);

            accountService.RemoveUser(data.Users.FirstOrDefault());

            Assert.Equal(0, data.Users.Count());
        }
    }
}
