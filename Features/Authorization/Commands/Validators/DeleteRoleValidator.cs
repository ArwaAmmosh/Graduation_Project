
namespace Graduation_Project.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator:AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructors

        #endregion
        public DeleteRoleValidator(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
            _authorizationService = authorizationService;
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

           
        }

        public void ApplyCustomValidationsRules()
        {
           /* RuleFor(x => x.Id)
                     .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistById(Key))
                     .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);*/
        }

        #endregion
    }
}
