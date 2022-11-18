using Application.Likes.Shared.Interfaces;
using Application.Retweets.Shared.Interfaces;
using Application.Tweets.Queries.GetHashtagTweets;
using Application.Tweets.Shared.Models;

namespace Application.UnitTests.Tweet;

public class GetHashtagTweetsQueryHandlerTest
{
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IBlockRepository> _blockRepositoryMock;
    private readonly Mock<IHashtagRepository> _hashtagRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILikeRepository> _likeRepositoryMock;
    private readonly Mock<IRetweetRepository> _retweetRepositoryMock;
    private readonly Mock<ILogger<GetHashtagTweetsQueryHandler>> _loggerMock;
    private readonly Mock<PaginationQueryRequest> _request;
    private readonly IRequestHandler<GetHashtagTweetsQuery, Result<List<TweetViewModel>>> _sut;

    public GetHashtagTweetsQueryHandlerTest()
    {
        _tweetCacheServiceMock = new();
        _mapperMock = new();
        _tweetRepositoryMock = new();
        _currentUserServiceMock = new();
        _blockRepositoryMock = new();
        _hashtagRepositoryMock = new();
        _userRepositoryMock = new();
        _likeRepositoryMock = new();
        _retweetRepositoryMock = new();
        _loggerMock = new();
        _request = new();

        _sut = new GetHashtagTweetsQueryHandler(
            _tweetCacheServiceMock.Object,
            _mapperMock.Object,
            _tweetRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _blockRepositoryMock.Object,
            _hashtagRepositoryMock.Object,
            _userRepositoryMock.Object,
            _likeRepositoryMock.Object,
            _retweetRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void GetHashtagTweetsQueryHandler_ShouldReturnNull_WhenHashTagEmpty()
    {
        // Arrange
        var query = new GetHashtagTweetsQuery(
            new PaginationQueryRequest { Keyword = string.Empty });

        var cts = new CancellationTokenSource();

        // Act
        var result = _sut.Handle(query, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetHashtagTweetsQueryHandler_ShouldReturnNull_WhenHashTagNull()
    {
        // Arrange
        var query = new GetHashtagTweetsQuery(_request.Object);

        var cts = new CancellationTokenSource();

        // Act
        var result = _sut.Handle(query, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
