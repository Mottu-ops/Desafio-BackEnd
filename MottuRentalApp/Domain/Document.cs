using System;

namespace MottuRentalApp.Domain
{
  public class Document
  {
    public required string UserId { get; set; }
    public required string Number { get; set; }
    public required DocumentType Type { get; set; }
    public char? Category { get; set; }
    public string? FileReference { get; set; }
  }
}
