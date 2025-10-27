using FluentValidation;
using SoftPlan.TaskManager.Application.DTOs;

namespace SoftPlan.TaskManager.Application.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(2000);
        RuleFor(x => x.UserId).NotEmpty();
    }
}

