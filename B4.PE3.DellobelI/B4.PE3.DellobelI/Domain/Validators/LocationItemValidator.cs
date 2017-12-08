using B4.PE3.DellobelI.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Validators
{
    public class LocationItemValidator : AbstractValidator<Location>
    {
        public LocationItemValidator()
        {
            RuleFor(item => item.LocationName)
                .NotEmpty()
                .WithMessage("Naam vereist om aan een lijst te voegen ")
                .Length(3, 80)
                .WithMessage("Lengte dient tussen 3 en 80 letters");

        }
    }
}
