using CarParkSystem.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Service.Validations
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropoertyName} is required").NotEmpty().WithMessage("{PropoertyName} is required").MaximumLength(200).WithMessage("{PropoertyName} can not be longer than 200 characters");
        }
    }
}
