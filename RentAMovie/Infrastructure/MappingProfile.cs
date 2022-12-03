namespace RentAMovie.Infrastructure
{
    using AutoMapper;
    using RentAMovie.Data.Models;
    using RentAMovie.Models.PersonModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewTmdbSinglePersonModel, Actor>();
            CreateMap<ViewTmdbSinglePersonModel, Director>();
        }
    }
}
