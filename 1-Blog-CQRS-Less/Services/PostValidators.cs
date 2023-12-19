using FluentValidation;

namespace _1_Blog_CQRS_Less.Services;

public class CreatePostValidator : AbstractValidator<PostDTO>
{
    public CreatePostValidator()
    {
        RuleFor(p => p.Title).MinimumLength(5).MaximumLength(255);
        RuleFor(p => p.Content).MinimumLength(30);
    }
}