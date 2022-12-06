namespace RentAMovie.Test.Mocks
{
    using AutoMapper;
    using Moq;

    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperMock = new Mock<IMapper>();
                return mapperMock.Object;
            }
        }
    }
}
