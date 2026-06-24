using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.Repositories.Interfaces;
using FluentValidation;

namespace ElectronicsStore.API.Validators
{
    public class ProductCreateValidator : AbstractValidator<CreateProductDto>
    {
        public ProductCreateValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to zero.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync(async (id, ct) => await categoryRepository.CategoryExistsAsync(id))
                .WithMessage("Category does not exist.");
        }
    }
}