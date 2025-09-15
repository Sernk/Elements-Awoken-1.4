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
    public class BurstBuffStatue : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ArmorStatue);
            Item.createTile = TileType<BurstBuff>();
			Item.placeStyle = 0;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statue of the Story Spinner");
            // Tooltip.SetDefault("I am not really creative with tooltips, y'know. I do not even know if I exist.\nMaybe I am even just an illusion? Oh my god, my whole life was a lie!\n15% increased ranged damage\n15% decreased movement speed");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(BuffType<StatueBuffAmadis>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffGenihWat>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffRanipla>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOrange>()) == -1 && player.FindBuffIndex(BuffType<StatueBuffOinite>()) == -1)
            {
                return true;
            }
            else
            {
                Main.NewText(GetInstance<EALocalization>().Statues, Color.Red);
                return false;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemType<BurstStatue>(), 1);
            recipe.AddIngredient(ItemType<MysticLeaf>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
