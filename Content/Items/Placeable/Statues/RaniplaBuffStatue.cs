using ElementsAwoken.Content.Buffs.TileBuffs;
using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.Content.Tiles.Statues;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Placeable.Statues
{
    public class RaniplaBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ArmorStatue);
            Item.createTile = ModContent.TileType<RaniplaBuff>();
			Item.placeStyle = 0;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(ModContent.BuffType<StatueBuffBurst>()) == -1 && player.FindBuffIndex(ModContent.BuffType<StatueBuffGenihWat>()) == -1 && player.FindBuffIndex(ModContent.BuffType<StatueBuffRanipla>()) == -1 && player.FindBuffIndex(ModContent.BuffType<StatueBuffAmadis>()) == -1 && player.FindBuffIndex(ModContent.BuffType<StatueBuffOinite>()) == -1)
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
            recipe.AddIngredient(ModContent.ItemType<RaniplaStatue>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
