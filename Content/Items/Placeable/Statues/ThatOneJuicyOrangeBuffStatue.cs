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
    public class ThatOneJuicyOrangeBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 11;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = 0;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ThatOneJuicyOrangeBuff>();
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
            recipe.AddIngredient(ModContent.ItemType<ThatOneJuicyOrangeStatue>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}