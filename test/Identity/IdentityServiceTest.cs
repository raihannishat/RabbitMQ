using Application.IdentityManagement.Auth.Commands.SignUp;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.UnitTests.Identity;

public class IdentityServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAuthRepository> _authRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ITweetCacheService> _tweetCacheServiceMock;
    private readonly Mock<ILogger<IdentityService>> _loggerMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly IIdentityService _sut;
    private readonly Mock<string> _secretKeyMock;

    public IdentityServiceTest()
    {
        _userRepositoryMock = new();
        _authRepositoryMock = new();
        _mapperMock = new();
        _configurationMock = new();
        _jwtTokenServiceMock = new();
        _emailServiceMock = new();
        _tweetCacheServiceMock = new();
        _loggerMock = new();
        _currentUserServiceMock = new();
        _secretKeyMock = new();

        _sut = new IdentityService(
            _userRepositoryMock.Object,
            _authRepositoryMock.Object,
            _mapperMock.Object,
            _configurationMock.Object,
            _jwtTokenServiceMock.Object,
            _emailServiceMock.Object,
            _tweetCacheServiceMock.Object,
            _loggerMock.Object,
            _currentUserServiceMock.Object);
    }

    [Fact]
    public void Registration_ShouldReturnFailure_WhenUserEmailEmailIsExist()
    {
        // Arrange
        var email = "raihannishat.swe@gmail.com";

        var command = new SignUpCommand
        {
            Email = email
        };

        var user = new Domain.Entities.User();

        // _configurationMock.Setup(x => x.GetConnectionString("abc"))
        // .Returns("xyz");

        _userRepositoryMock.Setup(x => x.FindOneByMatchAsync(e => e.Email == email))
            .ReturnsAsync(user);

        // Act
        var result = _sut.Registration(command).Result;

        // Assert
        result.Error.Should().Match("Email is already used");
    }
}
