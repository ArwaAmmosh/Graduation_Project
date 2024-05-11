
namespace Graduation_Project.Features.Tool.Command.Validators
{
    public class AddToolValidatitor: AbstractValidator<PostToolDto>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _localizer;
        #endregion
        #region Constructors
        public AddToolValidatitor(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();

        }
        #endregion
        #region Handle Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Price)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Acadmicyear)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
    
            RuleFor(x => x.Category)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.College)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.RentTime)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
           
            RuleFor(x => x.Department)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.University)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }
        #endregion
    }
}
