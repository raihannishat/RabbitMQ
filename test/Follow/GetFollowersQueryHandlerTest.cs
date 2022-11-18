namespace Application.UnitTests.Follow;

public class GetFollowersQueryHandlerTest
{
    private readonly Mock<IFollowRepository> _followRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IBlockRepository> _blockRepositoryMock;
    private readonly Mock<ILogger<GetFollowersQueryHandler>> _loggerMock;
    private readonly Mock<PaginationQueryRequest> _request;
    private readonly IRequestHandler<GetFollowersQuery, Result<List<SearchedUserViewModel>>> _sut;

    public GetFollowersQueryHandlerTest()
    {
        _followRepositoryMock = new();
        _userRepositoryMock = new();
        _currentUserServiceMock = new();
        _blockRepositoryMock = new();
        _request = new();
        _loggerMock = new();

        _sut = new GetFollowersQueryHandler(
            _followRepositoryMock.Object,
            _userRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _blockRepositoryMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void GetFollowersQueryHandler_ShouldReturnNull_WhenTargetUserIsNull()
    {
        // Arrange
        var targetUserId = "8D7D4079-05D9-4F0E-8AA4-955332D4F55F";

        var query = new GetFollowersQuery(_request.Object);

        var cts = new CancellationTokenSource();

        _userRepositoryMock.Setup(x => x.FindByIdAsync(targetUserId))
            .ReturnsAsync(() => null!);

        // Act
        var result = _sut.Handle(query, cts.Token).Result;

        // Assert
        result.Should().BeNull();
    }
}
