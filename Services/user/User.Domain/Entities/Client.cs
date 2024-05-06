using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Exceptions;
using User.Domain.Validators;

namespace User.Domain.Entities
{
    public sealed record Client : ModelBase
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPFCnpj { get; set; }
        public DateTime Birth { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public string CnhImage { get; set; }
        public EnumRole Role { get; set; }

        protected Client() { }

        public Client(string name, string email, string password, string cpfCnpj, DateTime birth, string cnhNumber, string cnhType,
           string cnhImage, EnumRole role)
        {
            Name = name;
            Email = email;
            Password = password;
            CPFCnpj = cpfCnpj.Replace(".", String.Empty).Replace("/", String.Empty).Replace("-", String.Empty);
            Birth = birth;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImage = cnhImage;
            Role = role;
            _err = new List<string>();
        }


        public override bool Validate()
        {
            var validators = new Validator();
            var validation = validators.Validate(this);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _err?.Add(error.ErrorMessage);
                }
                throw new PersonalizeExceptions("Some errors are wrongs, please fix it and try again.", _err!);
            }
            return validation.IsValid;
        }

        public void ValidateChangeName(string name)
        {
            Name = name;
            Validate();
        }
        public void ValidateChangePassword(string password)
        {
            Password = password;
            Validate();
        }
        public void ValidateChangeEmail(string email)
        {
            Email = email;
            Validate();
        }

        public enum EnumRole
        {
            User,
            Admin
        }
    }
}
