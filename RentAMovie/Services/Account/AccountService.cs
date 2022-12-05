namespace RentAMovie.Services.Account
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data;
    using RentAMovie.Data.Models;
    using System.Collections.Generic;

    public class AccountService : IAccountService
    {
        private readonly ViewMoviesDbContext data;
        private readonly IMapper mapper;

        public AccountService(ViewMoviesDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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
                .ProjectTo<ViewUserModel>(mapper.ConfigurationProvider)
                .ToList();
        }

        public void RemoveUser(User user)
        {
            data.Users.Remove(user);
            data.SaveChanges();
        }
    }
}
