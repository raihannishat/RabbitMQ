using Application.Likes.Commands;
using Application.Likes.Shared.Interfaces;
using Application.Retweets.Shared.Interfaces;

namespace Application.UnitTests.Like;

public class LikeCommandHandlerTest
{
    private readonly Mock<ILikeRepository> _likeRepositoryMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<INotificationRepository> _notificationRepositoryMock;
    private readonly Mock<IRetweetRepository> _retweetRepositoryMock;
    private readonly Mock<ILogger<LikeCommandHandler>> _loggerMock;
    private readonly IRequestHandler<LikeCommand, Result<Unit>> _sut;

    public LikeCommandHandlerTest()
    {
        _likeRepositoryMock = new();
        _tweetRepositoryMock = new();
        _currentUserServiceMock = new();
        _tweetCacheServiceMock = new();
        _notificationRepositoryMock = new();
        _retweetRepositoryMock = new();
        _loggerMock = new();

        _sut = new LikeCommandHandler(
            _likeRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _tweetCacheServiceMock.Object,
            _tweetRepositoryMock.Object,
            _notificationRepositoryMock.Object,
            _retweetRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void LikeCommandHandler_ShouldReturnNull_WhenTweeterIsNull()
    {
        // Arrange
        var tweeetId = "F308BE61-2192-456A-969A-B55AA83B4160";

        var command = new LikeCommand();

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweeetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
