using MelonLoader;
using HarmonyLib;
using LabFusion.Entities;
using System.Reflection;
using LabFusion.Player;

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

    [HarmonyPatch(typeof(PlayerID))]
    public static class PlayerIDPatches {
        private static Type PlayerType = typeof(PlayerID);
        private static FieldInfo NameFieldInfo = PlayerType.GetField("_username", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch(nameof(PlayerID))]
        [HarmonyPrefix]
        public static void PlayerIDPrefix(PlayerID __instance) {
            var metaData = __instance.Metadata;
            var id = __instance.PlatformID;
            var metaData = $"{__instance.Metadata.Username} \n({id})";
            if (__instance.Metadata.Username != name) {
                NameFieldInfo.SetValue(__instance, name);
            }
        }
    }
}