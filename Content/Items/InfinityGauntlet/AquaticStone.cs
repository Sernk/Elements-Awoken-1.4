using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Items.Storyteller;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.InfinityGauntlet
{
    public class AquaticStone : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 10;
        }
        public override void UpdateInventory(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

            if (!player.HasItem(ModContent.ItemType<EmptyGauntlet>()))
            {
                if (player.HasItem(ModContent.ItemType<MoonStone>()) || player.HasItem(ModContent.ItemType<PyroStone>()) || player.HasItem(ModContent.ItemType<AridStone>()) || player.HasItem(ModContent.ItemType<FrigidStone>()) || player.HasItem(ModContent.ItemType<DeathStone>()))
                {
                    if (modPlayer.overInfinityCharged == 0)
                    {
                        Main.NewText(ModContent.GetInstance<EALocalization>().InfinityStone);
                    }
                    modPlayer.overInfinityCharged++;
                }
                else
                {
                    modPlayer.overInfinityCharged = 0;
                }
            }
            else
            {
                modPlayer.overInfinityCharged = 0;
            }

            player.ignoreWater = true;
            player.statManaMax2 += 50;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CInfinityCrys>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 16);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}