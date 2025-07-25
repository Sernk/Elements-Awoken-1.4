using Terraria.ModLoader.Config;

namespace ElementsAwoken
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        [Header("$Mods.ElementsAwoken.Config.ElementsAwokenConfig")]
        [LabelKey("$Mods.ElementsAwoken.Config.AlchemistPotionShop")] // Enable Alchemist Potion Shop
        [TooltipKey("$Mods.ElementsAwoken.Config.EnablesShop")] // Enables the Alchemists potion shop, replacing the potion material shop
        public bool alchemistPotions { get; set; }
        [LabelKey("$Mods.ElementsAwoken.Config.ResourceBars")] // Resource Bars
        [TooltipKey("$Mods.ElementsAwoken.Config.TurnsUI")] // Turns the energy and insanity UI into bars
        public bool resourceBars { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.DisableLabs")] // Disable Labs
        [TooltipKey("$Mods.ElementsAwoken.Config.StopsLabs")] // Stops Labs from generating on world gen

        [ReloadRequired]
        public bool labsDisabled { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.DisableBoss")] // Disable Boss Prompts
        [TooltipKey("$Mods.ElementsAwoken.Config.Disablestheeffects")] // Disables the effects that happen in the world 30 minutes after beating a boss

        public bool promptsDisabled { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.ItemChanges")] // Disable Vanilla Item Changes
        [TooltipKey("$Mods.ElementsAwoken.Config.vanillaitems")] // Disabled all changes to vanilla items

        public bool vItemChangesDisabled { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.Screenshake")] // Disable Screenshake
        public bool screenshakeDisabled { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.LowDust")] // Low Dust Mode
        [TooltipKey("$Mods.ElementsAwoken.Config.Reduces")] // Reduces the amount of dust created by NPCs, projectiles and more (WARNING: only applied to certain objects)

        public bool lowDust { get; set; }

        [LabelKey("$Mods.ElementsAwoken.Config.EnterDebug")] // Enter Debug Mode
        [TooltipKey("$Mods.ElementsAwoken.Config.Shows")] // Shows information for testing (Mainly for Mod Devs)

        public bool debugMode { get; set; }
    }
}