using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public WalkRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDBContext.Walks.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomainModel = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (walkDomainModel == null)
                return null;

            nZWalksDBContext.Walks.Remove(walkDomainModel);
            await nZWalksDBContext.SaveChangesAsync();

            return walkDomainModel;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = nZWalksDBContext.Walks.Include("WalkDifficulty").Include("Region").AsQueryable();

            // Filter..
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting..
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }

            //Pagination..
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //var walks = await nZWalksDBContext.Walks.Include("WalkDifficulty").Include("Region").ToListAsync();
            //return walks;
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            var walkDomainModel = await nZWalksDBContext.Walks.Include("WalkDifficulty")
                .Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            
            if (walkDomainModel == null)
                return null;

            return walkDomainModel;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await nZWalksDBContext.Walks.Include("WalkDifficulty")
                .Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
                return null;

            walkDomainModel.Name = walk.Name;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.WalkImageURL = walk.WalkImageURL;
            walkDomainModel.WalkDifficulty = walk.WalkDifficulty;
            walkDomainModel.RegionId = walk.RegionId;                        

            await nZWalksDBContext.SaveChangesAsync();
            return walkDomainModel;            
        }
    }
}
