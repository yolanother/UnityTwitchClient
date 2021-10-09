using System;
using TwitchLib.Client.Models;

namespace DoubTech.TwitchClient
{
    [Serializable]
    public class UserInfo
    {
        public string username;
        public string displayName;
        public string userId;

        public UserInfo() {}
        public UserInfo(ChatMessage message) => new UserInfo()
        {
            username = message.Username,
            userId = message.UserId,
            displayName = message.DisplayName
        };

        public UserInfo(ChatCommand command) => new UserInfo()
        {
            username = command.ChatMessage.Username,
            userId = command.ChatMessage.UserId,
            displayName = command.ChatMessage.DisplayName
        };

        public static explicit operator UserInfo(ChatMessage message) => new UserInfo(message);

        public static explicit operator UserInfo(ChatCommand command) => new UserInfo(command);
    }

    public static class UserInfoUtil
    {
        public static UserInfo UserInfo(this ChatMessage message) => new UserInfo(message);
        public static UserInfo UserInfo(this ChatCommand command) => new UserInfo(command);
    }
}
