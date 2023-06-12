using Microsoft.EntityFrameworkCore;
using Ticketize.Application.Contracts.Persistence;
using Ticketize.Domain.Entities;

namespace Ticketize.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TicketizeDbContext ticketizeDbContext) : base(ticketizeDbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includeHistory)
        {
            var allCategories = await _ticketizeDbContext.Categories.Include(x => x.Events).ToListAsync();
            if (!includeHistory)
            {
                allCategories.ForEach(c => c.Events?.ToList().RemoveAll(c => c.Date < DateTime.Today));
            }

            return allCategories;
        }
    }
}
