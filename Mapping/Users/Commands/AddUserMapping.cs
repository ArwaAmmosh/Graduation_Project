﻿namespace Graduation_Project.Mapping.Users
{
    public partial class MappingProfile

    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();

        }
    }
}
