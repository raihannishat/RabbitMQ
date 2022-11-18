using Application.Tweets.Commands.DeleteTweet;

namespace Application.UnitTests.Tweet;

public class DeleteTweetCommandHandlerTest
{
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<IUserTimelineRepository> _userTimelineRepositoryMock;
    private readonly Mock<ILogger<DeleteTweetCommandHandler>> _loggerMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IRequestHandler<DeleteTweetCommand, Result<Unit>> _sut;

    public DeleteTweetCommandHandlerTest()
    {
        _tweetRepositoryMock = new();
        _userTimelineRepositoryMock = new();
        _loggerMock = new();
        _tweetCacheServiceMock = new();
        _currentUserServiceMock = new();
        _mapperMock = new();

        _sut = new DeleteTweetCommandHandler(
            _mapperMock.Object,
            _currentUserServiceMock.Object,
            _tweetRepositoryMock.Object,
            _tweetCacheServiceMock.Object,
            _userTimelineRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void DeleteTweetCommandHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "E5EE35D0-F804-41CB-9D34-2324FDD240DB";

        var command = new DeleteTweetCommand(tweetId);

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void DeleteTweetCommandHandler_ShouldReturnNull_WhenUserIdIsNotEqual()
    {
        // Arrange
        var tweetUserId = "2A1CE2B4-AE34-4BFC-ACEC-CBA625062447";

        var currentUserId = "DD72C73D-7E41-44BA-8970-2D25FBB3D337";

        var tweet = new Domain.Entities.Tweet
        {
            UserId = tweetUserId
        };

        var command = new DeleteTweetCommand(tweetUserId);

        var cts = new CancellationTokenSource();

        _currentUserServiceMock.Setup(x => x.UserId)
            .Returns(currentUserId);

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetUserId))
            .ReturnsAsync(tweet);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
