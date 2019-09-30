using System.Threading.Tasks;
using com.b_velop.IdentityProvider.Model;

namespace com.b_velop.IdentityProvider
{
    public interface IIdentityProviderService
    {
        Task<Token> GetTokenAsync(InfoItem infoItem);
    }
}