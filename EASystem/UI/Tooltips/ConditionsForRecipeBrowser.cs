using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.EAUtilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Loot.BIDRC;
using static ElementsAwoken.EASystem.Loot.BiomeConditions;
using static ElementsAwoken.EAUtilities.EAColors;

namespace ElementsAwoken.EASystem.UI.Tooltips
{
    public class ConditionsForRecipeBrowser : GlobalItem
    {
        public static string GetConditionDescription(string BossName, string BiomeName)
        {
            string BossNametext = string.Format(ModContent.GetInstance<EALocalization>().BIDRC, BossName);
            string BiomeNametext = string.Format(ModContent.GetInstance<EALocalization>().BiomeConditions, BiomeName);
            return "Icon" + " " + "Recipe Browser:" + " " + BossNametext + " " + "&" + " " + BiomeNametext;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ElementsAwoken.recipebrowser && ModContent.GetInstance<Config>().RecipeBrowser == false)
            {
                if (item.type == ModContent.ItemType<DesertEssence>()) tooltips.Add(new TooltipLine(Mod, "RecipeBrowser:Conditions", GetConditionDescription(new BIDRC(BossType.EyeOfCthulhu).GetBossName(), new BiomeConditions(BiomeID.Desert).GetBiomeName())) { OverrideColor = White });
                if (item.type == ModContent.ItemType<FireEssence>()) tooltips.Add(new TooltipLine(Mod, "RecipeBrowser:Conditions", GetConditionDescription(new BIDRC(BossType.Skeletron).GetBossName(), new BiomeConditions(BiomeID.Underworld).GetBiomeName()))  { OverrideColor = White });
                if (item.type == ModContent.ItemType<SkyEssence>()) tooltips.Add(new TooltipLine(Mod, "RecipeBrowser:Conditions", GetConditionDescription(new BIDRC(BossType.AllMechs).GetBossName(), new BiomeConditions(BiomeID.Sky).GetBiomeName())) { OverrideColor = White });
                if (item.type == ModContent.ItemType<FrostEssence>()) tooltips.Add(new TooltipLine(Mod, "RecipeBrowser:Conditions", GetConditionDescription(new BIDRC(BossType.Plantera).GetBossName(), new BiomeConditions(BiomeID.Frost).GetBiomeName())) { OverrideColor = White });
                if (item.type == ModContent.ItemType<WaterEssence>()) tooltips.Add(new TooltipLine(Mod, "RecipeBrowser:Conditions", GetConditionDescription(new BIDRC(BossType.DukeFishron).GetBossName(), new BiomeConditions(BiomeID.InBeach).GetBiomeName())) { OverrideColor = White });
            }
        }
    }
}