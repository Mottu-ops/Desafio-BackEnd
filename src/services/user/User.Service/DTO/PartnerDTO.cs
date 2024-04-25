using System.Text.Json.Serialization;

namespace User.Service.DTO;

public class PartnerDto {
        public long Id { get; set;}
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public DateTime DateBirth { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public string CnhImage { get; set; }
        public string Role { get; set; }

        public PartnerDto() {}
        public PartnerDto(long id, string name, string email, string password, string cnpj, DateTime dateBirth, string cnhNumber, string cnhType,
            string cnhImage, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Cnpj = cnpj;
            DateBirth = dateBirth;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImage = cnhImage;
            Role = role;
        }
}