using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticketize.Application.Features.Categories.Commands;
using Ticketize.Application.Features.Categories.Queries.GetCategoriesList;
using Ticketize.Application.Features.Categories.Queries.GetCategoriesListEventListVm;

namespace Ticketize.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await _mediator.Send(new GetCategoryListQuery());

            return Ok(dtos);
        }

        [HttpGet("allwithevents", Name = nameof(GetCategoriesWithEvents))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory)
        {
            var dtos = await _mediator.Send(new GetCategoriesListWithEventsQuery { IncludeHistory = includeHistory});

            return Ok(dtos);
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<CreateCategoryCommandResponse>> CreateCategoryAsync([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);

            return Ok(response);
        }
    }
}
