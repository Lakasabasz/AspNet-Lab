using FluentValidation;
using Podstawy_widoków.DTOs;

namespace Podstawy_widoków.Validators;

public class AddReservationValidator : AbstractValidator<AddReservation>
{
    public AddReservationValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x).Must((reservation, _, _) => reservation.Begin < reservation.End);
    }
}