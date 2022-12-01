namespace RentAMovie.Services.Account
{
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data.Models;

    public interface IAccountService
    {
        User GetUser(string id);

        List<ViewUserModel> GetAllUsers();

        public void RemoveUser(User user);
    }
}
