using System.ComponentModel;
using UnityEngine;

#if UNITY_EDITOR
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
#endif

namespace DoubTech.TwitchClient
{
    [CreateAssetMenu(fileName = "TwitchAuth", menuName = "DoubTech/Twitch Client/Auth",
        order = 1)]
    public class TwitchAuth : ScriptableObject
    {
        public string channel = "";

        [Header("Client Access")] [Tooltip("Get these from https://dev.twitch.tv/")]
        [PasswordPropertyText]
        public string clientId = "";

        [PasswordPropertyText]
        public string clientSecret = "";


        [Header("Bot Access Token")]
        [Tooltip("Get these from https://twitchtokengenerator.com/")]
        [PasswordPropertyText]
        public string botAccessToken = "";
        [PasswordPropertyText]
        public string botRefreshToken = "";

        [Tooltip("Needs channel:read:redemptions & openid")]
        [PasswordPropertyText]
        public string oauthRedemption = "";
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(TwitchAuth))]
    public class TwitchAuthInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var auth = (TwitchAuth) target;
            auth.channel = EditorGUILayout.TextField("Channel", auth.channel);
            GUILayout.Space(16);
            if (GUILayout.Button(
                new GUIContent("Client Access", "Get these from https://dev.twitch.tv/"),
                EditorStyles.label))
            {
                Application.OpenURL("https://dev.twitch.tv/console/apps");
            }

            auth.clientId = EditorGUILayout.PasswordField("Client ID", auth.clientId);
            auth.clientSecret = EditorGUILayout.PasswordField("Client Secret", auth.clientSecret);

            GUILayout.Space(16);
            if (GUILayout.Button(
                new GUIContent("Bot Access", "Get these from https://twitchtokengenerator.com/"),
                EditorStyles.label))
            {
                Application.OpenURL("https://twitchtokengenerator.com/");
            }

            auth.botAccessToken = EditorGUILayout.PasswordField("Access Token", auth.botAccessToken);
            auth.botRefreshToken = EditorGUILayout.PasswordField("Refresh Token", auth.botRefreshToken);

            GUILayout.Space(16);
            auth.oauthRedemption =
                EditorGUILayout.PasswordField("OAUTH Redemption", auth.oauthRedemption);
        }
    }

    [CustomPropertyDrawer(typeof(TwitchAuth))]
    public class TwitchAuthPropDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
            //return EditorGUIUtility.singleLineHeight * 8 + 32;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null)
            {
                base.OnGUI(position, property, label);
            }
            else
            {
                var rect = position;
                rect.height = EditorGUIUtility.singleLineHeight;
                if (GUI.Button(rect, "Edit Auth"))
                {
                    EditorGUIUtility.PingObject(property.objectReferenceValue);
                    Selection.activeObject = property.objectReferenceValue;
                }
            }

            /* For some reason FindProperty isn't finding a value. Temp hack
            DrawField(rect, property, "channel");

            rect.y += 16;

            DrawUrlButton(rect, "Client Access", "https://dev.twitch.tv");
            DrawField(rect, property, "clientId", "Client ID", true);
            DrawField(rect, property, "clientSecret", "Client Secret", true);

            rect.y += 16;

            DrawUrlButton(rect, "Bot Access Token", "https://twitchtokengenerator.com/");
            DrawField(rect, property, "botAccessToken", "Access Token", true);
            DrawField(rect, property, "botRefreshToken", "Refresh Token", true);
            DrawField(rect, property, "oauthRedemption", "OAUTH Redemption", true);
            */
        }

        private void DrawUrlButton(Rect rect, string label, string url)
        {
            if (EditorGUI.LinkButton(rect, label))
            {
                Application.OpenURL(url);
            }
            rect.y += EditorGUIUtility.singleLineHeight;
        }

        private void DrawLabel(Rect rect, string label)
        {
            EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);
            rect.y += EditorGUIUtility.singleLineHeight;
        }

        private void DrawField(Rect rect, SerializedProperty property, string name, string label = "", bool isPassword = false)
        {
            var prop = property.serializedObject.FindProperty(name);
            if (null == prop)
            {
                Debug.LogError("Can't find prop: "+ name);
                return;
            }
            if (isPassword)
            {
                prop.stringValue = EditorGUI.PasswordField(rect, label, prop.stringValue);
            } else {
                EditorGUI.PropertyField(rect, prop);
            }

            rect.y += EditorGUIUtility.singleLineHeight;
        }
    }
    #endif
}
