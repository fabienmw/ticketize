using Ticketize.Application.Responses;

namespace Ticketize.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryDto Category { get; set; } = default!;
        public CreateCategoryCommandResponse() : base()
        {
            
        }
    }
}
