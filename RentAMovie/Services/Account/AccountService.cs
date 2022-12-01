namespace RentAMovie.Services.Account
{
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using System.Collections.Generic;

    public class AccountService : IAccountService
    {
        private readonly ViewMoviesDbContext data;

        public AccountService(ViewMoviesDbContext data)
        {
            this.data = data;
        }

        public User GetUser(string id)
        {
            return data.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public List<ViewUserModel> GetAllUsers()
        {
            return data.Users
                .OrderByDescending(u => u.DateCreated)
                .Select(u => new ViewUserModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    DateCreated = u.DateCreated,
                    Photo = u.Photo
                })
                .ToList();
        }

        public void RemoveUser(User user)
        {
            data.Users.Remove(user);
            data.SaveChanges();
        }
    }
}
