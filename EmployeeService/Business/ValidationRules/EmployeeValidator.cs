using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class EmployeeValidator :AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e=>e.FirstName).NotEmpty();
            RuleFor(e => e.FirstName).Length(2, 15).WithMessage("Employee name must be between 2-15 character");

            RuleFor(e => e.LastName).NotEmpty();
            RuleFor(e => e.LastName).Length(2, 15).WithMessage("Employee lastname must be between 2-15 character");

            RuleFor(e => e.Age).NotEmpty();
            RuleFor(e => e.Age).GreaterThanOrEqualTo(18).WithMessage(" Employee age must be greater than 18");


            RuleFor(e => e.Email).NotEmpty();
            RuleFor(e => e.Email).EmailAddress().WithMessage("Invalid email address");
            
            RuleFor(e => e.PhoneNumber).NotEmpty();
            RuleFor(e => e.PhoneNumber).Matches(@"^0\d{10}$").WithMessage("Phone number must be start with 0 and total character must be 11.");

            RuleFor(e => e.Gender).NotEmpty();

        }
    }
}
