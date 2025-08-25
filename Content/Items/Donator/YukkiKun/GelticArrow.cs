using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Donator.YukkiKun
{
    public class GelticArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;
            Item.damage = 7;
            Item.consumable = true;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = 999;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Arrows.GelticArrow>();
            Item.shootSpeed = 13f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.WoodenArrow, 10);
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}