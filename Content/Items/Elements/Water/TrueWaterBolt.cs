using ElementsAwoken.Content.Items.Essence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Water
{
    public class TrueWaterBolt : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterBolt);
            Item.damage = 100;
            Item.shootSpeed *= 2f;
            Item.shoot = ProjectileID.WaterBolt;
            Item.value = Item.buyPrice(0, 75, 0, 0);
            Item.rare = 8;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int num6 = 3;
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speed.X + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speed.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(2))
                {
                    case 0: type = ProjectileID.WaterBolt; break;
                    default: break;
                }
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 8);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(ItemID.WaterBolt, 1);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}