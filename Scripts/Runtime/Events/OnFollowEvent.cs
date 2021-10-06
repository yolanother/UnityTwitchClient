using System;
using DoubTech.ScriptableEvents;
using UnityEngine;

namespace DoubTech.TwitchClient.Events
{
    [CreateAssetMenu(fileName = "FollowEvent",
        menuName = "DoubTech/Twitch Client/Events/Follow Event")]
    [Serializable]
    public class OnFollowEvent : GameEvent<UserInfo> { }
}
