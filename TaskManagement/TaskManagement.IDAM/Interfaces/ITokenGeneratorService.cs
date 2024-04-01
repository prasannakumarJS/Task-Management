namespace TaskManagement.IDAM.Interfaces
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateToken(string username, string password);
    }
}
