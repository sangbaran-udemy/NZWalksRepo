using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;

        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDBContext.Regions.AddAsync(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;            
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetAsync(Guid id)
        {
            return await nZWalksDBContext.Regions.FirstOrDefaultAsync(i => i.Id == id);              
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await nZWalksDBContext.Regions.FirstOrDefaultAsync(i => i.Id == id);
            if(region != null)
            {
                nZWalksDBContext.Regions.Remove(region);
                await nZWalksDBContext.SaveChangesAsync();
                return region;
            }
            return null;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionToUpdate = await nZWalksDBContext.Regions.FirstOrDefaultAsync(i => i.Id == id);

            if(regionToUpdate != null)
            {
                regionToUpdate.Code = region.Code;
                regionToUpdate.Area = region.Area;
                regionToUpdate.Latitude = region.Latitude;
                regionToUpdate.Longitude = region.Longitude;
                regionToUpdate.Name = region.Name;
                regionToUpdate.Population = region.Population;

                await nZWalksDBContext.SaveChangesAsync();
                return regionToUpdate;
            }

            return null;
            
        }
    }
}
