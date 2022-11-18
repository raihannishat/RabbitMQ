namespace Application.UnitTests.Follow;

public class FollowUserCommandHandlerTest
{
    private readonly IRequestHandler<FollowUserCommand, Result<Unit>> _sut;
    private readonly Mock<IFollowRepository> _followRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<IHomeTimelineRepository> _homeTimelineRepositorymock;
    private readonly Mock<ILogger<FollowUserCommandHandler>> _loggerMock;

    public FollowUserCommandHandlerTest()
    {
        _followRepositoryMock = new();
        _currentUserServiceMock = new();
        _userRepositoryMock = new();
        _tweetCacheServiceMock = new();
        _tweetRepositoryMock = new();
        _homeTimelineRepositorymock = new();
        _loggerMock = new();

        _sut = new FollowUserCommandHandler(
            _followRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _userRepositoryMock.Object,
            _tweetCacheServiceMock.Object,
            _tweetRepositoryMock.Object,
            _homeTimelineRepositorymock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void FollowUserCommandHandler_ShouldReturnNull_WhenIserIdAndTargetUserAreSame()
    {
        // Arrange
        var targetUserId = "CAB7621F-D1D4-4BB2-A744-A2B1E4AFF3C6";

        var userId = new string(targetUserId);

        var command = new FollowUserCommand(targetUserId);

        var cts = new CancellationTokenSource();

        _currentUserServiceMock.Setup(x => x.UserId)
            .Returns(userId);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void FollowUserCommandHandler_ShouldReturnNull_WhenTargetUserIsNull()
    {
        // Arrange
        var targetUserId = "652BBA00-3963-48C2-972D-A10D0608C591";

        var command = new FollowUserCommand(targetUserId);

        var cts = new CancellationTokenSource();

        _userRepositoryMock.Setup(x => x.FindByIdAsync(targetUserId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
