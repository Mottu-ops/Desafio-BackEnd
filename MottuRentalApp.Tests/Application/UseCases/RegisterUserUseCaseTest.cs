using Moq;
using FluentAssertions;
using MottuRentalApp.Application.UseCases;
using MottuRentalApp.Application.Ports;
using MottuRentalApp.Domain;

namespace MottuRentalApp.Tests
{
  public class RegisterUserUseCaseTest
  {
    private readonly Mock<IUsersPort> _usersPort = new Mock<IUsersPort>();
    private RegisterUserUseCase _underTest;

    public RegisterUserUseCaseTest()
    {
      this._underTest = new RegisterUserUseCase(this._usersPort.Object);
    }

    [Fact]
    public void ShouldExecuteRegisterUserSuccessfully()
    {
      IList<Document> validDocuments = [
        new Document() {
          UserId = Guid.NewGuid().ToString(),
          Number = "721878912383",
          Type = DocumentType.CNPJ
        }
      ];
      RegisterUserDto userDto = new RegisterUserDto("Caio Saldanha", "29/11/1992", 2, validDocuments);
      User expectedUser = new User(userDto.Name, userDto.BirthDate, (UserType) userDto.Type, userDto.Documents);
      this._usersPort.Setup(port => port.SaveUser(It.IsAny<User>())).Returns(expectedUser);

      User result = this._underTest.Execute(userDto);

      Assert.NotNull(result);
    }

    [Fact]
    public void ShouldThrowWhenInvalidDto()
    {
      RegisterUserDto userDto = new RegisterUserDto("", "29/11/1992", 2, []);
      this._usersPort.Setup(port => port.SaveUser(It.IsAny<User>())).Returns(It.IsAny<User>());

      Assert.Throws<ArgumentException>(() => this._underTest.Execute(userDto));
    }
  }
}
