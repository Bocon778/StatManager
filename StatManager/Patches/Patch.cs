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

            if (!hasAppliedUpgrade && SemiFunc.RunIsLevel() && SemiFunc.IsMasterClientOrSingleplayer())
            {
                foreach (PlayerAvatar player in SemiFunc.PlayerGetAll())
                {
                    var steamId = SemiFunc.PlayerGetSteamID(player);
                    var upgrades = StatsManager.instance.FetchPlayerUpgrades(steamId);

                    int healthToAdd = StatManager.HealthBonus.Value - (upgrades.ContainsKey("Health") ? upgrades["Health"] : 0);
                    if (healthToAdd != 0)
                        PunManager.instance.UpgradePlayerHealth(steamId, healthToAdd);

                    int speedToAdd = StatManager.SpeedBonus.Value - (upgrades.ContainsKey("Speed") ? upgrades["Speed"] : 0);
                    if (speedToAdd != 0)
                        PunManager.instance.UpgradePlayerSprintSpeed(steamId, speedToAdd);

                    int mapCountToAdd = StatManager.MapCountBonus.Value - (upgrades.ContainsKey("Map Player Count") ? upgrades["Map Player Count"] : 0);
                    if (mapCountToAdd != 0)
                        PunManager.instance.UpgradeMapPlayerCount(steamId, mapCountToAdd);

                    int energyToAdd = StatManager.EnergyBonus.Value - (upgrades.ContainsKey("Stamina") ? upgrades["Stamina"] : 0);
                    if (energyToAdd != 0)
                        PunManager.instance.UpgradePlayerEnergy(steamId, energyToAdd);

                    int extraJumpToAdd = StatManager.ExtraJumpBonus.Value - (upgrades.ContainsKey("Extra Jump") ? upgrades["Extra Jump"] : 0);
                    if (extraJumpToAdd != 0)
                        PunManager.instance.UpgradePlayerExtraJump(steamId, extraJumpToAdd);

                    int grabRangeToAdd = StatManager.GrabRangeBonus.Value - (upgrades.ContainsKey("Range") ? upgrades["Range"] : 0);
                    if (grabRangeToAdd != 0)
                        PunManager.instance.UpgradePlayerGrabRange(steamId, grabRangeToAdd);

                    int grabStrengthToAdd = StatManager.GrabStrengthBonus.Value - (upgrades.ContainsKey("Strength") ? upgrades["Strength"] : 0);
                    if (grabStrengthToAdd != 0)
                        PunManager.instance.UpgradePlayerGrabStrength(steamId, grabStrengthToAdd);

                    int tumbleLaunchToAdd = StatManager.TumbleLaunchBonus.Value - (upgrades.ContainsKey("Launch") ? upgrades["Launch"] : 0);
                    if (tumbleLaunchToAdd != 0)
                        PunManager.instance.UpgradePlayerTumbleLaunch(steamId, tumbleLaunchToAdd);

                    int crouchRestToAdd = StatManager.CrouchRestBonus.Value - (upgrades.ContainsKey("Crouch Rest") ? upgrades["Crouch Rest"] : 0);
                    if (crouchRestToAdd != 0)
                        PunManager.instance.UpgradePlayerCrouchRest(steamId, crouchRestToAdd);

                    int tumbleWingsToAdd = StatManager.TumbleWingsBonus.Value - (upgrades.ContainsKey("Tumble Wings") ? upgrades["Tumble Wings"] : 0);
                    if (tumbleWingsToAdd != 0)
                        PunManager.instance.UpgradePlayerTumbleWings(steamId, tumbleWingsToAdd);

                    int tumbleClimbToAdd = StatManager.TumbleClimbBonus.Value - (upgrades.ContainsKey("Tumble Climb") ? upgrades["Tumble Climb"] : 0);
                    if (tumbleClimbToAdd != 0)
                        PunManager.instance.UpgradePlayerTumbleClimb(steamId, tumbleClimbToAdd);

                    int deathHeadToAdd = StatManager.DeathHeadBonus.Value - (upgrades.ContainsKey("Death Head") ? upgrades["Death Head"] : 0);
                    if (deathHeadToAdd != 0)
                        PunManager.instance.UpgradeDeathHeadBattery(steamId, deathHeadToAdd);
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