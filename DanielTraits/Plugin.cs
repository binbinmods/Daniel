using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using static Obeliskial_Essentials.Essentials;
using System.IO;
using static Obeliskial_Essentials.CardDescriptionNew;
using UnityEngine;
using System;
using static Daniel.Traits;
using BepInEx.Configuration;

namespace Daniel
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.stiffmeds.obeliskialessentials")]
    [BepInDependency("com.stiffmeds.obeliskialcontent")]
    [BepInProcess("AcrossTheObelisk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal int ModDate = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
        private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        internal static ManualLogSource Log;

        public static ConfigEntry<bool> EnableDebugging { get; set; }


        public static string characterName = "Daniel";
        public static string subclassName = "Redeemer"; // needs caps
        public static string debugBase = "Binbin - Testing " + characterName + " ";

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"{PluginInfo.PLUGIN_GUID} {PluginInfo.PLUGIN_VERSION} has loaded!");

            EnableDebugging = Config.Bind(new ConfigDefinition(subclassName, "Enable Debugging"), true, new ConfigDescription("Enables debugging logs."));

            // register with Obeliskial Essentials
            RegisterMod(
                _name: PluginInfo.PLUGIN_NAME,
                _author: "binbin",
                _description: "Daniel, the Redeemer.",
                _version: PluginInfo.PLUGIN_VERSION,
                _date: ModDate,
                _link: @"https://github.com/binbinmods/theredeemer",
                _contentFolder: "Daniel",
                _type: ["content", "hero", "trait"]
            );
            // apply patches
            string t = $"For every {medsNumFormatItem(4)} {medsSpriteText("dark")} charges you apply, apply {medsNumFormatItem(1)} {medsSpriteText("bless")} {medsNumFormatItem(1)} {medsSpriteText("burn")} to all heroes";
            string card = "atonement";
            AddTextToCardDescription(t, TextLocation.End, card, includeAB: true);
            

            harmony.PatchAll();
        }

        internal static void LogDebug(string msg)
        {
            if (EnableDebugging.Value)
            {
                Log.LogDebug(debugBase + msg);
            }

        }
        internal static void LogInfo(string msg)
        {
            Log.LogInfo(debugBase + msg);
        }
        internal static void LogError(string msg)
        {
            Log.LogError(debugBase + msg);
        }
    }
}
