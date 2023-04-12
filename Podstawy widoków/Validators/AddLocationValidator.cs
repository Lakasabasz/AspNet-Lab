using FluentValidation;
using Podstawy_widoków.DTOs;

namespace Podstawy_widoków.Validators;

public class AddLocationValidator: AbstractValidator<AddLocation>
{
    public AddLocationValidator()
    {
        RuleFor(x => x.City).Length(3, 32);
        RuleFor(x => x.Name).Length(3, 32);
        RuleFor(x => x.Street).Length(3, 32);
        RuleFor(x => x.BuildingNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}