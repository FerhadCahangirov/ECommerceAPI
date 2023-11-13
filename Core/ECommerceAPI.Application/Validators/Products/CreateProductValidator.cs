using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter product name.")
            .MaximumLength(150)
            .MinimumLength(5)
            .WithMessage("Please write product name between 5 and  150 chars");


            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter stock number.")
                .Must(s => s >= 0)
                .WithMessage("Stock number cannot be negative.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter price.")
                .Must(s => s >= 0)
                .WithMessage("Price cannot be negative.");

        }
    }
}
