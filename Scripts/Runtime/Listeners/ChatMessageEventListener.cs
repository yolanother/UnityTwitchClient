using System;
using DoubTech.ScriptableEvents;
using DoubTech.ScriptableEvents.BuiltinTypes;
using DoubTech.TwitchClient.Events;
using TwitchLib.Client.Models;
using UnityEngine;
using UnityEngine.Events;

namespace DoubTech.TwitchClient.Listeners
{
    [Serializable]
    public class
        ChatMessageEventListener : GameEventListener<ChatMessage, ChatMessageEvent, ChatMessageUnityEvent>
    {
        [SerializeField] private ChatMessageEvent gameEvent;
        [SerializeField] private ChatMessageUnityEvent onEvent = new ChatMessageUnityEvent();

        public override ChatMessageEvent GameEvent => gameEvent;
        public override ChatMessageUnityEvent OnEvent => onEvent;
    }

    [Serializable]
    public class ChatMessageUnityEvent : UnityEvent<ChatMessage>
    {
    }
}
