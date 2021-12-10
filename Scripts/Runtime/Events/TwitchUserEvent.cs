using System;
using DoubTech.ScriptableEvents;
using UnityEngine;

namespace DoubTech.TwitchClient.Events
{
    [CreateAssetMenu(fileName = "TwitchUserEvent",
        menuName = "DoubTech/Twitch Client/Events/Twitch User Event")]
    [Serializable]
    public class TwitchUserEvent : GameEvent<UserInfo>
    {
    }
}
