using B4.PE3.DellobelI.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Validators
{
    public class LocationGroupValidator : AbstractValidator<LocationGroup>
    {
        public LocationGroupValidator()
        {
            RuleFor(locationGroup => locationGroup.Title)
                .NotEmpty()
                .WithMessage("Titel mag niet leeg zijn.")
                .Length(3, 50)
                .WithMessage("Titel dient min 3 tot max 50 karakters te bevatten.");

            RuleFor(locationGroup => locationGroup.Description)
                .NotEqual(b => b.Title)
                .WithMessage("Beschrijving dient verschillend van de titel te zijn.")
                .NotEmpty()
                .WithMessage("Beschrijving mag niet leeg zijn.");
        }
    }   
}
