namespace JWTLearning.Services.Interfaces
{
    public interface IJWTAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);
    }
}
