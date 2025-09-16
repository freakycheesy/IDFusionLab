using MelonLoader;
using HarmonyLib;
using LabFusion.Entities;
using System.Reflection;
using LabFusion.Player;
using UnityEngine;

[assembly: MelonInfo(typeof(IDFusionLab.Core), "IDFusionLab", "1.0.0", "pietr", null)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonAdditionalDependencies("LabFusion")]
namespace IDFusionLab
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            HarmonyInstance.PatchAll();
        }
    }

    [HarmonyPatch(typeof(RigNameTag))]
    public static class RigNameTagatches {

        [HarmonyPatch(nameof(RigNameTag.Spawn))]
        [HarmonyPrefix]
        public static void Prefix(RigNameTag __instance) {
            var player = NetworkPlayer.Players.ToList().Find(x=>x.Username == __instance.Username);
            if (player == null) return;
            var id = player.PlayerID.PlatformID;
            var name = $"{__instance.Username} \n({id})";
            if (__instance.Username != name) {
                __instance.Username = name;
                MelonLogger.Msg($"Player:({name}) Joined at:({Time.time})");
                __instance.UpdateText();
            }
        }
    }
}