using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using System.Net.NetworkInformation;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> GetAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
