using ElectronicsStore.API.DTOs.UnavailableProduct;
using FluentValidation;

namespace ElectronicsStore.API.Validators
{
    public class UnavailableRequestCreateValidator : AbstractValidator<CreateUnavailableRequestFormDto>
    {
        public UnavailableRequestCreateValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20);

            RuleFor(x => x.RequestedProductName)
                .NotEmpty().WithMessage("Requested product name is required.")
                .MaximumLength(200);
        }
    }
}