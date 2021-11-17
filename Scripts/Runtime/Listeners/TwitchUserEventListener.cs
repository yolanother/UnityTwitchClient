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
        TwitchUserEventListener : GameEventListener<UserInfo, TwitchUserEvent, TwitchUserUnityEvent>
    {
        [SerializeField] private TwitchUserEvent gameEvent;
        [SerializeField] private TwitchUserUnityEvent onEvent = new TwitchUserUnityEvent();

        public override TwitchUserEvent GameEvent => gameEvent;
        public override TwitchUserUnityEvent OnEvent => onEvent;
    }

    [Serializable]
    public class TwitchUserUnityEvent : UnityEvent<UserInfo>
    {
    }
}
