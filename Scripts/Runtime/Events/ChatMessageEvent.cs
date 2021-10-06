
using System;
using DoubTech.ScriptableEvents;
using TwitchLib.Client.Models;
using UnityEngine;

namespace DoubTech.TwitchClient.Events
{
    [CreateAssetMenu(fileName = "ChatMessageEvent",
        menuName = "DoubTech/Twitch Client/Events/Chat Message Event")]
    [Serializable]
    public class ChatMessageEvent : GameEvent<ChatMessage> { }
}
