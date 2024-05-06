using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;
using User.Domain.Exceptions;
using User.Infra.Interfaces;
using User.Services.Interfaces;

namespace User.Services.Service
{
    public class ClientServices : IClientServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientImageUploadService _clientImageUploadService;


        public ClientServices(IUserRepository userRepository, IClientImageUploadService clientImageUploadService)
        {
            _userRepository = userRepository;
            _clientImageUploadService = clientImageUploadService;

        }

        public async Task<Client> Create(Client client)
        {
            var hasUser = await _userRepository.GetByCPFCNPJ(client.CPFCnpj);
            if (hasUser != null) throw new PersonalizeExceptions("This CPF or CPNPJ is already registered");

            hasUser = await _userRepository.GetByCnh(client.CnhNumber);
            if (hasUser != null) throw new PersonalizeExceptions("This CNH Number is already registered");

            string imageUrl = await _clientImageUploadService.UploadImageAsync("images/cnh/", $"{client.Name}-{client.CnhNumber}.png", client.CnhImage);

            var user = new Client(client.Name, client.Email, client.Password, client.CPFCnpj, client.Birth, client.CnhNumber, client.CnhType, imageUrl, client.Role);
            user.Validate();
            var newUser = await _userRepository.Create(user);

            return newUser;
        }

        public async Task<Client> Get(long id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<Client> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<List<Client>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task Remove(long id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<Client> Update(Client client)
        {
            var hasUser = await _userRepository.Get(client.Id);
            if (hasUser != null) throw new PersonalizeExceptions("This Client is invalid");

            hasUser = await _userRepository.GetByCPFCNPJ(client.CPFCnpj);
            if (hasUser != null) throw new PersonalizeExceptions("This CPF or CPNPJ is already registered");

            hasUser = await _userRepository.GetByCnh(client.CnhNumber);
            if (hasUser != null) throw new PersonalizeExceptions("This CNH Number is already registered");


            if (hasUser.CnhImage != client.CnhImage)
            {
                string imageUrl = await _clientImageUploadService.UploadImageAsync("images/cnh/", $"{client.Name}-{client.CnhNumber}", client.CnhImage);

                client.CnhImage = imageUrl;
            }

            var user = new Client(client.Name, client.Email, client.Password, client.CPFCnpj, client.Birth, client.CnhNumber, client.CnhType, client.CnhImage, client.Role);
            user.Validate();
            var newUser = await _userRepository.Update(user);
            return newUser;
        }
    }
}
