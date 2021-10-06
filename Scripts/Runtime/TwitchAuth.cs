using UnityEngine;

namespace DoubTech.TwitchClient
{
    [CreateAssetMenu(fileName = "TwitchAuth", menuName = "DoubTech/Twitch Client/Auth",
        order = 1)]
    public class TwitchAuth : ScriptableObject
    {
        public string channel = "";

        [Header("Client Access")] [Tooltip("Get these from https://dev.twitch.tv/")]
        public string clientId = "";

        public string clientSecret = "";


        [Header("Bot Access Token")]
        [Tooltip("Get these from https://twitchtokengenerator.com/")]
        public string botAccessToken = "";
        public string botRefreshToken = "";

        [Tooltip("Needs channel:read:redemptions & chat_login & openid")]
        public string oauthRedemption = "";
    }
}
