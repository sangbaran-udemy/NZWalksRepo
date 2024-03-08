namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
