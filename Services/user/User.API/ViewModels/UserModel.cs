namespace User.API.ViewModels
{
    public class UserModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPFCnpj { get; set; }
        public DateTime Birth { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public string CnhImage { get; set; }
        public EnumRole Role { get; set; }
    }
    public enum EnumRole
    {
        User,
        Admin
    }

}
