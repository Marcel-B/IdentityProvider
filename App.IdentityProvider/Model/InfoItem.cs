﻿using System;
using System.Text;

namespace com.b_velop.App.IdentityProvider.Model
{
    public class InfoItem
    {
        public InfoItem(
            string clientId,
            string secret,
            string url = null)
        {
            Credentials = Encoding.ASCII.GetBytes($"{clientId}:{secret}");
            Url = url ?? string.Empty;
        }

        public byte[] Credentials { get; }
        public string Scope { get; set; }
        private string _url;
        public string Url { get => _url; set => _url = value.Trim().TrimEnd('/'); }

        public string GetBase64Credentials()
            => Convert.ToBase64String(Credentials);

        public string GetUrlTokenPrefix()
            => Url + "/connect/token";
    }
}
