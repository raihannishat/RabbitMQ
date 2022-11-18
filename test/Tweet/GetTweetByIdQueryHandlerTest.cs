using Application.Likes.Shared.Interfaces;
using Application.Retweets.Shared.Interfaces;
using Application.Tweets.Queries.GetTweetById;
using Application.Tweets.Shared.Models;

namespace Application.UnitTests.Tweet;

public class GetTweetByIdQueryHandlerTest
{
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILikeRepository> _likeRepositoryMock;
    private readonly Mock<IRetweetRepository> _retweetRepositoryMock;
    private readonly Mock<ILogger<GetTweetByIdQueryHandler>> _loggerMock;
    private readonly IRequestHandler<GetTweetByIdQuery, Result<TweetViewModel>> _sut;

    public GetTweetByIdQueryHandlerTest()
    {
        _currentUserServiceMock = new();
        _tweetRepositoryMock = new();
        _userRepositoryMock = new();
        _likeRepositoryMock = new();
        _retweetRepositoryMock = new();
        _loggerMock = new();

        _sut = new GetTweetByIdQueryHandler(
            _currentUserServiceMock.Object,
            _tweetRepositoryMock.Object,
            _userRepositoryMock.Object,
            _likeRepositoryMock.Object,
            _retweetRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void GetTweetByIdQueryHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "C5EC84AB-4581-40F5-A6C2-346D44F8106C";
        var tewwtOwnerId = "0A9C63E4-98B4-47E6-A17C-660D36BD032E";

        var command = new GetTweetByIdQuery(tweetId, tewwtOwnerId);

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
