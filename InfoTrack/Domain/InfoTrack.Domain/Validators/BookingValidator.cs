using FluentValidation;
using InfoTrack.Domain.Entities;

namespace InfoTrack.Domain.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required.");
            RuleFor(x => x.BookingStartTime).NotEmpty().NotNull().WithMessage("Booking start time is required.");
        }
    }
}
