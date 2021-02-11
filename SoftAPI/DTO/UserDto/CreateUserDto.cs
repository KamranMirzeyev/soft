using FluentValidation;
using System.Text.RegularExpressions;

namespace SoftAPI.DTO.UserDto
{
    public class CreateUserDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(a => a.FirstName).NotNull().WithMessage("FisrtName not null").NotEmpty().WithMessage("FisrtName not empty").MaximumLength(100);

            RuleFor(a => a.LastName).NotNull().WithMessage("LastName  not null").NotEmpty().WithMessage("LastName  not empty").MaximumLength(100);

            RuleFor(a => a.Password).NotNull().WithMessage("Password  not null").NotEmpty().WithMessage("Password  not empty").MaximumLength(30);

            RuleFor(a=>a.Phone).NotNull().WithMessage("Phone  not null").NotEmpty().WithMessage("Phone  not empty").MaximumLength(50).Must(BeAValidPhone).WithMessage("format '(XX)XXXXXXX'");

            RuleFor(a => a.Email).NotNull().WithMessage("Password not null").NotEmpty().WithMessage("Password Name not empty").MaximumLength(30).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);



        }
        private bool BeAValidPhone(string phone)
        {
            const string phoneregex = @"^([\(]{1}[0-9]{2,3}[\)]{1}[0-9]{3}[0-9]{2}[0-9]{2})$";

            bool isValidPhone = Regex.IsMatch(phone.Trim(), phoneregex);

            return isValidPhone;
        }
    }

}
