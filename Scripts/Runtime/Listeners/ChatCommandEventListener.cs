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
        ChatCommandEventListener : GameEventListener<ChatCommand, ChatCommandEvent, ChatCommandUnityEvent>
    {
        [SerializeField] private ChatCommandEvent gameEvent;
        [SerializeField] private ChatCommandUnityEvent onEvent = new ChatCommandUnityEvent();

        public override ChatCommandEvent GameEvent => gameEvent;
        public override ChatCommandUnityEvent OnEvent => onEvent;
    }

    [Serializable]
    public class ChatCommandUnityEvent : UnityEvent<ChatCommand>
    {
    }
}
