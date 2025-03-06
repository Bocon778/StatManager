using HarmonyLib;
using UnityEngine;

namespace StatManager.Patches
{
    class Patch
    {
        private static bool hasAppliedUpgrade = false;

        [HarmonyPatch(typeof(GameDirector), "Start")]
        class GameDirectorPatch
        {
            static void Postfix()
            {
                GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(WaitForLevel());
            }
        }

        private static System.Collections.IEnumerator WaitForLevel()
        {
            while (!SemiFunc.LevelGenDone())
            {
                yield return new WaitForSeconds(0.5f);
            }

            if (!hasAppliedUpgrade && SemiFunc.RunIsLevel())
            {
                foreach (PlayerAvatar player in SemiFunc.PlayerGetAll())
                {
                    for (int i = 0; i < StatManager.HealthBonus.Value; i++)
                        PunManager.instance.UpgradePlayerHealth(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.SpeedBonus.Value; i++)
                        PunManager.instance.UpgradePlayerSprintSpeed(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.MapCountBonus.Value; i++)
                        PunManager.instance.UpgradeMapPlayerCount(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.EnergyBonus.Value; i++)
                        PunManager.instance.UpgradePlayerEnergy(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.ExtraJumpBonus.Value; i++)
                        PunManager.instance.UpgradePlayerExtraJump(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.GrabRangeBonus.Value; i++)
                        PunManager.instance.UpgradePlayerGrabRange(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.GrabStrengthBonus.Value; i++)
                        PunManager.instance.UpgradePlayerGrabStrength(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.GrabThrowBonus.Value; i++)
                        PunManager.instance.UpgradePlayerThrowStrength(SemiFunc.PlayerGetSteamID(player));

                    for (int i = 0; i < StatManager.TumbleLaunchBonus.Value; i++)
                        PunManager.instance.UpgradePlayerTumbleLaunch(SemiFunc.PlayerGetSteamID(player));
                }
                hasAppliedUpgrade = true;
            }
        }

        [HarmonyPatch(typeof(RunManager), "ResetProgress")]
        class RunManagerResetPatch
        {
            static void Postfix()
            {
                hasAppliedUpgrade = false;
            }
        }
    }
}
