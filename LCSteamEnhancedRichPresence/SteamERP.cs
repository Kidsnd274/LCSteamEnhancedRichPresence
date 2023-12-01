using BepInEx;
using System;
using Steamworks;
using UnityEngine;

namespace LCSteamEnhancedRichPresence;
public class SteamERP : MonoBehaviour
{
    private void Awake() {
        if (SteamManager.Instance == null) {
            var steamManagerGameObject = new GameObject("SteamManager");
            steamManagerGameObject.AddComponent<SteamManager>();
            DontDestroyOnLoad(steamManagerGameObject);
            Plugin.log.LogInfo("Created SteamManager GameObject");
        }
    }

    private void Start() {
        Plugin.log.LogInfo("SteamERP Object Start()");
        if (SteamManager.Instance != null) {
            UpdateRichPresence();
            Plugin.log.LogInfo("RichPresence set");
        } else {
            Plugin.log.LogInfo("SteamManager Instance was null");
        }
        
    }
    
    private void UpdateRichPresence() {
        SteamFriends.SetRichPresence("status", "current writing a mod for LC");
    }
}
