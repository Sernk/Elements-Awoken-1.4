using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.UI.Tooltips;
using ElementsAwoken.Events.VoidEvent;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossSummons
{
    public class VoidEventSummon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.rare = 10;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item44;
            Item.GetGlobalItem<EABossSummon>().AwakenedSummonItem = true;
        }
        public override bool CanUseItem(Player player)
        {
            var e = ModContent.GetInstance<EALocalization>();
            if (!Main.dayTime && Main.time > 9000)
            {
                Main.NewText(e.VoidEventSummon, 182, 15, 15);
                return false;
            }
            else if (MyWorld.voidInvasionUp)
            {
                Main.NewText(e.VoidEventSummon1, 182, 15, 15);
                return false;
            }
            else if (MyWorld.voidInvasionWillStart)
            {
                Main.NewText(e.VoidEventSummon2, 182, 15, 15);
                return false;
            }
            return true;
        }
        public override bool? UseItem(Player player)
        {
            var e = ModContent.GetInstance<EALocalization>();
            if (!Main.dayTime && !MyWorld.voidInvasionUp)
            {
                Main.NewText(e.VoidEventSummon3, 182, 15, 15);
                VoidEvent.StartInvasion();
                return true;
            }
            else if (!MyWorld.voidInvasionWillStart)
            {
                Main.NewText(e.VoidEventSummon4, 182, 15, 15);
                MyWorld.voidInvasionWillStart = true;
                return true;
            }
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); 
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Pyroplasm>(), 15);
            recipe.AddIngredient(ItemID.LunarBar, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
