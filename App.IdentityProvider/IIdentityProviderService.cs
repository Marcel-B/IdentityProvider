using System.Threading.Tasks;
using com.b_velop.App.IdentityProvider.Model;

namespace com.b_velop.App.IdentityProvider
{
    public interface IIdentityProviderService
    {
        Task<Token> GetToken(InfoItem infoItem);
    }
}