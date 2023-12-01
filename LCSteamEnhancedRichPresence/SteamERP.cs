using BepInEx;
using System.Collections;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LCSteamEnhancedRichPresence;

public class SteamERP : MonoBehaviour
{
    bool modInitialized = false; // Only start script if the game goes into main menu, check if SteamClient is initialised, if not disable script (might go LAN mode)

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Start() {
        Plugin.log.LogDebug("SteamERP Started... Waiting for Network Initialization");
        // Get GameNetworkManager and check disableSteam
        // create initialize function only if disableSteam is false and after OnSceneLoaded mainmenu for the first time.
        // if disablesteam is true, consider destroying this gameobject to disable mod until next boot
    }
    
    private void Update() {

    }

    private void UpdateRichPresence() {
        if (CheckIfSteamDisabled()) {
            Plugin.log.LogWarning("Attempted to SetRichPresence while Steam is disabled!");
            return;
        }
        try {
            SteamFriends.ClearRichPresence();
            bool result = SteamFriends.SetRichPresence("steam_display", "current writing a mod for LC");
            Plugin.log.LogInfo("RP returns " + result.ToString());
            result = SteamFriends.SetRichPresence("status", "test");
            Plugin.log.LogInfo("RP returns " + result.ToString());
            result = SteamFriends.SetRichPresence("steam_player_group", "test");
            SteamFriends.SetRichPresence("steam_player_group_size", "2000");
            Plugin.log.LogInfo("RP returns " + result.ToString());
            //SteamFriends.OpenOverlay("Friends");
            SteamClient.RunCallbacks();
            Plugin.log.LogInfo("RP set");
        } catch (System.Exception e) {
            Plugin.log.LogError(e.Message);
        }

    }

    private bool CheckIfSteamDisabled() { // Online or LAN mode
        return GameNetworkManager.Instance.disableSteam;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (!modInitialized) { // Initialize mod on the first load of main menu
            var currentScene = scene.name;
            if (currentScene is "MainMenu") {
                if (!CheckIfSteamDisabled()) {
                    Plugin.log.LogInfo("Steam is enabled!");
                    UpdateRichPresence();
                } else {
                    Plugin.log.LogInfo("Steam is disabled!");
                    Destroy(this.gameObject);
                }
            }
            // DO NOTHING HERE BECAUSE MOD IS NOT INITALIZED!
            return;
        }
        Plugin.log.LogInfo("HELLO");
        var name = SteamClient.SteamId;
        Plugin.log.LogInfo(name);
        Plugin.log.LogInfo("Game should be running");
        UpdateRichPresence();
        modInitialized = true;
    }

    private void OnSceneUnloaded(Scene scene) {

    }

    private void OnDestroy() {
        SteamClient.Shutdown();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Plugin.log.LogInfo("Mod disabled");
    }
}
