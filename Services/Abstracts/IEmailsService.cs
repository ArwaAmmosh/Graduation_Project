﻿namespace Graduation_Project.Services.Abstracts
{
    public interface IEmailsService
    {
        public Task<string> SendEmail(string email, string Message, string? reason);

    }
}
