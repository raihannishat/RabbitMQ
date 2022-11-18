using Application.Retweets.Commands;
using Application.Retweets.Shared.Interfaces;

namespace Application.UnitTests.Retweet;

public class RetweetCommandHandlerTest
{
    private readonly Mock<IRetweetRepository> _retweetRepositoryMock;
    private readonly Mock<IUserTimelineRepository> _userTimelineRepositoryMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<INotificationRepository> _notificationRepositoryMock;
    private readonly Mock<ILogger<RetweetCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IRequestHandler<RetweetCommand, Result<Unit>> _sut;

    public RetweetCommandHandlerTest()
    {
        _retweetRepositoryMock = new();
        _userTimelineRepositoryMock = new();
        _tweetRepositoryMock = new();
        _currentUserServiceMock = new();
        _tweetCacheServiceMock = new();
        _notificationRepositoryMock = new();
        _loggerMock = new();
        _mapperMock = new();

        _sut = new RetweetCommandHandler(
            _retweetRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _tweetCacheServiceMock.Object,
            _tweetRepositoryMock.Object,
            _userTimelineRepositoryMock.Object,
            _mapperMock.Object,
            _notificationRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void RetweetCommandHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "A8F6A51C-3475-4A7B-A5CE-8E3D8B2277E0";

        var command = new RetweetCommand();

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
