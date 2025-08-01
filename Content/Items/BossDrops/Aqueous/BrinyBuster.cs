using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Aqueous
{
    class BrinyBuster : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 10;
            Item.damage = 60;
            Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(0, 50, 0, 0);
            Item.rare = 8;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = 5;
            Item.useAnimation = 40; 
            Item.useTime = 40;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<BrinyBusterP>();
            Item.shootSpeed = 15.1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}