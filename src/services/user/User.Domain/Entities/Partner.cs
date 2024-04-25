using User.Core.Execeptions;
using User.Domain.Validators;

namespace User.Domain.Entities
{
    public class Partner : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public string Password { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime DateBirth { get; private set; }
        public string CnhNumber { get; private set; }
        public string CnhType { get; private set; }
        public string CnhImage { get; private set; }
        public string Role { get; private set; }

        protected Partner() {}

        public Partner(string name, string email, string password, string cnpj, DateTime dateBirth, string cnhNumber, string cnhType,
            string cnhImage, string role)
        {
            Name = name;
            Email = email;
            Password = password;
            Cnpj = cnpj;
            DateBirth = dateBirth;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImage = cnhImage;
            Role = role;
            _errors = new List<string>();
        }

        public override bool Validate()
        {
            var validators = new UserValidator();
            var validation = validators.Validate(this);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors?.Add(error.ErrorMessage);
                }
                throw new DomainException("Some errors are wrongs, please fix it and try again.", _errors!);
            }
            return validation.IsValid;
        }

        public void ChangeName(string name)
        {
            Name = name;
            Validate();
        }
        public void ChangePassword(string password)
        {
            Password = password;
            Validate();
        }
        public void ChangeEmail(string email)
        {
            Email = email;
            Validate();
        }


    }
}

