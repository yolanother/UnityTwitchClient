using System;
using System.Collections;
using System.Collections.Generic;
using DoubTech.ScriptableEvents;
using TwitchLib.Client.Models;
using UnityEngine;

namespace DoubTech.TwitchClient.Events
{
    [CreateAssetMenu(fileName = "ChatCommandEvent",
        menuName = "DoubTech/Twitch Client/Events/Chat Command Event")]
    [Serializable]
    public class TwitchUserEvent : GameEvent<UserInfo>
    {
    }
}
