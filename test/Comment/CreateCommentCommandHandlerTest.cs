namespace Application.UnitTests.Comment;

public class CreateCommentCommandHandlerTest
{
    private readonly IRequestHandler<CreateCommentCommand, Result<Unit>> _sut;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<INotificationRepository> _notificationRepositoryMock;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<ILogger<CreateCommentCommandHandler>> _loggerMock;

    public CreateCommentCommandHandlerTest()
    {
        _tweetRepositoryMock = new();
        _mapperMock = new();
        _currentUserServiceMock = new();
        _tweetCacheServiceMock = new();
        _notificationRepositoryMock = new();
        _commentRepositoryMock = new();
        _loggerMock = new();

        _sut = new CreateCommentCommandHandler(
            _mapperMock.Object,
            _currentUserServiceMock.Object,
            _tweetRepositoryMock.Object,
            _tweetCacheServiceMock.Object,
            _notificationRepositoryMock.Object,
            _commentRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void CreateCommentCommandHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "abxd-fghk";

        var command = new CreateCommentCommand
        {
            TweetId = tweetId
        };

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(command, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
