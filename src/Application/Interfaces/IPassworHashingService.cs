namespace NutriTrack.src.Application.Interfaces
{
    public interface IPassworHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
