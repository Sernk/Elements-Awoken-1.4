using ElementsAwoken.Content.Projectiles.Yoyos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.ItemSets.Mortemite
{
    public class Decay : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Code2);
            Item.useStyle = 5;
            Item.damage = 150;
            Item.width = 16;
            Item.height = 16;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = 541;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 10;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<DecayP>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MortemiteDust>(), 50);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}