namespace Application.UnitTests.Comment;

public class DeleteCommentCommandHandlerTest
{
    private readonly IRequestHandler<DeleteCommentCommand, Result<Unit>> _sut;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<ILogger<DeleteCommentCommandHandler>> _loggerMock;

    public DeleteCommentCommandHandlerTest()
    {
        _tweetRepositoryMock = new();
        _mapperMock = new();
        _currentUserServiceMock = new();
        _tweetCacheServiceMock = new();
        _commentRepositoryMock = new();
        _loggerMock = new();

        _sut = new DeleteCommentCommandHandler(
            _tweetRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _tweetCacheServiceMock.Object,
            _mapperMock.Object,
            _commentRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void DeleteCommentCommandHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "abxd-fghk";

        var command = new DeleteCommentCommand
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
