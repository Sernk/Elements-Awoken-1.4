using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.Loot;
public class BiomeConditions : IItemDropRuleCondition
{
    private readonly BiomeID biomeType;
    private readonly int customBiome;
    private readonly string nearbyDescription;

    private Player player => Main.LocalPlayer;

    public enum BiomeID
    {
        Desert,
        Sky,
        Underworld,
        InBeach,
        Frost,
        InDungeon,
        InNebulaTowerZone,
        InSolarTowerZone,
        InVortexTowerZone,
        InStardustTowerZone,
        InbloodMoon,
        InVoid
    }

    public BiomeConditions(BiomeID biomeType, int customBiomeID = -1, bool INVoid = false, string VoidText ="")
    {
        this.biomeType = biomeType;
        this.customBiome = customBiomeID;
        string biomeName = GetBiomeName();
        if (INVoid)
        {
            this.nearbyDescription = VoidText;
        }
        else
        {
            string text = string.Format(ModContent.GetInstance<EALocalization>().BiomeConditions, biomeName);
            this.nearbyDescription = text;
        }
    }
    public BiomeConditions(BiomeID biomeType)
    {
        this.biomeType = biomeType;
    }
    public string GetBiomeName()
    {
        return biomeType switch
        {
            BiomeID.Desert => ModContent.GetInstance<EALocalization>().Desert,
            BiomeID.Sky => ModContent.GetInstance<EALocalization>().Sky,
            BiomeID.Underworld => ModContent.GetInstance<EALocalization>().Hell,
            BiomeID.Frost => ModContent.GetInstance<EALocalization>().Frost,
            BiomeID.InBeach => ModContent.GetInstance<EALocalization>().Beach,
            BiomeID.InVoid => ModContent.GetInstance<EALocalization>().InVoid,
            BiomeID.InNebulaTowerZone => "Nebula Tower",
            BiomeID.InSolarTowerZone => "Solar Tower",
            BiomeID.InVortexTowerZone => "Vortex Tower",
            BiomeID.InStardustTowerZone => "Stardust Tower",
            BiomeID.InbloodMoon => "Blood Moon",
            BiomeID.InDungeon => "Dungeon",
            _ => "Unknown Biome",
        };
    }
    public bool GetBiomeNearby()
    {
        return biomeType switch
        {
            BiomeID.Desert => player.ZoneDesert && player.ZoneBeach == false,
            BiomeID.Sky => player.ZoneSkyHeight,
            BiomeID.Underworld => player.ZoneUnderworldHeight,
            BiomeID.Frost => player.ZoneSnow,
            BiomeID.InNebulaTowerZone => player.ZoneTowerNebula,
            BiomeID.InSolarTowerZone => player.ZoneTowerSolar,
            BiomeID.InVortexTowerZone => player.ZoneTowerVortex,
            BiomeID.InStardustTowerZone => player.ZoneTowerStardust,
            BiomeID.InbloodMoon => Main.bloodMoon,
            BiomeID.InBeach => player.ZoneBeach,
            BiomeID.InDungeon => player.ZoneDungeon,
            BiomeID.InVoid => MyWorld.voidInvasionUp,
            _ => false,
        };
    }
    public bool CanDrop(DropAttemptInfo info) => GetBiomeNearby();
    public bool CanShowItemDropInUI() => true;
    public string GetConditionDescription() => nearbyDescription;
}
