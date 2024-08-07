﻿using FluentValidation;
using Graduation_Project.Features.Users.commands.Models;
using Graduation_Project.Resource;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Users.commands.Validatitors
{
    public class AddUserValidatitors : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion
        #region Constructors
        public AddUserValidatitors(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();

        }
        #endregion
        #region Handle Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
                .Matches(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPassword]);
            RuleFor(x => x.University)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }
        #endregion

    }
}
