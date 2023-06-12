using MediatR;

namespace Ticketize.Application.Features.Categories.Queries.GetCategoriesListEventListVm
{
    public class GetCategoriesListWithEventsQuery : IRequest<List<CategoryEventListVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
