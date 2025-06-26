using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using StatManager.Patches;

namespace StatManager
{
    [BepInPlugin(modGUID, modeName, modVersion)]
    public class StatManager : BaseUnityPlugin
    {
        private const string modGUID = "Bocon.StatManager";
        private const string modeName = "Stat Manager";
        private const string modVersion = "1.2.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static StatManager Instance;

        internal ManualLogSource mls;

        internal static ConfigEntry<int> HealthBonus;
        internal static ConfigEntry<int> SpeedBonus;
        internal static ConfigEntry<int> MapCountBonus;
        internal static ConfigEntry<int> EnergyBonus;
        internal static ConfigEntry<int> ExtraJumpBonus;
        internal static ConfigEntry<int> GrabRangeBonus;
        internal static ConfigEntry<int> GrabStrengthBonus;
        internal static ConfigEntry<int> GrabThrowBonus;
        internal static ConfigEntry<int> TumbleLaunchBonus;
        internal static ConfigEntry<int> CrouchRestBonus;
        internal static ConfigEntry <int> TumbleWingsBonus;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Bocon.StatManager has loaded!");

            // Create configuration entries
            HealthBonus = Config.Bind("Stats", "Health", 10, "Amount of Health upgrades.");
            SpeedBonus = Config.Bind("Stats", "Sprint Speed", 5, "Amount of Sprint Speed upgrades.");
            MapCountBonus = Config.Bind("Stats", "Player Map Count", 1, "Amount of Player Map Count upgrades.");
            EnergyBonus = Config.Bind("Stats", "Energy", 10, "Amount of Energy upgrades.");
            ExtraJumpBonus = Config.Bind("Stats", "Extra Jump", 1, "Amount of Extra Jump upgrades.");
            GrabRangeBonus = Config.Bind("Stats", "Grab Range", 5, "Amount of Grab Range upgrades.");
            GrabStrengthBonus = Config.Bind("Stats", "Grab Strength", 5, "Amount of Grab Strength upgrades.");
            GrabThrowBonus = Config.Bind("Stats", "Grab Throw", 5, "Amount of Grab Throw upgrades.");
            TumbleLaunchBonus = Config.Bind("Stats", "Tumble Launch", 5, "Amount of Tumble Launch upgrades.");
            CrouchRestBonus = Config.Bind("Stats", "Crouch Rest", 2, "Amount of Crouch rest upgrades.");
            TumbleWingsBonus = Config.Bind("Stats", "Tumble Wings", 2, "Amount of Tubmle Wing upgrades.");

            harmony.PatchAll();
        }
    }
}
