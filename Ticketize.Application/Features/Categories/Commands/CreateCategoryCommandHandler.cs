using AutoMapper;
using MediatR;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createCategoryCommandResponse = new CreateCategoryCommandResponse();

            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                createCategoryCommandResponse.Success = false;
                createCategoryCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createCategoryCommandResponse.Success)
            {
                var category = new Category { Name = request.Name };
                category = await _categoryRepository.AddAsync(category);
                createCategoryCommandResponse.Category = _mapper.Map<CreateCategoryDto>(category);
            }

            return createCategoryCommandResponse;
        }
    }
}
