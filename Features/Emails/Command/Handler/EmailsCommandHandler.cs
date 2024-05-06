using Graduation_Project.Features.Emails.Command.Models;
using SharpDX.DXGI;

namespace Graduation_Project.Features.Emails.Command.Handler
{
    public class EmailsCommandHandler : ResponseHandler,
          IRequestHandler<SendEmailCommand, Response<string>>
    {
       #region Fields
        private readonly IEmailsService _emailsService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructors
        public EmailsCommandHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                    IEmailsService emailsService) : base(stringLocalizer)
        {
            _emailsService = emailsService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
        #endregion
    }
}
