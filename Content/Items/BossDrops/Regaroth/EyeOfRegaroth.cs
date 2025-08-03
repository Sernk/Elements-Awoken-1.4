using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Regaroth
{
    public class EyeOfRegaroth : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 42;
            Item.mana = 5;
            Item.useTime = 12;
            Item.useAnimation = 16;
            Item.useStyle = 5;
            Item.UseSound = SoundID.Item8;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.staff[Item.type] = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.shoot = ModContent.ProjectileType<TheSilencerP>();
            Item.shootSpeed = 18f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 speed, int type, int damage, float knockback)
        {
            int numberProjectiles = 2 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speed.X, speed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void HoldItem(Player player)
        {
            Vector2 vector32 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector32.X = (float)player.bodyFrame.Width - vector32.X;
            }
            if (player.gravDir != 1f)
            {
                vector32.Y = (float)player.bodyFrame.Height - vector32.Y;
            }
            vector32 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            Vector2 position = player.RotatedRelativePoint(player.position + vector32, true) - player.velocity;
            for (int num277 = 0; num277 < 4; num277++)
            {
                int dustType = 135;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        dustType = 135;
                        break;
                    case 1:
                        dustType = 164;
                        break;
                    default: break;
                }
                Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, dustType, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f)];
                dust.position = position;
                dust.velocity *= 0f;
                dust.noGravity = true;
                dust.fadeIn = 1f;
                dust.velocity += player.velocity;
                if (Main.rand.Next(2) == 0)
                {
                    dust.position += Utils.RandomVector2(Main.rand, -4f, 4f);
                    dust.scale += Main.rand.NextFloat();
                    if (Main.rand.Next(2) == 0)
                    {
                        dust.customData = player;
                    }
                }
            }
        }
    }
}