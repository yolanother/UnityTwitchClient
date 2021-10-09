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
        ChatMessageEventListener : GameEventListener<ChatMessage, ChatMessageEvent,
            ChatMessageUnityEvent>
    {
        [SerializeField] public string senderFilter;

        [SerializeField] private ChatMessageEvent gameEvent;
        [SerializeField] private ChatMessageUnityEvent onEvent = new ChatMessageUnityEvent();

        public override ChatMessageEvent GameEvent => gameEvent;
        public override ChatMessageUnityEvent OnEvent => onEvent;

        public override void Invoke(ChatMessage t)
        {
            base.Invoke(t);
        }

        public override void OnEventRaised(ChatMessage t)
        {
            if (string.IsNullOrEmpty(senderFilter) || t.UserId == senderFilter)
            {
                base.OnEventRaised(t);
            }
        }
    }

    [Serializable]
    public class ChatMessageUnityEvent : UnityEvent<ChatMessage>
    {
    }
}
