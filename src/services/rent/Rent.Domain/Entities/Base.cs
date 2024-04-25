namespace Rent.Domain.Entities;
public abstract class Base
{
    public long Id { get; set; }
    public DateTime StartDate {get; private set;}
    public DateTime EndDate {get; private set;}
    public decimal Price {get; private set;}
    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;
    public abstract bool Validate();


}