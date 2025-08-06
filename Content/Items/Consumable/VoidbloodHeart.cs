using ElementsAwoken.Content.Items.Materials;
using ElementsAwoken.EASystem.Global;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.UI.Tooltips.EARarity;

namespace ElementsAwoken.Content.Items.Consumable
{
    public class VoidbloodHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Rarity13>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item119;
        }
        public override bool CanUseItem(Player player)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            if (player.GetModPlayer<MyPlayer>().voidBlood)
            {
                Main.NewText(EALocalization.VoidbloodHeart, Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = false;
            }
            else
            {
                Main.NewText(EALocalization.VoidbloodHeart1, Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = true;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimsonSeeds, 1);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.CorruptSeeds, 1);
            recipe1.AddIngredient(ModContent.ItemType<Stardust>(), 2);
            recipe1.AddTile(TileID.DemonAltar);
            recipe1.Register();
        }
    }
}
