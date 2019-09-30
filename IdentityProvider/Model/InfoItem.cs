using System;
using System.Text;

namespace com.b_velop.IdentityProvider.Model
{
    public class InfoItem
    {
        public InfoItem(
            string clientId,
            string secret,
            string scope,
            string url = null)
        {
            Credentials = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
            Url = url ?? string.Empty;
            ClientId = clientId;
            Scope = scope;
            Secret = secret;
        }

        public string ClientId { get; set; }
        public string Secret { get; set; }
        public byte[] Credentials { get; }
        public string Scope { get; set; }
        private string _url;
        public string Url { get => _url; set => _url = value.Trim().TrimEnd('/'); }

        public string GetBase64Credentials()
            => Convert.ToBase64String(Credentials);

        public string GetUrlTokenPrefix()
            => Url + "/connect/token";

        public void Delete()
            => Secret = string.Empty;
    }
}
