using HackGuide.PsnUtil.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HackGuide.PsnUtil
{
    public class PsnClient
    {
        static HttpClient _client = new HttpClient();

        private const string _tokenURL = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token";

        static PsnClient()
        {
            var byteArray = Encoding.ASCII.GetBytes("ba495a24-818c-472b-b12d-ff231c1b5745:mvaiZkRsAsI1IBkY");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public string GetLoginURL() => "https://id.sonyentertainmentnetwork.com/signin/?service_entity=urn:service-entity:psn&response_type=code&client_id=ba495a24-818c-472b-b12d-ff231c1b5745&redirect_uri=https://remoteplay.dl.playstation.net/remoteplay/redirect&scope=psn:clientapp&request_locale=en_US&ui=pr&service_logo=ps&layout_type=popup&smcid=remoteplay&PlatformPrivacyWs1=minimal&error=login_required&error_code=4165&error_description=User+is+not+authenticated&no_captcha=false#/signin?entry=%2Fsignin";
        
        public async Task<TokenResponse> GetTokenResponse(string codeURL)
        {
            if (!codeURL.Contains("code")) return default;
            var code = codeURL.Split('=')[1].Split('&')[0];
            TokenResponse tokenObj = default;
            using (var request = new HttpRequestMessage(HttpMethod.Post, _tokenURL))
            {
                request.Content = new StringContent($"grant_type=authorization_code&code={code}&redirect_uri=https://remoteplay.dl.playstation.net/remoteplay/redirect&", Encoding.ASCII, "application/x-www-form-urlencoded");
                var tokenResponse = await _client.SendAsync(request);
                if (!tokenResponse.IsSuccessStatusCode) return default;
                tokenObj = JsonSerializer.Deserialize<TokenResponse>(await tokenResponse.Content.ReadAsStringAsync());
                return tokenObj;
            }
        }

        public async Task<UserInfoResponse> GetUserInfoResponse(TokenResponse tokenResponse)
        {
            if (tokenResponse == default) return default;
            var got = await _client.GetAsync(_tokenURL + "/" + tokenResponse.AccessToken);
            if (!got.IsSuccessStatusCode) return default;
            return await JsonSerializer.DeserializeAsync<UserInfoResponse>(await got.Content.ReadAsStreamAsync());
        }

        public Task<string> GetAID(UserInfoResponse userInfoResponse)
        {
            var idBytes = BitConverter.GetBytes(ulong.Parse(userInfoResponse.UserId));
            var almost = Convert.ToBase64String(idBytes);
            return Task.FromResult(BitConverter.ToString(Convert.FromBase64String(almost)).Replace("-", "").ToLower());
        }

        public async Task<string> GetAID(string codeURL)
        {
            var token = await this.GetTokenResponse(codeURL);
            var info = await this.GetUserInfoResponse(token);
            return await this.GetAID(info);
        }
    }
}
