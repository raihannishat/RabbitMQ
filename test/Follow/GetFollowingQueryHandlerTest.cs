namespace Application.UnitTests.Follow;

public class GetFollowingQueryHandlerTest
{
    private readonly Mock<IFollowRepository> _followRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IBlockRepository> _blockRepositoryMock;
    private readonly Mock<ILogger<GetFollowingQueryHandler>> _loggerMock;
    private readonly Mock<PaginationQueryRequest> _request;
    private readonly IRequestHandler<GetFollowingQuery, Result<List<SearchedUserViewModel>>> _sut;

    public GetFollowingQueryHandlerTest()
    {
        _followRepositoryMock = new();
        _userRepositoryMock = new();
        _currentUserServiceMock = new();
        _blockRepositoryMock = new();
        _request = new();
        _loggerMock = new();

        _sut = new GetFollowingQueryHandler(
            _followRepositoryMock.Object,
            _userRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _blockRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void GetFollowingQueryHandler_ShouldReturnNull_WhenTargetUserIsNull()
    {
        // Arrange
        var targetUserId = "F7AB3876-D8C5-42E7-A799-D7C1C32614D7";

        var query = new GetFollowingQuery(_request.Object);

        var cts = new CancellationTokenSource();

        _userRepositoryMock.Setup(x => x.FindByIdAsync(targetUserId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(query, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
