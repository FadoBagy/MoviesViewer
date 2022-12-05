namespace RentAMovie.Infrastructure
{
    using AutoMapper;
    using RentAMovie.Areas.Admin.Models.Account;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewTmdbSinglePersonModel, Actor>();
            CreateMap<ViewTmdbSinglePersonModel, Director>();

            CreateMap<User, ViewUserModel>()
                .ForMember(u => u.Username, cfg => cfg.MapFrom(u => u.UserName));
        }
    }
}
