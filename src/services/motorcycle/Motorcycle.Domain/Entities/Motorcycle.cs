using Motorcycle.Core.Exceptions;
using Motorcycle.Domain.Validators;

namespace Motorcycle.Domain.Entities
{
    public class Vehicle : Base
    {

        public string PlateCode { get; private set; }
        public string Color { get; private set; }
        public string Model { get; private set; }
        public string Year { get; private set; }


        protected Vehicle() { }
        public Vehicle(long id, string plateCode, string color, string manufacturer, string year)
        {
            Id = id;
            PlateCode = plateCode;
            Color = color;
            Model = manufacturer;
            Year = year;
        }
        public override bool Validate()
        {
            var validators = new VehicleValidator();
            var validationErrors = validators.Validate(this);
            if (!validationErrors.IsValid)
            {
                foreach (var error in validationErrors.Errors)
                {
                    _errors?.Add(error.ErrorMessage);
                }
                throw new DomainException("Something is wrong, please fix it and try again.");
            }
            return validationErrors.IsValid;
        }
    }
}

