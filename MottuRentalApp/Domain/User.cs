using System;

namespace MottuRentalApp.Domain
{
	public class User
	{
    public User(
      string name,
      string birthDate,
      UserType type,
      IList<Document> documents
    )
    {
      Identifier = Guid.NewGuid().ToString();
      Name = name;
      BirthDate = birthDate;
      UserType = type;
      Documents = documents;
    }

    public string Identifier { get; }
		public string Name { get; }
    public string BirthDate { get; set; }
    public UserType UserType { get; set; }
    public IList<Document> Documents { get; set; }
	}
}
