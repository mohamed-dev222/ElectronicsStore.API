using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.Repositories.Interfaces;
using FluentValidation;

namespace ElectronicsStore.API.Validators
{
    public class ProductUpdateValidator : AbstractValidator<UpdateProductDto>
    {
        public ProductUpdateValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(x => x.Name)
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .GreaterThan(0).When(x => x.Price.HasValue)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).When(x => x.Quantity.HasValue)
                .WithMessage("Quantity must be greater than or equal to zero.");

            RuleFor(x => x.CategoryId)
                .MustAsync(async (id, ct) => id == null || await categoryRepository.CategoryExistsAsync(id.Value))
                .WithMessage("Category does not exist.");
        }
    }
}