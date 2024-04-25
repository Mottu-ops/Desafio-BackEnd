using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Order.Tests.Service.Configurations;
using User.Core.Execeptions;
using User.Domain.Entities;
using User.Infra.interfaces;
using User.Service.Bundles;
using User.Service.DTO;
using User.Service.Interfaces;

namespace Order.Tests.Service;

public class ServiceTest
{
    private readonly IPartnerServices _sut;
    private readonly IMapper _mapper;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    Bogus.Faker<PartnerDto> partnerDtoBogus = new Bogus.Faker<PartnerDto>();
    public ServiceTest()
    {
        _mapper = AutoMapperConfiguration.GetConfig();
        _userRepositoryMock = new Mock<IUserRepository>();

        _sut = new PartnerServices(
            mapper: _mapper,
            userRepository: _userRepositoryMock.Object
            );

         partnerDtoBogus.CustomInstantiator(faker => new PartnerDto
        {
            Id = faker.UniqueIndex,
            Name = faker.Person.FullName,
            Email = faker.Person.Email,
            Password = faker.Person.FullName,
            Cnpj = faker.Company.Cnpj(),
            CnhImage = faker.Image.PlaceImgUrl(4, 5, "any"),
            DateBirth = faker.Person.DateOfBirth,
            CnhType = "AB",
            CnhNumber = "123456789",
            Role = "admin", 
        });
    }
    [Fact(DisplayName = "Create Valid User")]
    public async void ShouldCreateAValidDTOUserAndReturnsAValidData() {
        //Arranges
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => null);
        _userRepositoryMock.Setup(x => x.Create(It.IsAny<Partner>())).ReturnsAsync(() => partner);

        //Act
        var result = await _sut.Create(partnerDto);
        
        //Assert
        result.Should().BeEquivalentTo(partnerDto);
    }

    [Fact(DisplayName = "User creating exception ")]
    public void ShouldNotCreateAValidDTOUserAndReturnsAValidData() {
        //Arranges
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => partner);
        _userRepositoryMock.Setup(x => x.Create(It.IsAny<Partner>())).ReturnsAsync(() => partner);

        //Act
         Func<Task<PartnerDto>> act = async() => await _sut.Create(partnerDto);
        
        //Assert
        act.Should().ThrowAsync<DomainException>();
    }

    [Fact(DisplayName = "User Get by Id")]
    public async Task ShouldProvideAValidUserIfValidUserId() {
        //Arranges
        var userId = new Randomizer().Int(1, 9999999);
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        _userRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => partner);
        //Act
        var result = await _sut.Get(userId);
        
        //Assert
        result.Should().BeEquivalentTo(partnerDto);
    }

    [Fact(DisplayName = "User Get by Email")]
    public async Task ShouldProvideAValidUserIfValidUserEmil() {
        //Arranges
        var userEmail = new Faker().Person.Email;
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => partner);
        //Act
        var result = await _sut.Get(userEmail);
        
        //Assert
        result.Should().BeEquivalentTo(partnerDto);
    }

    [Fact(DisplayName = "User Get by Email - Invalid Email")]
    public void ShouldThrowIfNotExistEmail()
    {
        //Arranges
        var userEmail = new Faker().Person.Email;
        _userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => null);
        //Act
        Func<Task<PartnerDto>> act = async () => await _sut.Get(userEmail);
        //Assert
        act.Should()
            .ThrowAsync<DomainException>();
    }
    
    [Fact(DisplayName = "User Get All Users")]
    public async void ShouldReturnsAllUsers()
    {
        //Arranges
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        var users = new List<Partner>();
        users.Add(partner);
       _userRepositoryMock.Setup(x => x.GetAll()).Returns(async () => users);

        //Act
        var result = await _sut.GetAll();

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<List<PartnerDto>>(users));
    }


    [Fact(DisplayName = "User Get All Users")]
    public async void ShouldRemoveTheUser()
    {
        //Arranges
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        var users = new List<Partner>();
        users.Add(partner);
       _userRepositoryMock.Setup(x => x.GetAll()).Returns(async () => users);

        //Act
        var result = await _sut.GetAll();

        //Assert
        result.Should().BeEquivalentTo(_mapper.Map<List<PartnerDto>>(users));
    }

    [Fact(DisplayName = "Update User")]
    public async void ShouldUpdateUserWithAValidData()
    {
        //Arranges
        var partnerDto = partnerDtoBogus.Generate();
        var partner = _mapper.Map<Partner>(partnerDto);
        _userRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => partner);
        _userRepositoryMock.Setup(x => x.Update(It.IsAny<Partner>())).ReturnsAsync(() => partner);

        //Act
        var result = await _sut.Update(partnerDto);

        //Assert
        result.Should().BeEquivalentTo(partnerDto);
    }

    [Fact(DisplayName = "Remove User")]
    public async void ShouldRemoveUserWithAValidData()
    {
        //Arranges
        var userId = new Randomizer().Long(1, 9999999);
        _userRepositoryMock.Setup(x => x.Delete(It.IsAny<long>())).Verifiable();


        //Act
        await _sut.Remove(userId);
    
        //Assert
        _userRepositoryMock.Verify(x => x.Delete(userId), Times.Once);
    }
}
