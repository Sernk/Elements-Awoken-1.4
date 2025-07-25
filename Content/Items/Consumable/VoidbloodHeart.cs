using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Consumable
{
    public class VoidbloodHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.rare = ModContent.RarityType<EARarity.Rarity13>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 4;
            Item.UseSound = SoundID.Item119;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Voidblood Heart");
            // Tooltip.SetDefault("Disabled all natural regeneration\nHealing potions are disabled\nThe nurse costs 3x more and is disabled when injured and during bossfights\nYou leak toxic Voidblood on hit and low health");
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<MyPlayer>().voidBlood)
            {
                Main.NewText("Your veins return to normal.", Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = false;
            }
            else
            {
                Main.NewText("Your veins turn black.", Color.DarkRed);
                player.GetModPlayer<MyPlayer>().voidBlood = true;
            }
            return true;
        }
        //public override void AddRecipes()
        //{
        //    ModRecipe recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.CrimsonSeeds, 1);
        //    recipe.AddIngredient(ItemType<Stardust>(), 2);
        //    recipe.AddTile(TileID.DemonAltar);
        //    recipe.SetResult(this);
        //    recipe.AddRecipe(); 
        //    recipe = new ModRecipe(mod);
        //    recipe.AddIngredient(ItemID.CorruptSeeds, 1);
        //    recipe.AddIngredient(ItemType<Stardust>(), 2);
        //    recipe.AddTile(TileID.DemonAltar);
        //    recipe.SetResult(this);
        //    recipe.AddRecipe();
        //}
    }
}
