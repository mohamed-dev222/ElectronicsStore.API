using ElectronicsStore.API.DTOs.Order;
using FluentValidation;

namespace ElectronicsStore.API.Validators
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches("^01[0-9]{9}$").WithMessage("Invalid Egyptian phone number.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District is required.");

            RuleFor(x => x.AddressDetails)
                .NotEmpty().WithMessage("Address details are required.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.")
                .Must(items => items != null && items.All(i => i.Quantity > 0))
                .WithMessage("Each order item must have a quantity greater than zero.");
        }
    }
}