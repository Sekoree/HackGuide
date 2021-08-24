using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HackGuide.PsnUtil.Entities
{
    public class UserInfoResponse
    {
        [JsonPropertyName("scopes")]
        public string Scopes { get; set; }
        [JsonPropertyName("expiration")]
        public DateTime? Expiration { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("dcim_id")]
        public string DcimId { get; set; }
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("user_uuid")]
        public string UserUUID { get; set; }
        [JsonPropertyName("online_id")]
        public string OnlineId { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("language_code")]
        public string LanguageCode { get; set; }
        [JsonPropertyName("community_domain")]
        public string CommunityDomain { get; set; }
        [JsonPropertyName("is_sub_account")]
        public bool? IsSubAccount { get; set; }
    }
}
