using Plan.Domain.Validators;

namespace Plan.Domain.Entity;

public class RentPlan : Base
{
    public string Name {get; private set;}
    public decimal DailyRate {get; private set;}
    public int Days {get; private set;}
    public long User {get; private set;}
    public decimal LateFee {get; private set;}

    protected RentPlan() {}

    public RentPlan(string name, decimal dailyRate, int days, long user, decimal lateFee)
    {
        Name = name;
        DailyRate = dailyRate;
        Days = days;
        User = user;
        LateFee = lateFee;
    }

    public override bool Validate()
    {
        var validators = new PlanValidator();
            var validation = validators.Validate(this);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors?.Add(error.ErrorMessage);
                }
                throw new Exception("Some errors are wrongs, please fix it and try again.");
            }
            return validation.IsValid;
    }
}