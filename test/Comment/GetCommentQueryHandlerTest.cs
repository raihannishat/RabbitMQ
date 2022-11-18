namespace Application.UnitTests.Comment;

public class GetCommentQueryHandlerTest
{
    private readonly IRequestHandler<GetCommentQuery, Result<List<CommentViewModel>>> _sut;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ITweetRepository> _tweetRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IBlockRepository> _blockRepositoryMock;
    private readonly Mock<ILogger<GetCommentQueryHandler>> _loggerMock;
    private readonly Mock<PaginationQueryRequest> _paginationQueryRequestMock;

    public GetCommentQueryHandlerTest()
    {
        _mapperMock = new();
        _tweetRepositoryMock = new();
        _currentUserServiceMock = new();
        _userRepositoryMock = new();
        _commentRepositoryMock = new();
        _blockRepositoryMock = new();
        _loggerMock = new();
        _paginationQueryRequestMock = new();

        _sut = new GetCommentQueryHandler(
            _mapperMock.Object,
            _tweetRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _userRepositoryMock.Object,
            _commentRepositoryMock.Object,
            _blockRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void GetCommentQueryHandler_ShouldReturnNull_WhenTweetIsNull()
    {
        // Arrange
        var tweetId = "7A3E36AE-68F5-4756-B025-08F6305EF77D";

        var query = new GetCommentQuery(_paginationQueryRequestMock.Object, tweetId);

        var cts = new CancellationTokenSource();

        _tweetRepositoryMock.Setup(x => x.FindByIdAsync(tweetId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(query, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
