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

                    // Health
                    int currentHealth = upgrades.ContainsKey("Health") ? upgrades["Health"] : 0;
                    int healthToAdd = StatManager.HealthBonus.Value - currentHealth;
                    for (int i = 0; i < healthToAdd; i++)
                        PunManager.instance.UpgradePlayerHealth(steamId);

                    // Speed
                    int currentSpeed = upgrades.ContainsKey("Speed") ? upgrades["Speed"] : 0;
                    int speedToAdd = StatManager.SpeedBonus.Value - currentSpeed;
                    for (int i = 0; i < speedToAdd; i++)
                        PunManager.instance.UpgradePlayerSprintSpeed(steamId);

                    // MapCount
                    int currentMapCount = upgrades.ContainsKey("Map Player Count") ? upgrades["Map Player Count"] : 0;
                    int mapCountToAdd = StatManager.MapCountBonus.Value - currentMapCount;
                    for (int i = 0; i < mapCountToAdd; i++)
                        PunManager.instance.UpgradeMapPlayerCount(steamId);

                    // Energy (Stamina)
                    int currentEnergy = upgrades.ContainsKey("Stamina") ? upgrades["Stamina"] : 0;
                    int energyToAdd = StatManager.EnergyBonus.Value - currentEnergy;
                    for (int i = 0; i < energyToAdd; i++)
                        PunManager.instance.UpgradePlayerEnergy(steamId);

                    // ExtraJump
                    int currentExtraJump = upgrades.ContainsKey("Extra Jump") ? upgrades["Extra Jump"] : 0;
                    int extraJumpToAdd = StatManager.ExtraJumpBonus.Value - currentExtraJump;
                    for (int i = 0; i < extraJumpToAdd; i++)
                        PunManager.instance.UpgradePlayerExtraJump(steamId);

                    // GrabRange
                    int currentGrabRange = upgrades.ContainsKey("Range") ? upgrades["Range"] : 0;
                    int grabRangeToAdd = StatManager.GrabRangeBonus.Value - currentGrabRange;
                    for (int i = 0; i < grabRangeToAdd; i++)
                        PunManager.instance.UpgradePlayerGrabRange(steamId);

                    // GrabStrength
                    int currentGrabStrength = upgrades.ContainsKey("Strength") ? upgrades["Strength"] : 0;
                    int grabStrengthToAdd = StatManager.GrabStrengthBonus.Value - currentGrabStrength;
                    for (int i = 0; i < grabStrengthToAdd; i++)
                        PunManager.instance.UpgradePlayerGrabStrength(steamId);

                    // GrabThrow
                    int currentGrabThrow = upgrades.ContainsKey("Throw") ? upgrades["Throw"] : 0;
                    int grabThrowToAdd = StatManager.GrabThrowBonus.Value - currentGrabThrow;
                    for (int i = 0; i < grabThrowToAdd; i++)
                        PunManager.instance.UpgradePlayerThrowStrength(steamId);

                    // TumbleLaunch
                    int currentTumbleLaunch = upgrades.ContainsKey("Launch") ? upgrades["Launch"] : 0;
                    int tumbleLaunchToAdd = StatManager.TumbleLaunchBonus.Value - currentTumbleLaunch;
                    for (int i = 0; i < tumbleLaunchToAdd; i++)
                        PunManager.instance.UpgradePlayerTumbleLaunch(steamId);

                    // Crouch Rest
                    int currentCrouchRest = upgrades.ContainsKey("Crouch Rest") ? upgrades["Crouch Rest"] : 0;
                    int crouchRestToAdd = StatManager.CrouchRestBonus.Value - currentCrouchRest;
                    for (int i = 0; i < crouchRestToAdd; i++)
                        PunManager.instance.UpgradePlayerCrouchRest(steamId);

                    // Tumble Wings
                    int currentTumbleWings = upgrades.ContainsKey("Tumble Wings") ? upgrades["Tumble Wings"] : 0;
                    int tumbleWingsToAdd = StatManager.TumbleWingsBonus.Value - currentTumbleWings;
                    for (int i = 0; i < tumbleWingsToAdd; i++)
                        PunManager.instance.UpgradePlayerTumbleWings(steamId);

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