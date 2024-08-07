﻿using Graduation_Project.Features.Tool.Queries.Results;
using AutoMapper;


namespace Graduation_Project.Mapping.Tools
{
    public partial class MappingProfile: Profile
    {
      
        public MappingProfile() 
        {
            GetToolByIdMapping();
            GetToolPaginationMapping();
            AddToolMapping();
        }
      
    }
}