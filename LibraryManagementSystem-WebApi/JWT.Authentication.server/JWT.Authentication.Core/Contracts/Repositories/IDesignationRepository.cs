namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface IDesignationRepository
    {
        Task<string?> GetUserDesignation(string staffId);
    }
}