using System;

namespace MottuRentalApp.Domain
{
	public class User
	{
    public User(
      string documentNumber,
      string documentType,
      string name,
      string birthDate,
      UserType type
    )
    {
      Identifier = Guid.NewGuid().ToString();
      Name = name;
      BirthDate = birthDate;
      UserType = type;
      Documents =
      [
        new Document() { Number = documentNumber, Type = (DocumentType) Enum.Parse(typeof(DocumentType), documentType) },
      ];
    }

    public string Identifier { get; }
		public string Name { get; }
    public string BirthDate { get; set; }
    public UserType UserType { get; set; }
    public IList<Document> Documents { get; set; }
	}
}
