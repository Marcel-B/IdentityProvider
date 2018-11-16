using System.Threading.Tasks;

namespace com.marcelbenders.App.IdentityProvider.Services
{
    public interface IIdentityProvider
    {
        Task<string> GetIdentityToken(string clientId, string secret, string scope);
    }
}