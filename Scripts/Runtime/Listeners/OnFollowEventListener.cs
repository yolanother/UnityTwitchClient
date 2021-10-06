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
        OnFollowEventListener : GameEventListener<UserInfo, OnFollowEvent, OnFollowUnityEvent>
    {
        [SerializeField] private OnFollowEvent gameEvent;
        [SerializeField] private OnFollowUnityEvent onEvent = new OnFollowUnityEvent();

        public override OnFollowEvent GameEvent => gameEvent;
        public override OnFollowUnityEvent OnEvent => onEvent;
    }

    [Serializable]
    public class OnFollowUnityEvent : UnityEvent<UserInfo>
    {
    }
}
