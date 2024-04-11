using FluentValidation;
using Graduation_Project.Features.User.commands.Models;
using Microsoft.Extensions.Localization;
using OpenXmlPowerTools;
using SharpDX.DXGI;
using SolrNet.Utils;

namespace Graduation_Project.Features.User.commands.Validatitors
{
    public class AddUserValidatitors:AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion
        #region Constructors
        public AddUserValidatitors(IStringLocalizer<SharedResource> localizer) { 
            _localizer = localizer;
            ApplyValidationRules();

        }
        #endregion
        #region Handle Functions
        public void ApplyValidationRules()
        {
            RuleFor < X=>X.FullName >
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.M])
        }
        #endregion

    }
}
