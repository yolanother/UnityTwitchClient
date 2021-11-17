using System;
using System.Collections;
using TwitchLib.Api.V5.Models.Users;
using TwitchLib.Client.Models;
using TwitchLib.PubSub.Events;
using TwitchLib.Unity;
using UnityEngine;
using DoubTech.TwitchClient.Events;
using TwitchLib.Client.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace DoubTech.TwitchClient
{
    public class TwitchClient : MonoBehaviour
    {
        [SerializeField] private TwitchAuth twitchAuth;

        [FormerlySerializedAs("commandEvent")]
        [Header("Events")]
        [SerializeField] private ChatCommandEvent chatCommandEvent;
        [SerializeField] private ChatMessageEvent chatMessageEvent;
        [SerializeField] private OnFollowEvent onFollowEvent;
        [SerializeField] private TwitchUserEvent onUserJoinedEvent;
        [SerializeField] private TwitchUserEvent onUserLeftEvent;


        private Client client;
        private PubSub pubSub;
        private Api api;

        private void Start()
        {
            var credentials =
                new ConnectionCredentials(twitchAuth.channel, twitchAuth.botAccessToken);

            client = new Client();
            client.Initialize(credentials, twitchAuth.channel);
            client.OnConnected += OnConnected;
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnUserJoined += OnUserJoinedChannel;
            client.OnUserLeft += OnUserLeftChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
            client.Connect();

            pubSub = new PubSub();
            // pubSub.OnWhisper += OnWhisper;
            pubSub.OnPubSubServiceConnected += OnPubSubServiceConnected;
            pubSub.OnPubSubServiceError += OnPubSubServiceError;
            pubSub.OnPubSubServiceClosed += OnPubSubServiceClosed;
            pubSub.OnListenResponse += OnListenResponse;
            pubSub.OnChannelPointsRewardRedeemed += OnChannelPointsReceived;
            pubSub.OnFollow += OnFollow;

            // Deprecated but good as fallback. (Was being rate limited perhaps around 12:18 friday.)
            pubSub.OnRewardRedeemed += OnRewardRedeemed;
            pubSub.Connect();

            api = new Api();
            api.Settings.ClientId = twitchAuth.botAccessToken;
        }

        private void OnUserLeftChannel(object sender, OnUserLeftArgs e)
        {
            Debug.Log($"<color=red>{e.Username} has left.</color>");
            onUserLeftEvent?.Invoke(new UserInfo()
            {
                username = e.Username
            });
        }

        private void OnUserJoinedChannel(object sender, OnUserJoinedArgs e)
        {
            Debug.Log($"<color=green>{e.Username} has joined.</color>");
            onUserJoinedEvent?.Invoke(new UserInfo()
            {
                username = e.Username
            });
        }

        private void OnFollow(object sender, OnFollowArgs e)
        {
            var userInfo = new UserInfo()
            {
                username = e.Username,
                displayName = e.DisplayName,
                userId = e.UserId
            };

            onFollowEvent?.Invoke(userInfo);
        }

        private void OnRewardRedeemed(object sender, OnRewardRedeemedArgs e)
        {

        }

        private void OnChannelPointsReceived(object sender, OnChannelPointsRewardRedeemedArgs e)
        {

        }

        private void OnListenResponse(object sender, OnListenResponseArgs e)
        {

        }

        private void OnPubSubServiceClosed(object sender, EventArgs e)
        {

        }

        private void OnPubSubServiceError(object sender, OnPubSubServiceErrorArgs e)
        {

        }

        private void OnPubSubServiceConnected(object sender, EventArgs e)
        {
            pubSub.ListenToRewards(twitchAuth.channel); // GOOD AS A FALLBACK FOR DEBUG.
            pubSub.ListenToChannelPoints(twitchAuth.channel);

            //pubSub.SendTopics(twitchAuth.oauthRedemption);
            pubSub.SendTopics();
        }

        /// <summary>
        /// Coroutine to Fetch twitch user profile image
        /// </summary>
        /// <param name="userLogin">twitch username</param>
        /// <param name="callback">callback for when the image is ready, wont be called if the requests fail</param>
        public IEnumerator GetUserProfileIcon(string userLogin, Action<Sprite> callback)
        {
            //not huge fan of this flow but it was in the twitch lib examples �\_(?)_/�
            Users getUsersResponse = null;

            //helix requires access tokens in the header... cba, using kraken for now, even if its deprecated
            yield return api.InvokeAsync(api.V5.Users.GetUserByNameAsync(userLogin),
                ((response) => { getUsersResponse = response; })
            );

            var users = getUsersResponse.Matches;

            if (users.Length > 0)
            {
                var user = users[0];
                var imageUrl = user.Logo;

                var www = UnityWebRequestTexture.GetTexture(imageUrl);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(www);

                    var sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
                    callback(sprite);
                }
            }
        }

        private void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            Debug.Log($"<color=green>[{e.Command.ChatMessage.Username}]</color> <color=blue>{e.Command.CommandText}</color> {e.Command.ArgumentsAsString}");
            chatCommandEvent?.Invoke(e.Command);
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            Debug.Log($"<color=green>[{e.ChatMessage.Username}]</color> {e.ChatMessage.Message}");
            chatMessageEvent?.Invoke(e.ChatMessage);
        }

        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
        }

        private void OnConnected(object sender, OnConnectedArgs e)
        {
            Debug.Log($"The bot {e.BotUsername} succesfully connected to Twitch.");
            if (!string.IsNullOrWhiteSpace(e.AutoJoinChannel))
                Debug.Log(
                    $"The bot will now attempt to automatically join the channel provided when the Initialize method was called: {e.AutoJoinChannel}");
        }
    }
}
