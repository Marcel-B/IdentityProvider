using System.Threading.Tasks;

namespace com.b_velop.App.IdentityProvider {
    public interface IIdentityProvider {
        Task<string> GetIdentityToken (string clientId, string secret, string scope);
        Task<string> GetIdentityToken (
            string clientId,
            string secret,
            string scope,
            string authorityUrl);
    }
}