using BepInEx;
using HarmonyLib;
using MysticClient.UISetting;
using MysticEmotes.Loading;
using MysticEmotes.Main;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MysticEmotes.Patches
{
    [HarmonyPatch(typeof(GorillaTagger), "Awake")]
    internal class StupidPatch // just what Colossus did to get his emotes working you can find his at
    { // https://github.com/ColossusYTTV/Colossal-Emotes/blob/main/MakeItFuckingWork/TotallyNotHookingGorillaTagger.cs
        public static void Prefix() => BepInPatcher.CreatePatch();
    }
    internal class PluginInfo {
        public const string GUID = "com.thatguy.gorillatag.mysticemotes";
        public const string Name = "Mystic Emotes";
        public const string Version = "1.0.0";
    }
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class BepInPatcher : BaseUnityPlugin
    {
        BepInPatcher() => new Harmony(PluginInfo.GUID).PatchAll(Assembly.GetExecutingAssembly());

        public static void CreatePatch()
        {
            var loader = new GameObject("Emote Loader");
            loader.AddComponent<Plugin>();
            loader.AddComponent<AssetLoading>();
            loader.AddComponent<UI>();
            loader.AddComponent<UIMenu>();
            loader.AddComponent<UIInputs>();
            DontDestroyOnLoad(loader);
        }
    }
}
