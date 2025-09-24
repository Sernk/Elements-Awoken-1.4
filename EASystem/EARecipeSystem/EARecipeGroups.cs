using ElementsAwoken.Content.Items.Elements.Sky;
using ElementsAwoken.Content.Items.Elements.Void;
using ElementsAwoken.Content.Items.Elements.Water;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ElementsAwoken.EASystem.EARecipeSystem
{
    public class EARecipeGroups : ModSystem, ILocalizedModType
    {
        public string LocalizationCategory => "LocalizationEARecipeGroups";

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

        public override void Load()
        {
            _ = this.GetLocalization("RecipeGroups.Name").Value;
            _ = this.GetLocalization("RecipeGroups.Name1").Value;
            _ = this.GetLocalization("RecipeGroups.Name2").Value;
            _ = this.GetLocalization("RecipeGroups.Name3").Value;
            _ = this.GetLocalization("RecipeGroups.Name4").Value;
            _ = this.GetLocalization("RecipeGroups.Name5").Value;
            _ = this.GetLocalization("RecipeGroups.Name6").Value;
            _ = this.GetLocalization("RecipeGroups.Name7").Value;
            _ = this.GetLocalization("RecipeGroups.Name8").Value;
            _ = this.GetLocalization("RecipeGroups.Name9").Value;
            _ = this.GetLocalization("RecipeGroups.Name10").Value;
            _ = this.GetLocalization("RecipeGroups.Name11").Value;
            _ = this.GetLocalization("RecipeGroups.Name12").Value;
            _ = this.GetLocalization("RecipeGroups.Name13").Value;
            _ = this.GetLocalization("RecipeGroups.Name14").Value;
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name").Value, [492, 493, 665, 749, 761, 785, 786, 821, 822, 823, 948, 1162, 1165, 1515, 1583, 1584, 1585, 1586, 1797, 1830, 1871, 2280, 2494, 2609, 2770, 3468, 3469, 3470, 3471, 3580, 3582, 3588, 3592, ItemID.ArkhalisWings, ItemID.LeinforsWings, ModContent.ItemType<VoidBoots>(), ModContent.ItemType<BubblePack>(), ModContent.ItemType<SkylineWings>()]);
            RecipeGroup.RegisterGroup(WingGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name1").Value, [3468, 3469, 3470, 3471]);
            RecipeGroup.RegisterGroup(LunarWings, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name2").Value, [ItemID.SilverBroadsword, ItemID.TungstenBroadsword,]);
            RecipeGroup.RegisterGroup(SilverSword, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name3").Value, [ItemID.SandBlock, ItemID.CrimsandBlock, ItemID.EbonsandBlock, ItemID.PearlsandBlock,]);
            RecipeGroup.RegisterGroup(SandGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name4").Value, [ItemID.Sandstone, ItemID.CrimsonSandstone, ItemID.CorruptSandstone, ItemID.HallowSandstone,]);
            RecipeGroup.RegisterGroup(SandstoneGroup, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name5").Value, [ItemID.IceBlock, ItemID.RedIceBlock, ItemID.PurpleIceBlock, ItemID.PinkIceBlock,]);
            RecipeGroup.RegisterGroup(IceGroup, group);

            // bars
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name6").Value, [ItemID.CopperBar, ItemID.TinBar,]);
            RecipeGroup.RegisterGroup(CopperBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name7").Value, [ItemID.IronBar, ItemID.LeadBar,]);
            RecipeGroup.RegisterGroup(IronBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name8").Value, [ItemID.SilverBar, ItemID.TungstenBar,]);
            RecipeGroup.RegisterGroup(SilverBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name9").Value, [ItemID.GoldBar, ItemID.PlatinumBar,]);
            RecipeGroup.RegisterGroup(GoldBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name10").Value, [ItemID.DemoniteBar, ItemID.CrimtaneBar,]);
            RecipeGroup.RegisterGroup(EvilBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name11").Value, [ItemID.DemoniteOre, ItemID.CrimtaneOre,]);
            RecipeGroup.RegisterGroup(EvilOre, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name12").Value, [ItemID.CobaltBar, ItemID.PalladiumBar,]);
            RecipeGroup.RegisterGroup(CobaltBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name13").Value, [ItemID.MythrilBar, ItemID.OrichalcumBar,]);
            RecipeGroup.RegisterGroup(MythrilBar, group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + this.GetLocalization("RecipeGroups.Name14").Value, [ItemID.AdamantiteBar, ItemID.TitaniumBar,]);
            RecipeGroup.RegisterGroup(AdamantiteBar, group);
        }
    }
}