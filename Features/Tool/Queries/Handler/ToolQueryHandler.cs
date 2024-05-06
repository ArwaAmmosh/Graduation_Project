using AutoMapper;
using Graduation_Project.Entities;
using Graduation_Project.Features.Tool.Queries.Models;
using Graduation_Project.Features.Tool.Queries.Results;
using Graduation_Project.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graduation_Project.Features.Tool.Queries.Handler
{
    public class ToolQueryHandler : ResponseHandler,
          IRequestHandler<GetToolPaginationQuery, PaginatedResult<GetToolPaginationReponse>>,
          IRequestHandler<GetToolByIdQuery, Response<GetToolByIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly ToolManager _toolManager;

        public ToolQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IMapper mapper,
                                  ToolManager toolManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _toolManager = toolManager;
        }

        public async Task<PaginatedResult<GetToolPaginationReponse>> Handle(GetToolPaginationQuery request, CancellationToken cancellationToken)
        {
            var tools = await _toolManager.GetAllToolsAsync();
            var paginatedList = await _mapper.ProjectTo<GetToolPaginationReponse>(tools.AsQueryable())
                                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }

        public async Task<Response<GetToolByIdResponse>> Handle(GetToolByIdQuery request, CancellationToken cancellationToken)
        {
            var tool = await _toolManager.GetToolByIdAsync(request.Id);
            if (tool == null)
                return NotFound<GetToolByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetToolByIdResponse>(tool);
            return Success(result);
        }
    }
}
