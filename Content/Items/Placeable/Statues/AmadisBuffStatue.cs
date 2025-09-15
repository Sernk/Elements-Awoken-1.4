using ElementsAwoken.Content.Buffs.TileBuffs;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Tiles.Statues;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Items.Placeable.Statues
{
    public class AmadisBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ArmorStatue);
            Item.createTile = TileType<AmadisBuff>();
			Item.placeStyle = 0;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(BuffType<StatueBuffBurst>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffGenihWat>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffRanipla>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOrange>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOinite>()) == -1)
            {
                return true;
            }
            else
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Statues, Color.Red);
                return false;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemType<AmadisStatue>(), 1);
            recipe.AddIngredient(ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}