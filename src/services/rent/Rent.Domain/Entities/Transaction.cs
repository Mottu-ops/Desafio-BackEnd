using Rent.Domain.Entities;
using Rent.Domain.Validators;
using Rent.Core.Exceptions;

namespace Rent.Domain.Entities;
public class Transaction : Base
{
    public long DeliveryMan { get; private set; }
    public long Manager { get; private set; }
    public long Motorcycle { get; private set; }
    public long Plan { get; private set; }
    
    protected Transaction() {}

    public Transaction(long deliveryMan, long manager, long motorcycle, long plan)
    {
        DeliveryMan = deliveryMan;
        Manager = manager;
        Motorcycle = motorcycle;
        Plan = plan;
    }

    public override bool Validate()
    {
        var validators = new TransactionValidator();
        var validationErrors =  validators.Validate(this);
        if(!validationErrors.IsValid) {
            foreach(var error in validationErrors.Errors) {
                _errors.Add(error.ErrorMessage);
            }
            throw new DomainException("Something is wrong, please fix it and try again.");
 
        }
        return validationErrors.IsValid;
    }
}