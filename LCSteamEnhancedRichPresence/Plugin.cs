using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace LCSteamEnhancedRichPresence
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource log;
        private void Awake()
        {
            log = Logger;
            log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            GameObject steamErpGameObject = new GameObject();
            steamErpGameObject.AddComponent<SteamERP>();
            DontDestroyOnLoad(steamErpGameObject);
            steamErpGameObject.hideFlags = HideFlags.HideAndDontSave;
            log.LogInfo("Created SteamERP Object");
        }
    }
}
