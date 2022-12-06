namespace RentAMovie.Test.Mocks
{
    using Moq;
    using RentAMovie.Services.Home;

    public static class HomeServiceMock
    {
        public static IHomeService Instance 
        {
            get
            {
                var homeServiceMock = new Mock<IHomeService>();

                homeServiceMock
                        .Setup(m => m.GetReviewCount())
                        .Returns(15);
                homeServiceMock
                        .Setup(m => m.GetMovieCount())
                        .Returns(10);
                homeServiceMock
                        .Setup(m => m.GetActorCount())
                        .Returns(5);
                homeServiceMock
                        .Setup(m => m.GetCastCount())
                        .Returns(3);
                homeServiceMock
                        .Setup(m => m.GetUserCount())
                        .Returns(1);

                return homeServiceMock.Object;
            }
        }
    }
}
