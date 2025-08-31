using ElementsAwoken.Content.Items.BossDrops.VoidLeviathan;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.Darkstone;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Events.VoidEvent;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class VoidEventSummon2 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = ModContent.RarityType<EARarity.Rarity12>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (MyWorld.voidInvasionUp)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().VoidEventSummon1, 182, 15, 15);
                return false;
            }
            else if (Main.time < 16220 || Main.dayTime)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().VoidEventSummon5, 182, 15, 15);
                return false;
            }
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (!Main.dayTime && !MyWorld.voidInvasionUp && Main.time > 16220)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().VoidEventSummon6, 182, 15, 15);
                VoidEvent.StartInvasion();
                return true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidAshes>(), 2);
            recipe.AddIngredient(ModContent.ItemType<VoidEssence>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Darkstone>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
