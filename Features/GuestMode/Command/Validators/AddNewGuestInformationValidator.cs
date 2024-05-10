
namespace Graduation_Project.Features.GuestMode.Command.Validators
{
    public class AddNewGuestInformationValidator: AbstractValidator<AddNewGuestInfoCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion
        #region Constructors
        public AddNewGuestInformationValidator(IStringLocalizer<SharedResource> localizer)
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
            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }
        #endregion

    }
}
