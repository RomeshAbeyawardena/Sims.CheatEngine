namespace Sims.CheatEngine.Contracts
{
    public interface IApplicationSettings
    {
        string CultureName { get; set; }
        long? DistributedMemoryCacheSizeLimitInBytes { get; set; }
    }
}