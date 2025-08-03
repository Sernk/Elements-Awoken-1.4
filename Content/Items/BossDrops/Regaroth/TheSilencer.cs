using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class TheSilencer : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.damage = 57;
            Item.knockBack = 5;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<TheSilencerP>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile( source,position.X, position.Y, speed.X, speed.Y, ModContent.ProjectileType<TheSilencerP>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            if (Main.rand.Next(6) == 0)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(7);
                position += Vector2.Normalize(new Vector2(speed.X, speed.Y)) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
    }
}