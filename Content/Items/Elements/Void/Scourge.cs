using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.Elements.Void
{
    public class Scourge : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 134;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 16;
            Item.useTime = 4;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 3.25f;
            Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ScourgeFire>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Gel;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .75f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int num6 = Main.rand.Next(2, 3);
            for (int index = 0; index < num6; ++index)
            {
                float SpeedX = speed.X + (float)Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speed.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
                switch (Main.rand.Next(3))
                {
                    case 0: type = ModContent.ProjectileType<ScourgeFire>(); break;
                    default: break;
                }
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(EAU.VoidEssence, 10);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(EAU.ElementalForge);
            recipe.Register();
        }
    }
}