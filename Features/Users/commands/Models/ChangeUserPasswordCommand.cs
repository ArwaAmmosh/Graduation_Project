﻿namespace Graduation_Project.Features.Users.commands.Models
{
    public class ChangeUserPasswordCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
