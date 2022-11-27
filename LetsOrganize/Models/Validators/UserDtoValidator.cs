using FluentValidation;
using LetsOrganize.Entities;

namespace LetsOrganize.Models.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(LetsOrganizeDbContext dbContext)
        {
            RuleFor(e => e.Email)
                .Custom((emailValue, context) =>
                {
                    var takenEmail = dbContext.Users.Any(u => u.Email == emailValue);
                    if (takenEmail)
                    {
                        context.AddFailure("Email", "Account with this email exists.");
                    }
                });

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.Password).MinimumLength(8);

            RuleFor(c => c.ConfirmPassword).Equal(p => p.Password);
        }
    }
}
