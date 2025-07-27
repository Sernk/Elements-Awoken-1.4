using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem
{
    public class EARecipeGroups : ModSystem
    {
        public const string WingGroup = "WingGroup";
        public const string LunarWings = "LunarWings";
        public const string SilverSword = "SilverSword";
        public const string SandGroup = "SandGroup";
        public const string SandstoneGroup = "SandstoneGroup";
        public const string IceGroup = "IceGroup";
        public const string CopperBar = "CopperBar";
        public const string IronBar = "IronBar";
        public const string SilverBar = "SilverBar";
        public const string GoldBar = "GoldBar";
        public const string EvilBar = "EvilBar";
        public const string EvilOre = "EvilOre";
        public const string CobaltBar = "CobaltBar";
        public const string MythrilBar = "MythrilBar";
        public const string AdamantiteBar = "AdamantiteBar";
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Wings"), new int[] { 492, 493, 665, 749, 761, 785, 786, 821, 822, 823, 948, 1162, 1165, 1515, 1583, 1584, 1585, 1586, 1797, 1830, 1871, 2280, 2494, 2609, 2770, 3468, 3469, 3470, 3471, 3580, 3582, 3588, 3592, ItemID.ArkhalisWings, ItemID.LeinforsWings, /*ItemType("VoidWings"),ItemType("BubblePack"),ItemType("SkylineWings")*/});
            RecipeGroup.RegisterGroup(WingGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Lunar Wings"), new int[] { 3468, 3469, 3470, 3471 });
            RecipeGroup.RegisterGroup(LunarWings, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver or Tungsten Sword"), new int[] { ItemID.SilverBroadsword, ItemID.TungstenBroadsword, });
            RecipeGroup.RegisterGroup(SilverSword, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sand"), new int[] { ItemID.SandBlock, ItemID.CrimsandBlock, ItemID.EbonsandBlock, ItemID.PearlsandBlock, });
            RecipeGroup.RegisterGroup(SandGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sandstone"), new int[] { ItemID.Sandstone, ItemID.CrimsonSandstone, ItemID.CorruptSandstone, ItemID.HallowSandstone, });
            RecipeGroup.RegisterGroup(SandstoneGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Ice Block"), new int[] { ItemID.IceBlock, ItemID.RedIceBlock, ItemID.PurpleIceBlock, ItemID.PinkIceBlock, });
            RecipeGroup.RegisterGroup(IceGroup, group);

            // bars
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Copper Bar"), new int[] { ItemID.CopperBar, ItemID.TinBar, });
            RecipeGroup.RegisterGroup(CopperBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Iron Bar"), new int[] { ItemID.IronBar, ItemID.LeadBar, });
            RecipeGroup.RegisterGroup(IronBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver Bar"), new int[] { ItemID.SilverBar, ItemID.TungstenBar, });
            RecipeGroup.RegisterGroup(SilverBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Gold Bar"), new int[] { ItemID.GoldBar, ItemID.PlatinumBar, });
            RecipeGroup.RegisterGroup(GoldBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Evil Bar"), new int[] { ItemID.DemoniteBar, ItemID.CrimtaneBar, });
            RecipeGroup.RegisterGroup(EvilBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Evil Ore"), new int[] { ItemID.DemoniteOre, ItemID.CrimtaneOre, });
            RecipeGroup.RegisterGroup(EvilOre, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Cobalt Bar"), new int[] { ItemID.CobaltBar, ItemID.PalladiumBar, });
            RecipeGroup.RegisterGroup(CobaltBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Mythril Bar"), new int[] { ItemID.MythrilBar, ItemID.OrichalcumBar, });
            RecipeGroup.RegisterGroup(MythrilBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Adamantite Bar"), new int[] { ItemID.AdamantiteBar, ItemID.TitaniumBar, });
            RecipeGroup.RegisterGroup(AdamantiteBar, group);
        }
    }
}